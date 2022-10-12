using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebApi.Controllers;

[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<HtmlToPdfUser> _userManager;
    private readonly SignInManager<HtmlToPdfUser> _signInManager;
    
    
    public AuthController(IConfiguration configuration,
        SignInManager<HtmlToPdfUser> signInManager,
        UserManager<HtmlToPdfUser> userManager)
    {
        _configuration = configuration;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LogIn([FromBody] HtmlToPdfLoginModel loginModel)
    {
        IActionResult result = Unauthorized();
        if (await _userManager.FindByNameAsync(loginModel.Email) is not { } user) return result;
        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);
        
        if (signInResult.Succeeded)
        {
            result = Ok(new LoginResponse {Token = BuildJwtToken(user.Id)});
        }

        return result;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] HtmlToPdfLoginModel loginModel)
    {
        var identityResult = await _userManager.CreateAsync(new HtmlToPdfUser()
        {
            UserName = loginModel.Email,
            Email = loginModel.Email
        }, loginModel.Password);
        
        var result = identityResult.Succeeded ?
            "User Created, Eureka!" :
            @$"Error! Code: { identityResult.Errors.FirstOrDefault()?.Code }, 
                   Description: {identityResult.Errors.FirstOrDefault()?.Description }";
        return Ok(result);
    }

    private string BuildJwtToken(string userId)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token =  new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
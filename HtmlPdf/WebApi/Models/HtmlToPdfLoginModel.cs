namespace WebApi.Models;

public class HtmlToPdfLoginModel
{
    public string Email { get; set; }
    
    /// <summary>
    /// Ideally we add another "ConfirmPassword" field and check if correct
    /// </summary>
    public string Password { get; set; }
}
using WebApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Context;


/// <summary>
/// Ideally, we add this to a separate project "Repository" or whatever naming
/// </summary>
public class HtmlToPdfContext: IdentityDbContext<HtmlToPdfUser>
{
    public HtmlToPdfContext(DbContextOptions options) : base(options)
    {
        
    }
}
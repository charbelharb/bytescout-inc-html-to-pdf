using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Core;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class HtmlToPdfAuthorize: AuthorizeAttribute
{
    public HtmlToPdfAuthorize()
    {
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
    }
}
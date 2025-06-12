using Microsoft.AspNetCore.Identity;

namespace CodeBlog.API.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> Roles);
    }
}

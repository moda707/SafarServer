using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SafarObjects.UserClasses;

namespace SafarApi.Core
{
    public abstract class SafarControllerBase : Controller
    {
        readonly UserManager<Users> userManager;

        protected SafarControllerBase(UserManager<Users> userManager)
        {
            this.userManager = userManager;
        }

        protected async Task<Users> GetCurrentUserAsync()
        {
            return await userManager.GetUserAsync(User);
        } 
    }
}

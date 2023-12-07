using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Okta.AspNetCore;
namespace pract5.Controllers
{
    public class AccountController : Controller
    {


        public IActionResult Login([FromQuery] string returnUrl)
        {
            var redirectUri = returnUrl is null ? Url.Content("~/") : "/" + returnUrl;
            if (User.Identity.IsAuthenticated)
            {
                return LocalRedirect(redirectUri);
            }
            return Challenge();
        }
        [Authorize]
        public async Task<IActionResult> Logout([FromQuery] string returnUrl)
        {
            var redirectUri = returnUrl is null ? Url.Content("~/") : "/" + returnUrl;
            if (!User.Identity.IsAuthenticated)
            {
                return LocalRedirect(redirectUri);
            }
            await HttpContext.SignOutAsync();
            return LocalRedirect(redirectUri);
        }
        /*
       public IActionResult SignIn()
       {
           if (!HttpContext.User.Identity.IsAuthenticated)
           {
               return Challenge(OktaDefaults.MvcAuthenticationScheme);
           }

           return RedirectToAction("Index", "Home");
       }
       [HttpPost]
       public IActionResult SignOut()
       {
           return new SignOutResult(
              new[]
              {
        OktaDefaults.MvcAuthenticationScheme,
        CookieAuthenticationDefaults.AuthenticationScheme,
              },
              new AuthenticationProperties { RedirectUri = "/Home/" });
       }
       */
    }
}

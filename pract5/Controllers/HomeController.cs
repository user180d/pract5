using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pract5.Models;
using System.Diagnostics;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Runtime.InteropServices.JavaScript;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;

namespace pract5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OktaApiService _oktaApiService;

        public HomeController(ILogger<HomeController> logger, OktaApiService oktaApiService)
        {
            _logger = logger;
            _oktaApiService = oktaApiService;
        }

        public IActionResult Index()
        {
            return View();
        }

      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            string about="";
            string id = HttpContext.User.Identity.Name;

            if (string.IsNullOrEmpty(id)) 
            {
                return BadRequest("User ID not found.");
            }
            string content = await _oktaApiService.GetProfile(id);
            JObject res = JObject.Parse(content.Substring(1, content.Length - 2));

            var viewModel = new UserViewModel
            {
                email = id,
                about = res["profile"]?["about"]?.ToString(),
                phone = res["profile"]?["mobilePhone"]?.ToString(),
                nickname = res["profile"]?["usernick"]?.ToString()
            };
            return View(viewModel);
        }
    }
}
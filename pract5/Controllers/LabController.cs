using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using pract5.Labs;
using pract5.Models;
using System.Diagnostics;

namespace pract5.Controllers
{
    [Authorize]
    public class LabController : Controller
    {
        Lab1 l = new Lab1();
        Lab2 l2 = new Lab2();
        Lab3 l3 = new Lab3();
        public async Task LogTokenAndClaims()
        {
            var identityToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);

            Debug.WriteLine($"Identity token:  {identityToken}");

            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"Claim type: {claim.Type} - Claim value: {claim.Value}");
            }
        }
        public IActionResult Lab1()
        {
            LogTokenAndClaims();
            var model = new DataModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Calculate1(DataModel model)
        {
            if (model.input == null)
            {
                return RedirectToAction("Lab1", "Lab");
            }
            l.input = model.input;
            model.result = l.start();
            return View("Lab1", model);
        }
        public IActionResult Lab2()
        {
            LogTokenAndClaims();
            var model = new DataModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Calculate2(DataModel model)
        {
            if (model.input==null)
            {
                return RedirectToAction("Lab2", "Lab");
            }
            l2.input = model.input;
            model.result = l2.start();
            return View("Lab2", model);
        }
        public IActionResult Lab3()
        {
            LogTokenAndClaims();
            var model = new DataModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Calculate3(DataModel model)
        {
            if (!string.IsNullOrEmpty(model.input))
            {
                string[] lines = new string[] { model.row + " " + model.col };
                l3.input = lines.Concat(model.input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)).ToArray();
            }
            else 
            {
                return RedirectToAction("Lab3", "Lab");
            }
            model.result = l3.start();
            return View("Lab3", model);
        }
    }
}

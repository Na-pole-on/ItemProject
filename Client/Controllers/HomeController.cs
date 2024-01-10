using Client.Models;
using Client.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
            => View();

        [Authorize]
        public IActionResult Login()
            => RedirectToAction("Index");

        [Authorize]
        public IActionResult Logout()
            => SignOut("Cookies", "oidc");
    }
}
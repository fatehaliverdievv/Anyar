using Anyar.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Anyar.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
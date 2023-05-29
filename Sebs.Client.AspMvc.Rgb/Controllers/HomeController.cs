using Microsoft.AspNetCore.Mvc;
using Sebs.Client.AspMvc.Rgb.Extensions;
using Sebs.Common.Rgb.Models;

namespace Sebs.Client.AspMvc.Rgb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var data = TempData.Extract<RgbModel>("rgbColorData");
            return View(data ?? null);
        }

        [HttpPost]
        public IActionResult Index(RgbModel model)
        {
            TempData.Insert("rgbColorData", model);

            return RedirectToAction(nameof(Index));
        }
    }
}
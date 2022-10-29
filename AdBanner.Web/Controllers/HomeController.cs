using AdBanner.Data.Model;
using AdBanner.Web.Models;
using AdBanner.Service.BannerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AdBanner.Services.BannerStatService;
using AdBanner.Data.ViewModel;

namespace AdBanner.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBannerService _bannerService;
        private readonly IBannerStatService _bannerStatService;
        public HomeController(ILogger<HomeController> logger, IBannerService bannerService, IBannerStatService bannerStatService)
        {
            _logger = logger;
            _bannerService = bannerService;
            _bannerStatService = bannerStatService;
        }

        public async Task<IActionResult> Index()
        {

            List<BannerViewModel> model = new List<BannerViewModel>();
            model = await _bannerService.FetchAllBanner();
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> BannerClick(int bannerid)
        {
            int result = await _bannerStatService.AddEvent(bannerid, Data.Activity.click);
            if (result > 0)
            {
                return Json(new { result = true });
            }
            return Json(new { result = false }); 
        }

        [HttpPost]
        public async Task<IActionResult> ImpressionView(int bannerid)
        {
            int result = await _bannerStatService.AddEvent(bannerid, Data.Activity.dispaly);
            if (result > 0)
            {
                return Json(new { result = true });
            }
            return Json(new { result = false });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

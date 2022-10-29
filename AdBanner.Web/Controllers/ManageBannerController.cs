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
//using NToastNotify;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace AdBanner.Web.Controllers
{
    public class ManageBannerController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBannerService _bannerService;  
        private readonly INotyfService _toastNotification;
        protected readonly IConfiguration _configuration;      
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ManageBannerController(ILogger<HomeController> logger, IBannerService bannerService, INotyfService toastNotification,
            IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _logger = logger;
            _bannerService = bannerService;
            _toastNotification = toastNotification;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _bannerService.FetchAllBanner();
            return View(model);
        }


        [HttpGet]

        public async Task<IActionResult> AddEdit(int Id)
        {
            try
            {
                var response = new BannerViewModel();  
                if(Id > 0)
                {
                    var model = await _bannerService.FetchBannerById(Id);
                    if(model != null)
                    {
                        return View(model);
                    }

                }
                else
                {
                    return View("AddEdit", response);
                }
                
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occur");
                _toastNotification.Warning("An error occur", null);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit(BannerViewModel model)
        {
            var response = 0;
          
            if (!ModelState.IsValid)
            {        
                _toastNotification.Warning("Invalid fields value", null);
                return View("AddEdit", model);
            }
            if (model.UploadImage != null) 
            {
                string url = await UploadImageAsync(model.UploadImage);
                model.ImageUrl = url;
                
            }
           

            response = await _bannerService.AddEditBanner(model);
            if (response == 0)
            {
                _toastNotification.Warning("Error occured", null);
                return View("AddEdit", model);

            }

            _toastNotification.Success("Added successfully", null);

            ModelState.Clear();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetSummary()
        {
            
            return View(new List<BannerStatsSummaryViewModel>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetSummary(DateTime date)
        {
            if(date != DateTime.MinValue)
            {
                var model = await _bannerService.FetchBannerTrackingPer(date);
                return View(model);
            }
            
            return View(new List<BannerStatsSummaryViewModel>());
        }
        private async Task<string> UploadImageAsync(IFormFile formFile)
        {
           
            var fileName = Guid.NewGuid().ToString();
            FileInfo fi = new FileInfo(formFile.FileName);
            fileName += fi.Extension;

            string UploadPath = _configuration["BannerImage_upload"];
            string fullPath = Directory.GetCurrentDirectory() + UploadPath + fileName;
            string directory = Directory.GetCurrentDirectory() + UploadPath;

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var fileSrteam = new FileStream(fullPath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileSrteam);
            }

            string url = string.Concat(_httpContextAccessor.HttpContext.Request.Scheme, "://", _httpContextAccessor.HttpContext.Request.Host, $"/BannerAds/{fileName}");
            return url;
        }
       

    }
}

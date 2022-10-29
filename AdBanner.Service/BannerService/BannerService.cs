using AdBanner.Data;
using AdBanner.Data.Model;
using AdBanner.Data.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdBanner.Service.BannerServices
{
    public class BannerService : IBannerService
    {
        public readonly AdBannerContext _context;
        private readonly ILogger<BannerService> _logger;
        public BannerService(AdBannerContext context, ILogger<BannerService> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<int> AddEditBanner(BannerViewModel model)
        {
            
            try
            {

                Banner banner = new Banner();
                if (model.Id == 0)
                {
                    banner = new Banner
                    {
                        Id = model.Id,
                        ImageUrl = model.ImageUrl,
                        LinkUrl = model.LinkUrl,
                        Online = model.Online,
                        Title = model.Title,
                    };
                    _context.Add(banner);
                }
                else
                {
                    banner = await _context.Banners.FindAsync(model.Id);
                    
                    if(banner == null)
                    {
                        return 0;
                    }

                    banner.ImageUrl = string.IsNullOrEmpty(model.ImageUrl) ? banner.ImageUrl : model.ImageUrl;
                    banner.LinkUrl = model.LinkUrl;
                    banner.Online = model.Online;
                    banner.Title = model.Title;
                    _context.Update(banner);

                }

                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An exception occur");
                return 0;
            }
        }

        public async Task<List<BannerViewModel>> FetchAllBanner()
        {
            List<BannerViewModel> model = new List<BannerViewModel>();
            try
            {
                model = await _context.Banners.Select(x => new BannerViewModel
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    LinkUrl = x.LinkUrl,
                    Online = x.Online,
                    Title = x.Title,
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occur");
            }
            return model;
        }

        public async Task<BannerViewModel> FetchBannerById(int id)
        {
            BannerViewModel model = new BannerViewModel();
            try
            {
                model = await _context.Banners.Include(p => p.BannerStats).Where(x => x.Id == id).Select(x => new BannerViewModel
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    LinkUrl = x.LinkUrl,
                    Online = x.Online,
                    Title = x.Title,
                    BannerStats = x.BannerStats.Select(p => new BannerStatViewModel {
                        Id = p.Id,                        
                        BannerId = p.FKBanner,
                        Date = p.Date,
                        Event = p.Event.ToString(),
                        Hour = p.Hour
                    }).ToList()
                }).FirstOrDefaultAsync();
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occur");
            }
            return null;
        }

        public async Task<List<BannerStatsSummaryViewModel>> FetchBannerTrackingPer(DateTime date)
        {
            List<BannerStatsSummaryViewModel> model = new List<BannerStatsSummaryViewModel>();
            try
            {
                
                model = await _context.BannerStatsSummaries.Where(p => p.Date.Date == date.Date).Select(x => new BannerStatsSummaryViewModel
                {
                    Title = x.Title,
                    BannerId = x.Id,
                    ClickPerHour = x.ClickPerHour,
                    Hour = x.Hour,
                    ImpressionPerHour = x.ImpressionPerHour
                }).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occur");
            }
            return model;
        }
    }
}

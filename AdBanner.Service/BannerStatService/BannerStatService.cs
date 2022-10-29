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

namespace AdBanner.Services.BannerStatService
{
    public class BannerStatService : IBannerStatService
    {
        public readonly AdBannerContext _context;
        private readonly ILogger<BannerStatService> _logger;
        public BannerStatService(AdBannerContext context, ILogger<BannerStatService> logger)
        {
            _context = context;
            _logger = logger;
        }

        //public async Task<int> AddEdit(BannerStatViewModel model)
        //{
        //    try
        //    {

        //        BannerStat stat = new BannerStat();
        //        if (model.Id == 0)
        //        {
        //            stat = new BannerStat
        //            {
        //                Id = model.Id,                        
        //                Date = DateTime.Now,
        //                Hour = DateTime.Now.Hour,
        //                Event = model.Event
                        
        //                FKBanner = model.BannerId
        //            };
        //            _context.Add(stat);
        //        }
        //        else
        //        {
        //            stat = await _context.BannerStats.FindAsync(model.Id);

        //            if (stat == null)
        //            {
        //                return 0;
        //            }

        //            stat.Clicks = model.Clicks;
        //            stat.Impressions = model.Impressions;
        //            stat.Hour = model.Hour;
        //            stat.Date = model.Date;
        //            stat.Event = model.Event;
        //            _context.Update(stat);

        //        }

        //        return await _context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {

        //        _logger.LogError(ex, "An exception occur");
        //        return 0;
        //    }
        //}

        public async Task<List<BannerStatViewModel>> FetchAllBannerStats(int bannerId)
        {
            List<BannerStatViewModel> model = new List<BannerStatViewModel>();
            try
            {
                model = await _context.BannerStats.Where(x => x.FKBanner == bannerId).Select(x => new BannerStatViewModel
                {
                    Id = x.Id,                    
                    BannerId = x.FKBanner,
                    Date = x.Date,
                    Event = x.Event.ToString(),
                    Hour = x.Hour                    
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occur");
            }
            return model;
        }

      

        public async Task<int> AddEvent(int id, Activity _event)
        {
            try
            {
                BannerStat stat = await _context.BannerStats.FindAsync(id);
                stat = new BannerStat
                {                   
                    Date = DateTime.Now,
                    Event = _event,
                    Hour = DateTime.Now.Hour,
                    FKBanner = id                   
                };

                _context.BannerStats.Add(stat);
            
              
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occur");
                return 0;
            }
        }

        //public async Task<int> IncrementImpression(int id)
        //{
        //    try
        //    {
        //        BannerStat stat = await _context.BannerStats.FindAsync(id);

        //        if (stat == null)
        //        {
        //            return 0;
        //        }

        //        stat.Impressions += 1;
        //        _context.Update(stat);
        //        return _context.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An exception occur");
        //        return 0;
        //    }
        //}
    }
}

using AdBanner.Data;
using AdBanner.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdBanner.Services.BannerStatService
{
    public interface IBannerStatService
    {
        
        Task<int> AddEvent(int id, Activity _event);
        Task<List<BannerStatViewModel>> FetchAllBannerStats(int bannerId);
        //Task<BannerStatViewModel> FetchBannerStatById(int id);
    }
}

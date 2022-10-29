using AdBanner.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdBanner.Service.BannerServices
{
    public interface IBannerService
    {
        Task<int> AddEditBanner(BannerViewModel model);
        Task<List<BannerViewModel>> FetchAllBanner();
        Task<List<BannerStatsSummaryViewModel>> FetchBannerTrackingPer(DateTime date);
        Task<BannerViewModel> FetchBannerById(int id);

    }
}

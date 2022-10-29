using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdBanner.Data.ViewModel
{
    public class BannerStatsSummaryViewModel
    {
        public int BannerId { get; set; }        
        public string Title { get; set; }      
        public int ClickPerHour { get; set; }
        public int ImpressionPerHour { get; set; }
        public int Hour { get; set; }
    }
}

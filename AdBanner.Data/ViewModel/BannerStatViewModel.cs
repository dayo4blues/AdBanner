using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdBanner.Data.ViewModel
{
    public class BannerStatViewModel
    {
        public int Id { get; set; }
        public int BannerId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int Hour { get; set; }
        public int Impressions { get; set; }
        public int Clicks { get; set; }
        public string Event { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdBanner.Data.Model
{
    public class BannerStat
    {
        [Key]
        public int Id { get; set; }        
        public int FKBanner { get; set; }
        public DateTime Date { get; set; }
        public int Hour { get; set; }
        //public int Impressions { get; set; }
        //public int Clicks { get; set; }       
        public Activity Event { get; set; }
        [ForeignKey("FKBanner")]
        public Banner Banner { get; set; }
        
    }
}

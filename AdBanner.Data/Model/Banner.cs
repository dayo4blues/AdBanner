using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdBanner.Data.Model
{
    public class Banner
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(255)]
        public string ImageUrl { get; set; }
        [StringLength(255)]
        public string LinkUrl { get; set; }   
        public bool Online { get; set; }
        public ICollection<BannerStat> BannerStats { get; set; }
    }
}

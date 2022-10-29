using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdBanner.Data.ViewModel
{
    public class BannerViewModel
    {
        public int Id { get; set; }      
        [Required(ErrorMessage ="Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please image is required")]
        public IFormFile UploadImage { get; set; }

        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Link Url is required")]
        public string LinkUrl { get; set; }   
        public bool Online { get; set; }
        public string Viewed { get; set; }
        public string Clicked { get; set; }
        public List<BannerStatViewModel> BannerStats { get; set; }
    }
}

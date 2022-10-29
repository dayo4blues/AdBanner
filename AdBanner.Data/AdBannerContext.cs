using AdBanner.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdBanner.Data
{
    public class AdBannerContext : DbContext
    {
        public AdBannerContext(DbContextOptions<AdBannerContext> options) : base(options)
        {
           
        }

        public DbSet<Banner> Banners { get; set; }
        public DbSet<BannerStat> BannerStats { get; set; }

        //Entity mapping for raw query
        public DbSet<BannerStatsSummary> BannerStatsSummaries { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<BannerStatsSummary>()
                .ToSqlQuery(@"select a.Id, a.Title, b.Hour,CONVERT(date, b.Date) [Date], (Select COUNT(*) from BannerStats where FKBanner = a.Id and Hour = b.Hour and Event = 2) as ClickPerHour,
                (Select COUNT(*) from BannerStats where FKBanner = a.Id and Hour = b.Hour and Event = 1) as ImpressionPerHour  
                from Banners a cross join BannerStats b group by hour,a.Id,Title, CONVERT(date, b.Date)")
                .HasNoKey();
        }
    }
       
        

    
    
}

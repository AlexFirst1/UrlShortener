using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Domain
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<UrlModel> Urls { get; set; }
    }

    //protected override void OnModelCreating(ModelBuilder builder)
    //{
    //    object p = base.OnModelCreating(builder);
    //    builder.Entity<UrlModel>().HasData(new UrlModel
    //    {
    //        Id = 1,
    //        Surl = "fdsgfd",
    //        Url = "dfvgsdg",
    //        Date = DateTime.Now,
    //        Count = 1
    //    });
    //}
}

using Mango.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {

        }

        public DbSet<Coupon> Coupons { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //this we are adding for adding identity in our project
            //When you override the creation you override the creation of the identity creation too.
            //If you have not inherited any IdentityDbContext you don't need it but if you have used
            //Identity then you have to invoke the identity's implementation too.
            //So to invoke the identity's implementation use base.OnModelCreating(builder)
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1,
                CouponCode = "10FF",
                DiscountAmount = 10,
                MinAmount = 20
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 2,
                CouponCode = "20FF",
                DiscountAmount = 20,
                MinAmount = 40
            });


        }
    }
}


using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HelpingHands_DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyImage> CompanyImages { get; set; }
        public DbSet<CompanyXAmenity> CompanyXAmenities { get; set; }
        public DbSet<CompanyXPayment> CompanyXPayments { get; set; }
        public DbSet<CompanyXService> CompanyXServices { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Enquiry> Enquiries { get; set; }
        public DbSet<FirstCategory> FirstCategories { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewXComment> ReviewXComments { get; set; }
        public DbSet<SecondCategory> SecondCategories { get; set; }

        public DbSet<Service> Services { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<ThirdCategory> ThirdCategories { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }

    }
}

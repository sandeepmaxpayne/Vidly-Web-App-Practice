using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    using System.Data.Entity;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    namespace Vidly.Models
    {
        // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
        public class ApplicationUser : IdentityUser
        {
            public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
            {
                // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
                var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
                // Add custom user claims here
                return userIdentity;
            }
        }

        public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
        {

            /// <summary>
            /// create customer Db Set
            /// </summary>
            public DbSet<Customer> Customers { get; set; }

            //Movies DbSet
            public DbSet<Movie> Movies { get; set; }

            public DbSet<MembershipType> MembershipTypes { get; set; }

            public DbSet<Genre> Genres { get; set; }

            public ApplicationDbContext()
                : base("DefaultConnection", throwIfV1Schema: false)
            {
            }

            public static ApplicationDbContext Create()
            {
                return new ApplicationDbContext();
            }
            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                // Using fluentAPI
                modelBuilder.Entity<Customer>()
                    .HasKey(c => c.CustomerId)
                    .Property(c => c.CustomerName)
                    .IsRequired()
                    .HasMaxLength(255);

                // Make Name required in MembershipType
                modelBuilder.Entity<MembershipType>()
                    .Property(c => c.Name)
                    .IsRequired();

                //Movies table
                modelBuilder.Entity<Movie>()
                    .HasKey(m => m.Id)
                    .Property(m => m.Name)
                    .IsRequired()
                    .HasMaxLength(255);
                

                modelBuilder.Entity<Genre>()
                    .HasKey(g => g.Id)
                    .Property(g => g.Name)
                    .HasMaxLength(255)
                    .IsRequired();


                base.OnModelCreating(modelBuilder);


            }
        }
    }
}
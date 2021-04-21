using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
   public  class DataContext : IdentityDbContext// DbContext

    {
       
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            //this.Users is of type DbSet<IdentityUser>
            
           // var user = this.Users.First();//user is of type IdentityUser
            //var tp = user.GetType();
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Owner>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Entity<PortofolioItem>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Entity<Address>().Property(x => x.Id).HasDefaultValueSql("NEWID()");


            Address address = new Address { Id = Guid.NewGuid(), City = "Cairo", Country = "Egypt", Number = 1, Street = "Ibrahim Bik Al Kabeer" };
            builder.Entity<Address>().HasData(address);
            ///
            Owner Me = new Owner
            {
                Id = Guid.NewGuid(),
                FullName = "go to Home/Index to login",
                Avatar = "dafault.jpg",
                Job = "Dashboard/Create(Edit)Profile - Dashboard/ShowPortofolioItems - Portofolio/Index/Guid? ",
                AddressId=address.Id  
                

            };
            builder.Entity<Owner>().HasData(Me);
            
            ///
            PortofolioItem item = new PortofolioItem
            {
                Id = Guid.NewGuid(),
                Name = "portofolio1",
                Description = "description of portrofolio1",
                ImageUrl = "portofolio1.jpg",
                OwnerId=Me.Id
            };
            builder.Entity<PortofolioItem>().HasData(item);
            //here we cant do this.SaveChanges ()  and cant add element to any DbSet, so we generate the Key Values in c# and put then in the FK values as you see

        }

        public DbSet<Owner> Owner { get; set; }
        public DbSet<PortofolioItem> PortofolioItem { get; set; }
        public DbSet<Address> Address { get; set; }
    }
}

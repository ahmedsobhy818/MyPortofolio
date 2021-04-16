using System;
using System.Collections.Generic;
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
            
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Owner>().Property(x => x.Id).HasDefaultValue(Guid.NewGuid());
            builder.Entity<PortofolioItem>().Property(x => x.Id).HasDefaultValue(Guid.NewGuid());
            builder.Entity<Address>().Property(x => x.Id).HasDefaultValue(Guid.NewGuid());


            Address address = new Address { Id = Guid.NewGuid(), City = "Cairo", Country = "Egypt", Number = 1, Street = "Ibrahim Bik Al Kabeer" };
            builder.Entity<Address>().HasData(address);
            ///
            Owner Me = new Owner
            {
                Id = Guid.NewGuid(),
                FullName = "Ahmed Sobhy",
                Avatar = "avatar.jpg",
                Job = ".Net Full Stack Developer",
                AddressId=address.Id    

            };
            builder.Entity<Owner>().HasData(Me);
            
            ///
            PortofolioItem item = new PortofolioItem
            {
                Id = Guid.NewGuid(),
                Name = "Asp.net web development",
                Description = "responsive website using latest microsof technologies",
                ImageUrl = "portofolio1.jpg",
                OwnerId=Me.Id
            };
            builder.Entity<PortofolioItem>().HasData(item);
            //
            
            
        }
        
        public DbSet<Owner> Owner { get; set; }
        public DbSet<PortofolioItem> PortofolioItem { get; set; }
        public DbSet<Address> Address { get; set; }
    }
}

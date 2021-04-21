using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
   public class Owner : EntityBase
    {
        public string FullName { get; set; }
        public string Job { get; set; }
        public string Avatar { get; set; }
        public Address Address { get; set; }

        [ForeignKey("Address")]
        public System.Guid AddressId { get; set; } //only to use in  data seed code , as we cant use the "Address" objeect in that code

        public virtual ICollection<PortofolioItem> PortofolioItems { get; set; }

        //public IdentityUser User { get; set; } //i commented it because i did not want to install asp.net core identity classes into the Core Project
        
        //[ForeignKey("User")] //i commented it because i dont have User object as i said in the prervious line
        
        //so i just need UserId to get the owner related to autheneticated user . i  dont need to refer to Owner.User
        public System.Guid? UserId { get; set; }

    }
}

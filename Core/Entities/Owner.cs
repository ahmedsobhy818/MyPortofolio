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

    }
}

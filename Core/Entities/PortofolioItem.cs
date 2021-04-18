using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class PortofolioItem : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

       //
        public Owner Owner { get; set; }

        [ForeignKey("Owner")]
        public System.Guid OwnerId { get; set; }//only to use in  data seed code , as we cant use the "Owner" objeect in that code
        //
        
    }
}

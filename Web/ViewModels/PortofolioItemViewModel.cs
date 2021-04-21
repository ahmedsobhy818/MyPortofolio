using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Web.ViewModels
{
    public class PortofolioItemViewModel
    {
        public Guid ItemId { get; set; }
        public Guid OwnerId { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 5)]
        public string Description { get; set; }

        public IFormFile ImageUploader { get; set; }

        public string ImageFile { get; set; }
    }
}

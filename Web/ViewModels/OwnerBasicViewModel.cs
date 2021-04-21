using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Web.ViewModels
{
    public class OwnerBasicViewModel
    {
        public Guid OwnerID { get; set; }
        public Guid AddressId { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 5)]
        public string FullName { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 5)]
        public string Job { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string Country { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string City { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string Street { get; set; }

        [Required]
        public int Number { get; set; }
        public IFormFile AvatarUploader { get; set; }

        public string Avatar { get; set; }

    }
}

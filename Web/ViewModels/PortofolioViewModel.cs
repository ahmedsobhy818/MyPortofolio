using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Web.ViewModels
{
    public class PortofolioViewModel
    {
        public Owner Owner { get; set; }
        public IList<PortofolioItem> portofolioItems { get; set; }

        public string AppTitle { get; set; }
    }
}

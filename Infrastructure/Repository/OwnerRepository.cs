using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Base_Classes;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class OwnerRepository : GenericRepository<Owner>
    {
        public OwnerRepository(DataContext context) : base(context)
        {

        }
        //custom  function can not be placed into the GenericRepository class
        public Owner GetFirstOwnerWithAddress()
        {
            return GetAllAsQueryable().Include(c => c.Address).First();
        }
    }
}

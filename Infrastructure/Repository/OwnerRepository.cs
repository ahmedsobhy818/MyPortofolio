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
        public Owner GetDefaultOwner()
        {
            return GetAllAsQueryable().Include(c => c.Address).Include(c => c.PortofolioItems).FirstOrDefault(c => c.UserId == null);
        }

        internal Owner DownloadOwnerWithFullDetails(Guid? id)
        {
           return  (id!=null?   GetAllAsQueryable().Include(c => c.Address).Include(c => c.PortofolioItems).FirstOrDefault(c=>c.Id==id):null) ;
        }

      

        internal object DownloadOwnerByUserIDWithFullDetails(Guid? userid)
        {
            return (userid != null ? GetAllAsQueryable().Include(c => c.Address).Include(c => c.PortofolioItems).FirstOrDefault(c => c.UserId == userid) : null);
        }
    }
}

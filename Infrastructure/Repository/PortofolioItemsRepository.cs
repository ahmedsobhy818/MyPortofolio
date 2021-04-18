using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Base_Classes;
using Core.Entities;

namespace Infrastructure.Repository
{
    public class PortofolioItemsRepository : GenericRepository<PortofolioItem>
    {
        public PortofolioItemsRepository(DataContext context):base(context)
        {
            
        }
        //custom filter function can not be placed into the GenericRepository class
        public IList<PortofolioItem> GetAllPortofolioItemsForOwner(Guid ownerID)
        {
            return GetAllAsQueryable().Where(c => c.OwnerId == ownerID).ToList();
        }
    }
}

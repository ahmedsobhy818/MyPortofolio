using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using Core.Base_Classes;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitOfWork.MyUOW
{
    public class PortofolioUOW : UnitOfWork<PortofolioUOW>   // IUnitOfWork<PortofolioUOW> 
    {
        private readonly DataContext _context;
        public PortofolioUOW(DataContext context) :base(context)
        {
            _context = context;
        }
       

        OwnerRepository _OwnerRepository = null;
        PortofolioItemsRepository _PortofolioItemRepository = null;
        

        public OwnerRepository OwnerRepository
        {
            get
            {
                _OwnerRepository = _OwnerRepository ?? new OwnerRepository(_context);
                return _OwnerRepository;
            }    
        }
        public PortofolioItemsRepository PortofolioItemRepository
        {
            get
            {
                _PortofolioItemRepository = _PortofolioItemRepository ?? new PortofolioItemsRepository(_context);
                return _PortofolioItemRepository;
            }
        }


    }
}

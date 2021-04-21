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
    public class PortofolioUOW : UnitOfWork<PortofolioUOW>   
    {
        private readonly DataContext _context;
        public PortofolioUOW(DataContext context) :base(context)
        {
            _context = context;
        }


        GenericRepository<Address> _AddressRepository = null;
        OwnerRepository _OwnerRepository = null;
        PortofolioItemsRepository _PortofolioItemRepository = null;
        
        public GenericRepository<Address> AddressRepository
        {
            get
            {
                _AddressRepository = _AddressRepository ?? new GenericRepository<Address>(_context);
                return _AddressRepository;
            }
        }

        public Owner GetPortofolio(Guid? id)
        {
            var o = OwnerRepository.GetById(id);
            var owner =  OwnerRepository.DownloadOwnerWithFullDetails(id);
            return owner;
        }
        public Owner GetPortofolioByUser(Guid? userid)
        {
            var owner = OwnerRepository.DownloadOwnerByUserIDWithFullDetails(userid);
            return owner as Owner;
        }
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
        public void CreateOwner(Owner owner)
        {
            AddressRepository.Insert(owner.Address);
            OwnerRepository.Insert(owner);
            Save();
        }

        public void UpdateOwner(Owner owner)
        {
            AddressRepository.Update(owner.Address);
            OwnerRepository.Update(owner);
            Save();
        }

        public PortofolioItem GetPortofolioItem(Guid? id)
        {
            var item = PortofolioItemRepository.GetById(id);
            if (item.Owner == null)
                GetPortofolio(item.OwnerId);
            return item;
        }

        public void AddPortofolioItem(PortofolioItem item, string userId)
        {
            Owner owner = GetPortofolioByUser(new Guid(userId));
            item.Owner = owner;
            PortofolioItemRepository.Insert(item);
            Save();
        }

        public void UpdatePortofolioItem(PortofolioItem item)
        {
            PortofolioItemRepository.Update(item);
            Save();
        }
    }
}

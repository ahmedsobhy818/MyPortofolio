using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Base_Classes;
using Core.Entities;
using Core.Interfaces;
using Infrastructure;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Infrastructure.UnitOfWork.MyUOW;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class PortofolioController : Controller
    {
        private readonly PortofolioUOW _uow;

        public PortofolioController(IUnitOfWork<PortofolioUOW> uow,IUnitOfWork<TestUOW> obj /*IUnitOfWork uow*/)
        {
            _uow = uow as PortofolioUOW;
        }
        public IActionResult Index(Guid? id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Owner Owner = null;

            if (id != null)
            {
                Owner = _uow.GetPortofolio(id);
            }
            else
            {
                if (userId != "" && userId!=null)
                {
                    Owner = _uow.GetPortofolioByUser(new Guid(userId));
                }
            }
            if (Owner == null)
                Owner = _uow.OwnerRepository.GetDefaultOwner();//.GetFirstOwnerWithAddress();

            var portofolioItems = _uow.PortofolioItemRepository.GetAllPortofolioItemsForOwner(Owner.Id);
            
            var ViewModel = new PortofolioViewModel
            {
               Owner = Owner,
              portofolioItems=portofolioItems,
              AppTitle = "My Portofolio"
            };
            
            return View(ViewModel);
        }
        public IActionResult About()//test function
        {
            
            
            var addr = new Address
            {
                City="London",
                Country="UK",
                Street="ABC",
                Number=818
            };
            
            var owner = new Owner
            {
                FullName = "Sayed Ali",
                Job = "Accountant",
                Avatar = "sayed.jpg",
                Address = addr,

            };
            
            var item = new PortofolioItem
            {
                Name="Calculations",
                Description="Full report of company",
                ImageUrl="portofolio5.jpg",
                Owner=owner
            };

            _uow.AddressRepository.Insert(addr);
            _uow.OwnerRepository.Insert(owner);
            _uow.PortofolioItemRepository.Insert(item);
            _uow.Save();
            return View();
        }
    }
}

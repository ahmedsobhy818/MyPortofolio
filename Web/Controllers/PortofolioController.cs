using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult Index()
        {
            var Owner = _uow.OwnerRepository.GetFirstOwnerWithAddress();
            var portofolioItems = _uow.PortofolioItemRepository.GetAllPortofolioItemsForOwner(Owner.Id);
            
            var ViewModel = new PortofolioViewModel
            {
               Owner = Owner,
              portofolioItems=portofolioItems,
              AppTitle = "My Portofolio"
            };
            
            return View(ViewModel);
        }
        public IActionResult About()
        {
            return View();
        }
    }
}

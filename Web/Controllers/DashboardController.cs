using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.UnitOfWork.MyUOW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IHostingEnvironment hosting;
        private readonly PortofolioUOW uow;
        public DashboardController(IHostingEnvironment hosting, IUnitOfWork<PortofolioUOW> uow)
        {
            this.hosting = hosting;
            this.uow = uow as PortofolioUOW;
        }

        // GET: DashboardController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: Dashboard/PortofolioItemDetails/xxxxxx
        public ActionResult PortofolioItemDetails(Guid? id)
        {
            var item = uow.GetPortofolioItem(id);
            var vm = new PortofolioItemViewModel
            {
                Description = item.Description,
                ImageFile = item.ImageUrl,
                ItemId = item.Id,
                Name = item.Name,
                OwnerId = item.OwnerId
            };
            return View(vm);

        }

        // GET: Dashboard/CreateProfile
        public ActionResult CreateProfile()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = uow.GetPortofolioByUser(new Guid(userId));
            if (owner != null)
            {
                return RedirectToAction("EditProfile");
            }
            return View();
        }

        // POST: Dashboard/CreateProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProfile(OwnerBasicViewModel model)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                string FileName = "";
                if (model.AvatarUploader != null)
                {
                    FileName = model.AvatarUploader.FileName.Split(@"\", StringSplitOptions.None).Last();
                    string uploadFolder = Path.Combine(hosting.WebRootPath, "img");
                    string FullPath = Path.Combine(uploadFolder, FileName);
                    model.AvatarUploader.CopyTo(new FileStream(FullPath, FileMode.Create));

                }
                else
                {
                    ModelState.AddModelError("upload", "Please upload an image");
                }
                if (!ModelState.IsValid)
                {

                    ModelState.AddModelError("", "please fix all errors");
                    return View(model);//return data as it is 
                }

                ///
                var addr = new Address
                {
                    City = model.City,
                    Country = model.Country,
                    Street = model.Street,
                    Number = model.Number
                };
                var owner = new Owner
                {
                    FullName = model.FullName,
                    Job = model.Job,
                    Avatar = FileName,
                    UserId = new Guid(userId),
                    Address = addr
                };
                uow.CreateOwner(owner);
                return Redirect("/Portofolio/Index/" + owner.Id);
                //return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        // GET: Dashboard/EditProfile
        public ActionResult EditProfile()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = uow.GetPortofolioByUser(new Guid(userId));
            if (owner == null)
            {
                //goto create
                return RedirectToAction("CreateProfile");
            }
            var model = new OwnerBasicViewModel
            {
                OwnerID = owner.Id,
                FullName = owner.FullName,
                Job = owner.Job,
                Avatar = owner.Avatar,
                Country = owner.Address.Country,
                City = owner.Address.City,
                Street = owner.Address.Street,
                Number = owner.Address.Number,
                AddressId = owner.AddressId


            };
            return View(model);
        }

        // POST: Dashboard/EditProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(OwnerBasicViewModel model)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                string FileName = model.Avatar;
                if (model.AvatarUploader != null)
                {
                    FileName = model.AvatarUploader.FileName.Split(@"\", StringSplitOptions.None).Last();
                    string uploadFolder = Path.Combine(hosting.WebRootPath, "img");
                    string FullPath = Path.Combine(uploadFolder, FileName);
                    model.AvatarUploader.CopyTo(new FileStream(FullPath, FileMode.Create));

                }
                else
                {
                    if (model.Avatar == "")
                        ModelState.AddModelError("upload", "Please upload an image");
                }

                if (!ModelState.IsValid)
                {

                    ModelState.AddModelError("", "please fix all errors");
                    return View(model);//return data as it is 
                }

                ///
                var addr = new Address
                {
                    Id = model.AddressId,
                    City = model.City,
                    Country = model.Country,
                    Street = model.Street,
                    Number = model.Number
                };
                var owner = new Owner
                {
                    Id = model.OwnerID,
                    FullName = model.FullName,
                    Job = model.Job,
                    Avatar = FileName,
                    UserId = new Guid(userId),
                    Address = addr
                };
                uow.UpdateOwner(owner);
                return Redirect("/Portofolio/Index/" + owner.Id);


            }
            catch
            {
                return View(model);
            }
        }

        // GET: Dashboard/DeletePortofolioIteme/xxxxxx
        public ActionResult DeletePortofolioIteme(Guid? id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var item = uow.GetPortofolioItem(id);
            var vm = new PortofolioItemViewModel
            {
                Description = item.Description,
                ImageFile = item.ImageUrl,
                ItemId = item.Id,
                Name = item.Name,
                OwnerId = item.OwnerId
            };
            return View(vm);
        }

        // POST: Dashboard/DeletePortofolioIteme/xxxxxxx
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePortofolioIteme(PortofolioItemViewModel model)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userId2 = uow.GetPortofolioItem(model.ItemId).Owner.UserId;
                if (userId.ToString() != userId2.ToString())
                    return RedirectToAction(nameof(ShowPortofolioItems));
                uow.PortofolioItemRepository.Delete(model.ItemId);
                uow.Save();
                return RedirectToAction(nameof(ShowPortofolioItems));
            }
            catch
            {
                return View(model);
            }
        }


        public ActionResult ShowPortofolioItems()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = uow.GetPortofolioByUser(new Guid(userId));
            if (owner == null)
                return RedirectToAction("CreateProfile");
            IEnumerable<PortofolioItemViewModel> vmList = owner.PortofolioItems.Select(item => new PortofolioItemViewModel
            {
                Name = item.Name,
                Description = item.Description,
                ImageFile = item.ImageUrl,
                ItemId = item.Id,
                OwnerId = item.Owner.Id
            });

            return View(vmList);
        }

        // GET: Dashboard/CreatePortofolioItem
        public ActionResult CreatePortofolioItem()
        {
            return View();
        }

        // POST: Dashboard/CreatePortofolioItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePortofolioItem(PortofolioItemViewModel model)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                string FileName = "";
                if (model.ImageUploader != null)
                {
                    FileName = model.ImageUploader.FileName.Split(@"\", StringSplitOptions.None).Last();
                    string uploadFolder = Path.Combine(hosting.WebRootPath, @"img\portfolio");
                    string FullPath = Path.Combine(uploadFolder, FileName);
                    model.ImageUploader.CopyTo(new FileStream(FullPath, FileMode.Create));

                }
                else
                {
                    ModelState.AddModelError("upload", "Please upload an image");
                }
                if (!ModelState.IsValid)
                {

                    ModelState.AddModelError("", "please fix all errors");
                    return View(model);//return data as it is 
                }
                var item = new PortofolioItem
                {
                    Name = model.Name,
                    Description = model.Description,
                    ImageUrl = FileName
                };

                uow.AddPortofolioItem(item, userId);
                return RedirectToAction("ShowPortofolioItems");
            }
            catch(Exception ex)
            {
                return View(model);
            }
        }

        /////////////////////////
        // GET: Dashboard/EditPortofolioItem
        public ActionResult EditPortofolioItem(Guid? id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var item = uow.GetPortofolioItem(id);
            var model = new PortofolioItemViewModel
            {
                Description=item.Description,
                Name=item.Name,
                ImageFile=item.ImageUrl,
                ItemId=item.Id,
                OwnerId=item.OwnerId
            };
            return View(model);
        }

        // POST: Dashboard/EditPortofolioItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPortofolioItem(PortofolioItemViewModel model)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                string FileName = model.ImageFile;
                if (model.ImageUploader != null)
                {
                    FileName = model.ImageUploader.FileName.Split(@"\", StringSplitOptions.None).Last();
                    string uploadFolder = Path.Combine(hosting.WebRootPath, @"img\portfolio");
                    string FullPath = Path.Combine(uploadFolder, FileName);
                    model.ImageUploader.CopyTo(new FileStream(FullPath, FileMode.Create));

                }
                else
                {
                    if (model.ImageFile == "")
                        ModelState.AddModelError("upload", "Please upload an image");
                }

                if (!ModelState.IsValid)
                {

                    ModelState.AddModelError("", "please fix all errors");
                    return View(model);//return data as it is 
                }

                ///
                var item = new PortofolioItem
                {
                    Description=model.Description,
                    Name=model.Name,
                    ImageUrl=FileName,
                    Id=model.ItemId,
                    OwnerId=model.OwnerId
                    
                };

                uow.UpdatePortofolioItem(item);
                return RedirectToAction("ShowPortofolioItems");


            }
            catch(Exception ex)
            {
                return View(model);
            }
        }

    }
}

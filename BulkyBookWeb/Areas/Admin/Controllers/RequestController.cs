using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    public class RequestController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public RequestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Home()
        {

            return View();
        }


        public IActionResult Upsert(int? id)
        {
            RequestVM requestVM = new()
            {
                Request = new(),
                RequirementList = _unitOfWork.Requirement.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                }),

            };

            if (id == null || id == 0)
            {
                return View(requestVM);
            }
            else
            {
                requestVM.Request = _unitOfWork.Request.GetFirstOrDefault(u => u.Id == id);
                return View(requestVM);

            }
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(RequestVM obj)
        { 
            if (ModelState.IsValid)
            {
                if (obj.Request.Id == 0)
                {
                    _unitOfWork.Request.Add(obj.Request);
                    TempData["success"] = "Request created successfully";
                }
                else
                {
                    _unitOfWork.Request.Update(obj.Request);
                    TempData["success"] = "Request updated successfully";
                }
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        #region API CALLS
        [HttpGet]

        public IActionResult GetAll()
        {
            var requestList = _unitOfWork.Request.GetAll(includeProperties: "Requirement");
            return Json(new { data = requestList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Request.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Request.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Succesful" });

        }
        #endregion

    }
}

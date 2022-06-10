using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Security.Claims;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Support, Employee")]
    public class AssignationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public AssignationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public IActionResult Index()
        {
            string role = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList().FirstOrDefault() ?? "";
            return View((object)role);
        }


        public IActionResult Upsert(int? id)
        {
            AssignationVM assignationVM = new()
            {
                Assignation = new(),
                ApplicationUserList = _unitOfWork.ApplicationUser.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                RequestList = _unitOfWork.Request.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Description,
                    Value = i.Id.ToString()
                }),



            };

            if (id == null || id == 0)
            {
                return View(assignationVM);
            }
            else
            {
                assignationVM.Assignation = _unitOfWork.Assignation.GetFirstOrDefault(u => u.Id == id);
                return View(assignationVM);

            }
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(AssignationVM obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Assignation.Id == 0)
                {
                    _unitOfWork.Assignation.Add(obj.Assignation);
                    TempData["success"] = "Request created successfully";
                }
                else
                {
                    _unitOfWork.Assignation.Update(obj.Assignation);
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
            var assignationList = _unitOfWork.Assignation.GetAll(includeProperties: "Request,ApplicationUser");
            return Json(new { data = assignationList });
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

using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Support, Employee")]
    public class RequirementController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public RequirementController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Requirement> objRequirementList = _unitOfWork.Requirement.GetAll();
            return View(objRequirementList);
        }
        //GET
        public IActionResult Create()
        {   
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Requirement obj)
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.Requirement.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Requerimiento creado exitosamente";
                return RedirectToAction("Index");
            }
            return View(obj);   
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var RequirementFromDbFirst = _unitOfWork.Requirement.GetFirstOrDefault(u => u.Id == id);

            if(RequirementFromDbFirst == null)
                {
                    return NotFound();
                } 
            return View(RequirementFromDbFirst);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Requirement obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Requirement.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Requerimiento actualizado correctamente";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var RequirementFromDbFirst = _unitOfWork.Requirement.GetFirstOrDefault(u => u.Id == id);
            if (RequirementFromDbFirst == null)
            {
                return NotFound();
            }
            return View(RequirementFromDbFirst);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Requirement.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Requirement.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Requerimiento borrado exitosamente";
            return RedirectToAction("Index");
            
            return View(obj);
        }
    }
}

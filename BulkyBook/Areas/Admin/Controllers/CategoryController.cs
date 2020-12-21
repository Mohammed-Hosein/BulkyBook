using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area ("Admin")]
    [Authorize (Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            if (id == null)
            {
                //this is for create 
                return View(category);
            }
            // this is for edit
            category = _UnitOfWork.Category.Get(id.GetValueOrDefault());
            if (category == null)
            {
                return NotFound();
            }
            else { 
            return View(category);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if(category.Id == 0)
                {
                    _UnitOfWork.Category.Add(category);
                    _UnitOfWork.Save();
                }
                else
                {
                    _UnitOfWork.Category.Update(category);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        #region API Calss
        [HttpGet]
        public IActionResult GetAll()
        {
            var allobj = _UnitOfWork.Category.GetAll();
            return Json(new{data=allobj} );

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var allobj = _UnitOfWork.Category.Get(id);
            if (allobj == null) { 
            return Json(new { success = false , message ="Error while deleting"});
            }
            else
            {
                _UnitOfWork.Category.Remove(id);
                _UnitOfWork.Save();
                return Json(new { success = true, message = "Deleting Succesful" });
            }

        }

        #endregion
    }
}

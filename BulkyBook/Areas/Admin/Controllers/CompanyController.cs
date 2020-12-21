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
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employye)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Company company = new Company();
            if (id == null)
            {
                //this is for create 
                return View(company);
            }
            // this is for edit
            company = _UnitOfWork.Company.Get(id.GetValueOrDefault());
            if (company == null)
            {
                return NotFound();
            }
            else
            {
                return View(company);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                {
                    _UnitOfWork.Company.Add(company);
                    _UnitOfWork.Save();
                }
                else
                {
                    _UnitOfWork.Company.Update(company);
                }
                _UnitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        #region API Calss
        [HttpGet]
        public IActionResult GetAll()
        {
            var allobj = _UnitOfWork.Company.GetAll();
            return Json(new { data = allobj });

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var allobj = _UnitOfWork.Company.Get(id);
            if (allobj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            else
            {
                _UnitOfWork.Company.Remove(id);
                _UnitOfWork.Save();
                return Json(new { success = true, message = "Deleting Succesful" });
            }

        }

        #endregion
    }
}


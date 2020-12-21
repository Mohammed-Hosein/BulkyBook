using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            CoverType CoverType = new CoverType();
            if (id == null)
            {
                //this is for create 
                return View(CoverType);
            }
            // this is for edit
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            CoverType = _UnitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get, parameter);
            if (CoverType == null)
            {
                return NotFound();
            }
            else
            {
                return View(CoverType);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Name", coverType.Name);
                if (coverType.Id == 0)
                {
                    _UnitOfWork.SP_Call.Execute(SD.Proc_CoverType_Create,parameter);
                    _UnitOfWork.Save();
                }
                else
                {
                    parameter.Add("@Id", coverType.Id);
                    _UnitOfWork.SP_Call.Execute(SD.Proc_CoverType_Update,parameter);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(coverType);
        }

        #region API Calss
        [HttpGet]
        public IActionResult GetAll()
        {
            var allobj = _UnitOfWork.SP_Call.List<CoverType>(SD.Proc_CoverType_GetAll, null);
            return Json(new { data = allobj });

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            var allobj = _UnitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get, parameter);
            if (allobj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            else
            {
                _UnitOfWork.SP_Call.Execute(SD.Proc_CoverType_Delete,parameter);
                _UnitOfWork.Save();
                return Json(new { success = true, message = "Deleting Succesful" });
            }

        }

        #endregion
    }
}

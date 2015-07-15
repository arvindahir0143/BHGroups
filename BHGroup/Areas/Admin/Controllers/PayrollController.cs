using BHGroup.Areas.Admin.ViewModels;
using BHGroupBAL;
using BHGroupEntity;
using BHGroupEntity.Classes;
using Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace BHGroup.Areas.Admin.Controllers
{
    [Authorize]
    public class PayrollController : Controller
    {
        //
        // GET: /Payroll/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult manage(int? id)
        {
            MemberPayroll model = new MemberPayroll();
            ViewBag.MemberLookUp = new MemberBAL().GetMemberLookUp();
            if (id != 0 && id != null)
            {
                model = new PayrollBAL().GetById(id.Value);

            }

            return View(model);
        }
        public ActionResult Save(MemberPayroll model)
        {
            try
            {
                bool Add_Flag = new PayrollBAL().isNewEntry(model.PaymentId);


                model.Status = "Active";
                if (Add_Flag)
                {
                    model.CreatOn = DateTime.Now;
                    model.ModifidOn = DateTime.Now;
                    new PayrollBAL().Create(model);
                }
                else
                {
                    model.ModifidOn = DateTime.Now;
                    new PayrollBAL().Update(model);
                }
                string message = CommonMsg.Success(EntityNames.Member, Add_Flag == true ? En_CRUD.Insert : En_CRUD.Update);
                TempData["successmsg"] = message;
            }
            catch (Exception ex)
            {
                TempData["errormsg"] = ex.Message;
            }
            return Redirect("Index");

        }

        public JsonResult Delete(int id = 0)
        {
            try
            {
                new PayrollBAL().Delete(id);
                return Json(new { success = true, message = CommonMsg.Success(EntityNames.Member, En_CRUD.Delete) });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = CommonMsg.Fail(EntityNames.Member, En_CRUD.Delete) });
            }
        }
        public JsonResult ChangeStatus(int id = 0)
        {
            try
            {
                new PayrollBAL().Updatestatus(id);
                return Json(new { success = true, message = "Status Changed Sucessfully" });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Status Changed Failed" });
            }
        }
        public string GetGridData(GridSettings grid)
        {
            try
            {
                return new PayrollBAL().GetGridData(grid,null);
            }
            catch (Exception ex)
            {
                return "";
            }
        }



    }
}

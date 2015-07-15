using BHGroup.Areas.Admin.ViewModels;
using BHGroupBAL;
using BHGroupEntity;
using BHGroupEntity.Classes;
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
    public class ReportsController : Controller
    {
        //
        // GET: /Admin/Reports/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Member()
        {
            ViewBag.MembertypeLookUp = new MemberBAL().GetMembersLookup();
            return View();
        }
        public ActionResult Franchies()
        {
            ViewBag.FranchiestypeLookUp = new MemberBAL().GetFranchiesLookup();
            return View();
        }
        public ActionResult Broker()
        {
            ViewBag.BrokertypeLookUp = new MemberBAL().GetBrokerLookup();
            return View();
        }
        public ActionResult Distributers()
        {
            ViewBag.DistributerstypeLookUp = new MemberBAL().GetDistributorsLookup();
            return View();
        }
        public string GetGridDataReports(GridSettings grid, int id, string type)
        {
            try
            {

                return new MemberBAL().GetGridDataReports(grid, type, id);
            }
            catch (Exception ex)
            {
                return "";
            }
        }



    }
}

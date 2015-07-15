using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BHGroup.Areas.Admin.ViewModels;
using WebMatrix.WebData;
using BHGroupBAL;
using BHGroupEntity;
using BHGroupEntity.Classes;

namespace BHGroup.Controllers
{



    [Authorize]
    public class MemberController : Controller
    {

        MemberBAL _MemberBAL = new MemberBAL();

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (HttpContext.Session != null)
                        HttpContext.Session.Abandon();


                    var Member = _MemberBAL.GetMember(model.MemberShipNo);

                    if (Member != null)
                    {
                        if (ModelState.IsValid && (model.Password == Member.Password))
                        {
                            //Session["MembershipNo"] = Member.MemberShipNo;
                            FormsAuthentication.SetAuthCookie(Member.Name.ToString(), false);
                            FormsAuthenticationTicket formsAuthenticationTicket = new FormsAuthenticationTicket(Member.Name.ToString(), false, 240);
                            HttpCookie httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(formsAuthenticationTicket));
                            Response.Cookies.Add(httpCookie);

                            if (Request.QueryString["ReturnUrl"] == null)
                                return RedirectToAction("Index", "MemberDashboard");
                            else
                                return Redirect(Request.QueryString["ReturnUrl"]);
                        }
                        else
                        {

                            TempData["errormsg"] = "The email or password provided is incorrect";

                        }
                    }
                    else
                    {

                        TempData["errormsg"] = "Member Does Not Exist";


                    }

                }



            }
            catch (Exception ex)
            {

                TempData["errormsg"] = ex.Message;
            }
            return View(model);

        }

        public ActionResult SignOut()
        {

            try
            {
                FormsAuthentication.SignOut();
            }
            catch (Exception)
            {

                throw;
            }
            return Redirect("Login");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ChangePassword(LoginModel model)
        {

            try
            {
                //var Member = _MemberBAL.GetByName(Convert.ToInt32(Session["MembershipNo"]));
                var Member = _MemberBAL.GetByName(User.Identity.Name);
                Member.Password = model.Password;
                new MemberBAL().Update(Member);


                TempData["successmsg"] = "Password Changed Succesfully";


            }
            catch (Exception ex)
            {

                TempData["errormsg"] = ex.Message;
            }
            return Redirect("ChangePassword");
        }

        public string GetGridData(GridSettings grid)
        {
            try
            {
                // var Member = _MemberBAL.GetByName(Convert.ToInt32(Session["MembershipNo"]));
                var Member = _MemberBAL.GetByName(User.Identity.Name);
                return new PayrollBAL().GetGridData(grid, Member.MemberId);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Payment()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult EditMember()
        {
            Member model = new Member();
            // var Member = _MemberBAL.GetByName(Convert.ToInt32(Session["MembershipNo"].ToString()));
            var Member = _MemberBAL.GetByName(User.Identity.Name);

            ViewBag.MemberLookUp = new MemberBAL().GetMemberLookUp();

            model = new MemberBAL().GetById(Member.MemberId);

            return View(model);

        }

        public ActionResult Save(Member model)
        {
            try
            {

                new MemberBAL().Update(model);

                TempData["successmsg"] = "Profile Updated Successfully";
            }
            catch (Exception ex)
            {
                TempData["errormsg"] = ex.Message;
            }
            return Redirect("EditMember");

        }



        public ActionResult Reports()
        {


            return View();
        }

        public string GetGridDataReports(GridSettings grid)
        {
            try
            {
                //var Member = _MemberBAL.GetByName(Convert.ToInt32(Session["MembershipNo"].ToString()));
                var Member = _MemberBAL.GetByName(User.Identity.Name);

                return new MemberBAL().GetGridDataReports(grid, Member.MemberType, Member.ParentId);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}

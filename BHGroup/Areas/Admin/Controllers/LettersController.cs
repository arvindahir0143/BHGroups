using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BHGroupBAL;
using BHGroupEntity;
using BHGroup.Areas.Admin.ViewModels;

namespace BHGroup.Areas.Admin.Controllers
{
    public class LettersController : Controller
    {
       
        public ActionResult Index(int? id = null)
        {
            LetterModel model = new LetterModel();
            if (id != 0 && id!= null)
            {                
                vw_PlotBooking oPloat = new PloatBookingBAL().GetLetterDetails(id.Value);                
                model.Name = oPloat.Name;
                model.Address = oPloat.Address;
                model.phoneno = oPloat.PhoneNo_Office;
                model.StartDate = oPloat.StartDate;

            }
            //ViewBag.MemberLookUp = new MemberBAL().GetAllMemberLookUp(id == null ?0 : id);
            return View(model);
        }

        public ActionResult CommitmentLetter(int? id = null)
        {
            LetterModel model = new LetterModel();
            if (id != 0 && id != null)
            {
                vw_PlotBooking oPloat = new PloatBookingBAL().GetLetterDetails(id.Value);
                List<DrowToken> oDrow = new DrowTokenBAL().GetById(oPloat.PloatBookingId);
                model.Name = oPloat.Name;
                model.Address = oPloat.Address;
                model.phoneno = oPloat.PhoneNo_Office;
                model.plottype = oPloat.PloatType;
                model.PlotDesc = oPloat.PlotDesc;
                model.StartDate = oPloat.StartDate;
                model.EndDate = oPloat.EndDate;
                string text = null;

                foreach (DrowToken item in oDrow)
                {
                    text += "A-"+item.DrowTokenId+" & ";
                }
                model.DrowToken = text;
                model.TotalAmt = oPloat.NetAmt;
                
            }
            //ViewBag.MemberLookUp = new MemberBAL().GetAllMemberLookUp(id == null ? 0 : id);
            return View(model);
        }
    }
}

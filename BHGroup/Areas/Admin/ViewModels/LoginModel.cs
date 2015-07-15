using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BHGroup.Areas.Admin.ViewModels
{
    public class LoginModel
    {

        public int Id { get; set; }

        
        public string UserName { get; set; }

       
        public string Password { get; set; }

       
        public string confirmPassword { get; set; }

       
        public int MemberShipNo { get; set; }

    }

    public class AdminModel
    {


        [Required(ErrorMessage = "Enter Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter confirmPassword")]
        [Display(Name = "confirmPassword")]
        public string confirmPassword { get; set; }


      

    }
}
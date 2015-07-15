﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BHGroupEntity
{
    [MetadataType(typeof(MemberMetadata))]
    public partial class Member
    {
    }
    public class MemberMetadata
    {
        public int MemberId { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public int MemberShipNo { get; set; }

        [Required(ErrorMessage = "Select Refferences")]
        [Display(Name = "Refference")]
        public int ParentId { get; set; }
        public string Age { get; set; }
        public DateTime DOB { get; set; }
        public string Profession { get; set; }
        public string Address { get; set; }
        public string OfficeAddress { get; set; }
        public string PhoneNo_Office { get; set; }
        public string PhoneNo_Resident { get; set; }
        public string NomRelation { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string PANNo { get; set; }
        public string NomineesName { get; set; }
        public string NomAge { get; set; }
        public DateTime NomDOB { get; set; }
        public string NomProfession { get; set; }
        public string PreferredZone { get; set; }
        public string Password { get; set; }
        public DateTime CreateOn { get; set; }

        [Required(ErrorMessage = "Select Member Type")]
        [Display(Name = "Name")]
        public string MemberType { get; set; }

        public int Intrest { get; set; }
    }
}
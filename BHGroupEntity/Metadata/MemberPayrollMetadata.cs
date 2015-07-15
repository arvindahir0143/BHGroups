using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHGroupEntity.Metadata
{
    [MetadataType(typeof(MemberPayrollMetadata))]
    public partial class MemberPayroll
    {
    }
    public class MemberPayrollMetadata
    {
        public int PaymentId { get; set; }
        public int MemberId { get; set; }
        [Required(ErrorMessage = "Enter Installment")]
        [Display(Name = "Installment")]
        public double Installment { get; set; }

        [Required(ErrorMessage = "Enter ReceiveAmt")]
        [Display(Name = "ReceiveAmt")]
        public double ReceiveAmt { get; set; }
        public double DueAmt { get; set; }
        public string Status { get; set; }
        public System.DateTime CreatOn { get; set; }
        public System.DateTime ModifidOn { get; set; }

        [Required(ErrorMessage = "Enter BankName")]
        [Display(Name = "BankName")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "Enter AccountNo")]
        [Display(Name = "AccountNo")]
        public string AccountNo { get; set; }

        [Required(ErrorMessage = "Enter PaymentType")]
        [Display(Name = "PaymentType")]
        public string PaymentType { get; set; }
    

        public virtual Member Member { get; set; }
    }
}

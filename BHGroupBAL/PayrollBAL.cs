
using BHGroupEntity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Utilities;


namespace BHGroupBAL
{
    public class PayrollBAL
    {
        #region Get Methods

        // grid for Member
        public string GetGridData(GridSettings grid, int? MemberId)
        {
            try
            {
                using (var ctx = new BHGroupEntities())
                {
                    IQueryable<MemberPayroll> query;

                    if (MemberId != null)
                    {
                        query = ctx.MemberPayrolls.AsQueryable().Where(x => x.MemberId == MemberId);
                    }
                    else
                    {
                        query = ctx.MemberPayrolls.AsQueryable();
                    }

                  

                    int count;
                    var data = query.GridCommonSettings(grid, out count);

                    var result = new
                    {
                        total = (int)Math.Ceiling((double)count / grid.PageSize),
                        page = grid.PageIndex,
                        records = count,
                        rows = (from p in data
                                select new
                                {
                                    id = p.PaymentId,
                                    Name = p.Member.Name,
                                    Installment = p.Installment,
                                    ReceiveAmt = p.ReceiveAmt,
                                    DueAmt = p.DueAmt,

                                    BankName = p.BankName,
                                    AccountNo = p.AccountNo,
                                    PaymentType = p.PaymentType,
                                    CreatOn = p.CreatOn,
                                    Status = p.Status,
                                    Action = p.PaymentId
                                }).ToArray()
                    };
                    return JsonConvert.SerializeObject(result, new IsoDateTimeConverter());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool isNewEntry(int entityId)
        {
            if (entityId <= 0)
                return true;
            else
                return false;
        }

        public MemberPayroll GetById(int id)
        {
            try
            {
                using (var ctx = new BHGroupEntities())
                {
                    return ctx.MemberPayrolls.Where(p => p.PaymentId == id).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CRUD Operations
        public void Create(MemberPayroll oMemberPayroll)
        {
            try
            {
                using (var ctx = new BHGroupEntities())
                {
                    oMemberPayroll.CreatOn = System.DateTime.Now;
                    ctx.MemberPayrolls.Add(oMemberPayroll);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(MemberPayroll oMemberPayroll)
        {
            try
            {
                using (var ctx = new BHGroupEntities())
                {

                    ctx.Entry(oMemberPayroll).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Updatestatus(int id)
        {
            try
            {
                using (var ctx = new BHGroupEntities())
                {
                    MemberPayroll oMemberpayroll = ctx.MemberPayrolls.Where(p => p.PaymentId == id).FirstOrDefault();
                    if (oMemberpayroll.Status == "Active")
                    {
                        oMemberpayroll.Status = "InActive";
                    }
                    else
                    {
                        oMemberpayroll.Status = "Active";
                    }
                    ctx.Entry(oMemberpayroll).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (var ctx = new BHGroupEntities())
                {
                    MemberPayroll oMemberPayroll = ctx.MemberPayrolls.Where(p => p.PaymentId == id).FirstOrDefault();
                    ctx.MemberPayrolls.Remove(oMemberPayroll);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // code CRUD 
        #endregion
    }
}

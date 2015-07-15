
using BHGroupEntity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Utilities;


namespace BHGroupBAL
{
    public class MemberBAL
    {
        #region Get Methods

        // grid for Member
        public string GetGridData(GridSettings grid, string Type)
        {
            try
            {
                using (var ctx = new BHGroupEntities())
                {
                    IQueryable<Member> query;

                    if (Type == "Franchies")
                    {
                        query = ctx.Members.AsQueryable().Where(x => ((x.MemberType == "Franchies") || (x.MemberType == "Distributers")));
                    }
                    else
                    {
                        query = ctx.Members.AsQueryable().Where(x => ((x.MemberType == "Member") || (x.MemberType == "Broker")));
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
                                    id = p.MemberId,
                                    Name = p.Name,
                                    MemberShipNo = p.MemberShipNo,
                                    DOB = p.DOB,
                                    Age = p.Age,
                                    MobileNo = p.MobileNo,
                                    EmailId = p.EmailId,
                                    Password = p.Password,
                                    MemberType = p.MemberType,
                                    Action = p.MemberId
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
        public List<SelectListItem> GetMemberLookUp()
        {
            using (var ctx = new BHGroupEntities())
            {
                var query = ctx.Members.OrderBy(o => o.Name);

                List<SelectListItem> MemberLookUp =
                    query.ToList().Select(s => new SelectListItem
                    {
                        Selected = (s.MemberId == 0),
                        Text = s.Name,
                        Value = s.MemberId.ToString()
                    }).ToList();
                return MemberLookUp;
            }
        }
        public List<SelectListItem> GetMembersLookup()
        {
            using (var ctx = new BHGroupEntities())
            {
                var query = ctx.Members.Where(x => x.MemberType == "Member").OrderBy(o => o.Name);

                List<SelectListItem> MemberLookUp =
                    query.ToList().Select(s => new SelectListItem
                    {
                        Selected = (s.MemberId == 0),
                        Text = s.Name,
                        Value = s.MemberId.ToString()
                    }).ToList();
                return MemberLookUp;
            }
        }
        public List<SelectListItem> GetBrokerLookup()
        {
            using (var ctx = new BHGroupEntities())
            {
                var query = ctx.Members.Where(x => x.MemberType == "Broker").OrderBy(o => o.Name);

                List<SelectListItem> MemberLookUp =
                    query.ToList().Select(s => new SelectListItem
                    {
                        Selected = (s.MemberId == 0),
                        Text = s.Name,
                        Value = s.MemberId.ToString()
                    }).ToList();
                return MemberLookUp;
            }
        }
        public List<SelectListItem> GetFranchiesLookup()
        {
            using (var ctx = new BHGroupEntities())
            {
                var query = ctx.Members.Where(x => x.MemberType == "Franchies").OrderBy(o => o.Name);

                List<SelectListItem> MemberLookUp =
                    query.ToList().Select(s => new SelectListItem
                    {
                        Selected = (s.MemberId == 0),
                        Text = s.Name,
                        Value = s.MemberId.ToString()
                    }).ToList();
                return MemberLookUp;
            }
        }
        public List<SelectListItem> GetDistributorsLookup()
        {
            using (var ctx = new BHGroupEntities())
            {
                var query = ctx.Members.Where(x => x.MemberType == "Distributers").OrderBy(o => o.Name);

                List<SelectListItem> MemberLookUp =
                    query.ToList().Select(s => new SelectListItem
                    {
                        Selected = (s.MemberId == 0),
                        Text = s.Name,
                        Value = s.MemberId.ToString()
                    }).ToList();
                return MemberLookUp;
            }
        }

        public static string GetMembershipNo()
        {
            try
            {
                using (var ctx = new BHGroupEntities())
                {
                    int? no = ctx.Members.Max(u => u.MemberId);
                    int mno = no == 0 ? 0 : no.Value + 1;
                    return "1000" + mno;
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
        public Member GetByRefrenceId(int id)
        {
            try
            {
                using (var ctx = new BHGroupEntities())
                {
                    return ctx.Members.Where(p => p.ParentId == id).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SelectListItem> GetAllMemberLookUp(int? selectedId)
        {
            using (var ctx = new BHGroupEntities())
            {
                var query = ctx.Members.OrderBy(o => o.Name);

                List<SelectListItem> MemberLookUp =
                    query.ToList().Select(s => new SelectListItem
                    {
                        Selected = (selectedId.HasValue && s.MemberId == selectedId),
                        Text = s.Name,
                        Value = s.MemberId.ToString()
                    }).ToList();
                return MemberLookUp;
            }
        }
        public Member GetMember(int MemberShipNo)
        {
            try
            {
                using (var ctx = new BHGroupEntities())
                {
                    return ctx.Members.Where(p => p.MemberShipNo == MemberShipNo).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Member GetById(int id)
        {
            try
            {
                using (var ctx = new BHGroupEntities())
                {
                    return ctx.Members.Where(p => p.MemberId == id).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Member GetByName(string Username)
        {
            try
            {
                using (var ctx = new BHGroupEntities())
                {
                    return ctx.Members.Where(p => p.Name == Username).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetGridDataReports(GridSettings grid, string Type, int ParentId)
        {
            try
            {
                using (var ctx = new BHGroupEntities())
                {

                    var query = ctx.Prc_ViewCommision(ParentId, Type).ToList().AsQueryable();

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
                                    id = p.MemberId,
                                    Name = p.Name,
                                    MemberShipNo = p.MemberShipNo,
                                    MemberType = p.MemberType,
                                    PloatType = p.PloatType,
                                    Qty = p.Qty,
                                    Total_Commision = p.Total_Commision,
                                    Action = p.MemberId
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
        #endregion

        #region CRUD Operations
        public void Create(Member oMember)
        {
            try
            {
                using (var ctx = new BHGroupEntities())
                {
                    oMember.CreateOn = System.DateTime.Now;
                    ctx.Members.Add(oMember);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Member oMember)
        {
            try
            {
                using (var ctx = new BHGroupEntities())
                {

                    ctx.Entry(oMember).State = EntityState.Modified;
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
                    Member oMember = ctx.Members.Where(p => p.MemberId == id).FirstOrDefault();
                    ctx.Members.Remove(oMember);
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

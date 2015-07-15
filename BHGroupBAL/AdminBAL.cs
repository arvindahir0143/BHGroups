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
    public class AdminBAL
    {
        public AdminLogin ValidUser()
        {
            try
            {
                using (var ctx = new BHGroupEntities())
                {
                    return ctx.AdminLogins.Where(p => p.UserName == "Admin").SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AdminLogin GetByName(string Username)
        {
            try
            {
                using (var ctx = new BHGroupEntities())
                {
                    return ctx.AdminLogins.Where(p => p.UserName == Username).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Update(AdminLogin oAdmin)
        {
            try
            {
                using (var ctx = new BHGroupEntities())
                {

                    ctx.Entry(oAdmin).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

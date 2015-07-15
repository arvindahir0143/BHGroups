﻿using BHGroupEntity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BHGroupEntity.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Utilities;
using System.Data.Entity;

namespace BHGroupBAL
{
    public class PloatBookingBAL
    {

        #region CRUD Operations
        public void Create(PloatBooking oPloatBooking)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    using (var ctx = new BHGroupEntities())
                    {
                        oPloatBooking.StartDate = DateTime.Now;
                        oPloatBooking.EndDate = DateTime.Now.AddMonths(75);

                        if (oPloatBooking.PloatType == En_PloatType.WeekendHome.ToString())
                        {
                            oPloatBooking.Amt = 600000;
                            oPloatBooking.PlotDesc = "100 sq. yard Net (140 sq. yard Super Build Up)";
                        }
                        else if (oPloatBooking.PloatType == En_PloatType.Villa.ToString())
                        {
                            oPloatBooking.Amt = 600000 * 2;
                            oPloatBooking.PlotDesc = "200 sq. yard Net (280 sq. yard Super Build Up)";
                        }
                        else if (oPloatBooking.PloatType == En_PloatType.FarmHouse.ToString())
                        {
                            oPloatBooking.Amt = 600000 * 3;
                            oPloatBooking.PlotDesc = "400 sq. yard Net (420 sq. yard Super Build Up)";
                        }
                        oPloatBooking.NetAmt = (oPloatBooking.Amt * oPloatBooking.Qty);
                        ctx.PloatBookings.Add(oPloatBooking);

                        int token = 0;
                        if (oPloatBooking.PloatType == En_PloatType.WeekendHome.ToString())
                            token = 1;
                        else if (oPloatBooking.PloatType == En_PloatType.Villa.ToString())
                            token = 2;
                        else if (oPloatBooking.PloatType == En_PloatType.FarmHouse.ToString())
                            token = 3;
                        int count_token = (token * oPloatBooking.Qty);
                        for (int i = 0; i < count_token; i++)
                        {
                            DrowToken oDrow = new DrowToken();
                            oDrow.MemberId = oPloatBooking.MemberId;
                            oDrow.PlotBookingId = oPloatBooking.PloatBookingId;
                            oDrow.Status = En_DrowStatus.Active.ToString();
                            oDrow.Creaton = System.DateTime.Now;
                            oDrow.Modifiedon = System.DateTime.Now;
                            ctx.DrowTokens.Add(oDrow);
                        }
                        ctx.SaveChanges();
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw ex;
                }
            }
        }

        public bool isNewEntry(int entityId)
        {
            if (entityId <= 0)
                return true;
            else
                return false;
        }
        public void Update(PloatBooking oPloat)
        {
            try
            {
                using (var ctx = new BHGroupEntities())
                {
                    ctx.Entry(oPloat).State = EntityState.Modified;
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
                    PloatBooking oPloat = ctx.PloatBookings.Where(p => p.PloatBookingId == id).FirstOrDefault();
                    ctx.PloatBookings.Remove(oPloat);
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

        public string GetGridData(GridSettings grid)
        {
            try
            {
                using (var ctx = new BHGroupEntities())
                {
                    var query = ctx.PloatBookings.Include("Member").AsQueryable();

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
                                    id = p.PloatBookingId,
                                    Name = p.Member.Name,
                                    PloatType = p.PloatType,
                                    PloatQty = p.Qty,
                                    Action = p.PloatBookingId
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

        public PloatBooking GetById(int id)
        {
            try
            {
                using (var ctx = new BHGroupEntities())
                {
                    return ctx.PloatBookings.Where(p => p.PloatBookingId == id).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public vw_PlotBooking GetLetterDetails(int id)
        {
            try
            {
                using (var ctx = new BHGroupEntities())
                {
                    return ctx.vw_PlotBooking.Where(p => p.PloatBookingId == id).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

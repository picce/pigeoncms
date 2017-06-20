using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Data.Common;
using PigeonCms;
using System.Diagnostics;
using Dapper;
using System.Collections;


namespace PigeonCms.Shop
{

    /// <summary>
    /// DAL for Order obj (table #__shop_payments)
    /// </summary>
    public class PaymentsManager : TableManager<Payment, PaymentsFilter, string>
    {
        [DebuggerStepThrough()]
        public PaymentsManager()
        {
            this.TableName = "#__shop_payments";
            this.KeyFieldName = "PayCode";
        }

        public override List<Payment> GetByFilter(PaymentsFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            var result = new List<Payment>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "SELECT PayCode, Name, AssemblyName, CssClass, IsDebug, Enabled, PayAccount, "
                    + " PaySubmitUrl, PayCallbackUrl, SiteOkUrl, SiteKoUrl, "
                    + " MinAmount, MaxAmount, PayParams "
                    + " FROM ["+ this.TableName +"] t "
                    + " WHERE 1=1 ";
                if (!string.IsNullOrEmpty(filter.PayCode))
                {
                    sSql += " AND t.PayCode = @PayCode ";
                    p.Add("PayCode", filter.PayCode, null, null, null);
                }
                if (filter.CurrentAmount > 0)
                {
                    sSql += " AND ( @CurrentAmount >= t.MinAmount AND (t.maxAmount=0 OR @CurrentAmount <= t.MaxAmount) ) ";
                    p.Add("CurrentAmount", filter.CurrentAmount, null, null, null);
                }
                if (filter.IsDebug != null)
                {
                    sSql += " AND t.IsDebug = @IsDebug ";
                    p.Add("IsDebug", filter.IsDebug == true, null, null, null);
                }
                if (filter.Enabled != null)
                {
                    sSql += " AND t.Enabled = @Enabled ";
                    p.Add("Enabled", filter.Enabled == true, null, null, null);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY t.Name ";
                }

                result = (List<Payment>)myConn.Query<Payment>(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override Payment GetByKey(string payCode)
        {
            var result = new Payment();
            var list = new List<Payment>();
            var filter = new PaymentsFilter();

            if (string.IsNullOrEmpty(payCode))
                return result;

            filter.PayCode = payCode;
            list = GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];

            return result;
        }

        public override int Update(Payment theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            int result = 0;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "UPDATE [" + this.TableName + "] "
                    + " SET Name=@Name, AssemblyName=@AssemblyName, CssClass=@CssClass, IsDebug=@IsDebug, "
                    + " Enabled=@Enabled, PayAccount=@PayAccount, "
                    + " PaySubmitUrl=@PaySubmitUrl, PayCallbackUrl=@PayCallbackUrl, "
                    + " SiteOkUrl=@SiteOkUrl, SiteKoUrl=@SiteKoUrl, "
                    + " MinAmount=@MinAmount, MaxAmount=@MaxAmount, PayParams=@PayParams "
                    + " WHERE PayCode = @PayCode";

                p.Add("PayCode", theObj.PayCode, null, null, null);
                p.Add("Name", theObj.Name, null, null, null);
                p.Add("AssemblyName", theObj.AssemblyName, null, null, null);
                p.Add("CssClass", theObj.CssClass, null, null, null);
                p.Add("IsDebug", theObj.IsDebug, null, null, null);
                p.Add("Enabled", theObj.Enabled, null, null, null);
                p.Add("PayAccount", theObj.PayAccount, null, null, null);
                p.Add("PaySubmitUrl", theObj.PaySubmitUrl, null, null, null);
                p.Add("PayCallbackUrl", theObj.PayCallbackUrl, null, null, null);
                p.Add("SiteOkUrl", theObj.SiteOkUrl, null, null, null);
                p.Add("SiteKoUrl", theObj.SiteKoUrl, null, null, null);
                p.Add("MinAmount", theObj.MinAmount, null, null, null);
                p.Add("MaxAmount", theObj.MaxAmount, null, null, null);
                p.Add("PayParams", theObj.PayParams, null, null, null);

                result = myConn.Execute(Database.ParseSql(sSql), p);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override Payment Insert(Payment theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;

            if (string.IsNullOrEmpty(theObj.PayCode))
                throw new ArgumentNullException("Invalid Payment key field");

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "INSERT INTO [" + this.TableName + "] "
                    + " (PayCode, Name, AssemblyName, CssClass, IsDebug, Enabled, PayAccount, "
                    + " PaySubmitUrl, PayCallbackUrl, SiteOkUrl, SiteKoUrl, "
                    + " MinAmount, MaxAmount, PayParams) "
                    + " VALUES(@PayCode, @Name, @AssemblyName, @CssClass, @IsDebug, @Enabled, @PayAccount, "
                    + " @PaySubmitUrl, @PayCallbackUrl, @SiteOkUrl, @SiteKoUrl, "
                    + " @MinAmount, @MaxAmount, @PayParams)";

                p.Add("PayCode", theObj.PayCode, null, null, null);
                p.Add("Name", theObj.Name, null, null, null);
                p.Add("AssemblyName", theObj.AssemblyName, null, null, null);
                p.Add("CssClass", theObj.CssClass, null, null, null);
                p.Add("IsDebug", theObj.IsDebug, null, null, null);
                p.Add("Enabled", theObj.Enabled, null, null, null);
                p.Add("PayAccount", theObj.PayAccount, null, null, null);
                p.Add("PaySubmitUrl", theObj.PaySubmitUrl, null, null, null);
                p.Add("PayCallbackUrl", theObj.PayCallbackUrl, null, null, null);
                p.Add("SiteOkUrl", theObj.SiteOkUrl, null, null, null);
                p.Add("SiteKoUrl", theObj.SiteKoUrl, null, null, null);
                p.Add("MinAmount", theObj.MinAmount, null, null, null);
                p.Add("MaxAmount", theObj.MaxAmount, null, null, null);
                p.Add("PayParams", theObj.PayParams, null, null, null);

                myConn.Execute(Database.ParseSql(sSql), p);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                myConn.Dispose();
            }
            return theObj;
        }

    }
}
using System;
using System.IO;
using System.Text;
using System.Web;
using Westwind.InternetTools;
using System.Diagnostics;
using System.Data.Common;
using PigeonCms;
using System.Globalization;

namespace PayPalIntegration
{
    public class TmpOrder: ITable
    {
        //private int id = 0;
        private decimal amount = 0.0m;
        private DateTime dateInserted;
        private DateTime dateUpdated;
        private bool confirmed = false;

        /// <summary>
        /// Automatic Id as PKey
        /// </summary>
        public int Id { get; set; }

        public decimal Amount
        {
            [DebuggerStepThrough()]
            get { return amount; }
            [DebuggerStepThrough()]
            set { amount = value; }
        }

        public DateTime DateInserted
        {
            [DebuggerStepThrough()]
            get { return dateInserted; }
            [DebuggerStepThrough()]
            set { dateInserted = value; }
        }

        public DateTime DateUpdated
        {
            [DebuggerStepThrough()]
            get { return dateUpdated; }
            [DebuggerStepThrough()]
            set { dateUpdated = value; }
        }

        public bool Confirmed
        {
            [DebuggerStepThrough()]
            get { return confirmed; }
            [DebuggerStepThrough()]
            set { confirmed = value; }
        }

        public TmpOrder() { }

        public TmpOrder(decimal amount) 
        {
            this.amount = amount;
        }

    }

    public class TmpOrdersFilter
    {}


    public class TmpOrdersManager: PigeonCms.TableManager<TmpOrder, TmpOrdersFilter, int>
    {

        public TmpOrdersManager()
        {
            this.TableName = "#__paypalTmpOrders";
            this.KeyFieldName = "Id";
        }

        public override TmpOrder GetByKey(int id)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            TmpOrder result = new TmpOrder();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT t.Id, t.Amount, t.[DateInserted], t.[DateUpdated], t.Confirmed "
                    + " FROM [" + this.TableName + "] t "
                    + " WHERE t.Id = @Id ";
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", id));
                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();

                if (myRd.Read())
                {
                    if (!Convert.IsDBNull(myRd["Id"]))
                        result.Id = (int)myRd["Id"];
                    if (!Convert.IsDBNull(myRd["Amount"]))
                        result.Amount = (decimal)myRd["Amount"];
                    if (!Convert.IsDBNull(myRd["DateInserted"]))
                        result.DateInserted = (DateTime)myRd["DateInserted"];
                    if (!Convert.IsDBNull(myRd["DateUpdated"]))
                        result.DateUpdated = (DateTime)myRd["DateUpdated"];
                    if (!Convert.IsDBNull(myRd["Confirmed"]))
                        result.Confirmed = (bool)myRd["Confirmed"];
                }
                myRd.Close();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override int Update(TmpOrder theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            int result = 0;

            theObj.DateUpdated = DateTime.Now;
            if (theObj.DateInserted == DateTime.MinValue)
                theObj.DateInserted = DateTime.Now;
            try
            {

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "UPDATE [" + this.TableName + "] "
                + " SET Amount=@Amount, [DateInserted]=@DateInserted, [DateUpdated]=@DateUpdated, "
                + " Confirmed=@Confirmed "
                + " WHERE Id = @Id";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Amount", theObj.Amount));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateInserted", theObj.DateInserted));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateUpdated", theObj.DateUpdated));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Confirmed", theObj.Confirmed));

                result = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override TmpOrder Insert(TmpOrder newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            TmpOrder result = new TmpOrder();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                result = newObj;
                result.Id = base.GetNextId();
                result.DateInserted = DateTime.Now;
                result.DateUpdated = DateTime.Now;

                sSql = "INSERT INTO [" + this.TableName + "] "
                    + "(Id, Amount, DateInserted, DateUpdated, Confirmed) "
                + " VALUES(@Id, @Amount, @DateInserted, @DateUpdated, @Confirmed) ";

                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", result.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Amount", result.Amount));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateInserted", result.DateInserted));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateUpdated", result.DateUpdated));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Confirmed", result.Confirmed));

                myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }
    }

	/// <summary>
	/// Summary description for PayPalUrl.
	/// </summary>
	public class PayPalHelper
	{
		public string LogoUrl = "";
		public string AccountEmail = "";
		public string BuyerEmail = "";
		public string SuccessUrl = "";
        public string CancelUrl = "";
        public string NotifyUrl = "";
		public string ItemName = "";
		public decimal Amount = 0.00M;
		public string InvoiceNo = "";
        public string CurrencyCode = "EUR";


		
		public string PayPalBaseUrl = "https://www.paypal.com/cgi-bin/webscr?";
//"https://www.paypal.com/cgi-bin/webscr?";
//"https://www.sandbox.paypal.com/us/cgi-bin/webscr?";


		public string LastResponse = "";

		public PayPalHelper()
		{

		}

		public string GetSubmitUrl()
		{
			StringBuilder	url	= new StringBuilder();

			url.Append( this.PayPalBaseUrl + "cmd=_xclick&business="+
				HttpUtility.UrlEncode( AccountEmail ) );

			if( BuyerEmail != null && BuyerEmail != "" )
				url.AppendFormat( "&email={0}", HttpUtility.UrlEncode( BuyerEmail ) );

			if (Amount != 0.00M)
                url.AppendFormat("&amount={0:f2}", Amount).Replace(',','.');

			if( LogoUrl != null && LogoUrl != "" )
				url.AppendFormat( "&image_url={0}", HttpUtility.UrlEncode( LogoUrl ) );

			if( ItemName != null && ItemName != "" )
				url.AppendFormat( "&item_name={0}", HttpUtility.UrlEncode( ItemName ) );

			if( InvoiceNo  != null && InvoiceNo != "" )
				url.AppendFormat( "&invoice={0}", HttpUtility.UrlEncode( InvoiceNo ) );

            //IPN
            if (NotifyUrl != null && NotifyUrl != "")
                url.AppendFormat("&notify_url={0}", HttpUtility.UrlEncode(NotifyUrl));

			if( SuccessUrl != null && SuccessUrl != "" )
                url.AppendFormat( "&return={0}", HttpUtility.UrlEncode( SuccessUrl ) );

			if( CancelUrl != null && CancelUrl != "" )
				url.AppendFormat( "&cancel_return={0}", HttpUtility.UrlEncode( CancelUrl ) );

            if (CurrencyCode != null && CurrencyCode != "")
                url.AppendFormat("&currency_code={0}", HttpUtility.UrlEncode(CurrencyCode));

            url.Append("&rm=2");

			return url.ToString();
		}

		/// <summary>
		/// Posts all form variables received back to PayPal. This method is used on 
		/// is used for Payment verification from the 
		/// </summary>
		/// <returns>Empty string on success otherwise the full message from the server</returns>
		public bool IPNPostDataToPayPal(string PayPalUrl, string PayPalEmail, decimal orderAmout) 
		{			
			HttpRequest Request = HttpContext.Current.Request;
			this.LastResponse = "";

			// *** Make sure our payment goes back to our own account
			string Email = Request.Form["receiver_email"];
            if (Email == null) Email = "";
			if (Email.Trim().ToLower() != PayPalEmail.ToLower()) 
			{
                this.LastResponse = "Invalid receiver email [receiver_email=" + Email + "]";
				return false;
			}

            //debug
            //foreach (string postKey in Request.Form)
            //    this.LastResponse += postKey + "=" + Request.Form[postKey] + "; \n";
            //return false;

            //check order amount
            string payment = Request.Form["mc_gross"];
            if (payment == null)
            {
                this.LastResponse = "Order corrupt: Invalid payment amount (null).";
                return false;
            }
            try
            {
                decimal dPayment = decimal.Parse(payment, CultureInfo.InvariantCulture.NumberFormat);
                if ( Math.Abs(dPayment - orderAmout) > 0.1m)
                {
                    this.LastResponse = "Order corrupt: Invalid payment amount. [mc_gross=" + payment + "; orderAmount=" + orderAmout.ToString() + "]";
                    return false;
                }
            }
            catch
            {
                this.LastResponse = "Invalid order amount returned from paypal. [mc_gross=" + payment + "; orderAmount=" + orderAmout.ToString() + "]";
                return false;
            }
	
			wwHttp Http = new wwHttp();
			Http.AddPostKey("cmd","_notify-validate");

			foreach (string postKey in Request.Form)
				Http.AddPostKey(postKey,Request.Form[postKey]);
    
			// *** Retrieve the HTTP result to a string
			this.LastResponse = Http.GetUrl(PayPalUrl);
    
			if (this.LastResponse ==  "VERIFIED" )
				return true;

			return false;
		}

	}
}

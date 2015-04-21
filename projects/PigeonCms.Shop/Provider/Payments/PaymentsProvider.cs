using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Data.Common;
using System.Diagnostics;
using PigeonCms;
using PigeonCms.Core.Helpers;
using System.Collections.Specialized;
using System.Web.UI;


namespace PigeonCms.Shop.PaymentsProvider
{
    public abstract class BasePayment/*<OM>*/
        /*where OM: PigeonCms.Shop.OrdersManager, new()*/
    {
        protected Payment PaymentData = null;
        protected Page Page = null;


        public BasePayment() { }

        public virtual BasePayment SetParams(Page page, Payment payment)
        {
            this.Page = page;
            this.PaymentData = payment;
            return this;
        }

        public abstract void Submit(PigeonCms.Shop.Order order, NameValueCollection data = null);

        //How to: Raise Base Class Events in Derived Classes
        //https://msdn.microsoft.com/en-us/library/hy3sefw3.aspx
        public event EventHandler<ConfirmEventArgs> PaymentConfirmed;

        //The event-invoking method that derived classes can override. 
        public virtual void OnPaymentConfirmed(ConfirmEventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of 
            // a race condition if the last subscriber unsubscribes 
            // immediately after the null check and before the event is raised.
            EventHandler<ConfirmEventArgs> handler = PaymentConfirmed;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public class ConfirmEventArgs : EventArgs
        {
            public bool Success = false;
            public decimal TotalPaid = 0m;
            public Order OrderToPay = null;
            public Payment PaymentData = null;
            public string MessageName;
            public Exception Exception = null;

            //useful in log
            public override string ToString()
            {
                string orderToPayString = "null";
                if (this.OrderToPay != null)
                {
                    orderToPayString = "#" + OrderToPay.Id.ToString()
                        + "/ref:" + OrderToPay.OrderRef
                        + "/pay:" + OrderToPay.PaymentCode
                        + "/tot:" + OrderToPay.TotalAmount.ToString();
                }

                string paymentDataString = "null";
                if (this.PaymentData != null)
                    paymentDataString = this.PaymentData.PayCode;

                string exceptionString = "null";
                if (this.Exception != null)
                    exceptionString = this.Exception.ToString();

                string res = "";
                res += "Success=" + this.Success.ToString();
                res += " <br>| TotalPaid=" + this.TotalPaid.ToString();
                res += " <br>| MessageName=" + this.MessageName;
                res += " <br>| payCode=" + paymentDataString;
                res += " <br>| OrderToPay=[" + orderToPayString + "]";
                res += " <br>| Exception=[" + exceptionString + "]";
                return res;
            }
        }

        public abstract void Confirm();

        protected string ParseUrl(string url)
        {
            string res = url;
            if (string.IsNullOrEmpty(res))
            {
                string baseurl = new UriBuilder(
                    HttpContext.Current.Request.Url.Scheme,
                    HttpContext.Current.Request.Url.Host,
                    HttpContext.Current.Request.Url.Port).ToString();
                res = baseurl;
            }
            res = addPayCodeParam(res);
            return res;
        }

        private string addPayCodeParam(string url)
        {
            var payParams = new NameValueCollection();
            payParams.Add("p", this.PaymentData.PayCode);
            Utility.BuildQueryString("", payParams);

            var res = Utility.BuildQueryString(url, payParams);
            return res;
        }
    }

    /// <summary>
    /// create an instance of class specified in paycode/payment
    /// the specified class have to extend BasePayment abstract class
    /// </summary>
    public sealed class PaymentFactory/*<OM>
        where OM : PigeonCms.Shop.OrdersManager, new()*/
    {
        public static BasePayment Create(Page page, string payCode)
        {
            if (string.IsNullOrEmpty(payCode))
                throw new ArgumentException("Missing payCode");

            var payment = new Payment();
            var man = new PaymentsManager();
            payment = man.GetByKey(payCode);

            return create(page, payment);
        }

        public static BasePayment Create(Page page, Payment payment)
        {
            if (string.IsNullOrEmpty(payment.PayCode))
                throw new ArgumentException("Missing payCode");

            return create(page, payment);
        }

        private static BasePayment create(Page page, Payment payment)
        {
            object[] parameters = new object[2];
            parameters[0] = page;
            parameters[1] = payment;

            object obj = PigeonCms.Reflection.Process(
                payment.AssemblyName, "SetParams", parameters);

            return (BasePayment)obj;
        }

    }//class

}//ns
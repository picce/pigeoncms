using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Data.Common;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Web.UI;
using System.Net;
using PigeonCms;
using PigeonCms.Core.Helpers;
using System.Text;
using System.Globalization;


namespace PigeonCms.Shop.PaymentsProvider
{


    public class PayPal : BasePayment
    {
        //override is mandatory to use event in derived class
        //public override event EventHandler<BasePayment.ConfirmEventArgs> OnConfirm;


        public override void Submit(IOrder order, NameValueCollection data = null)
        {
            decimal tot = order.TotalAmount;
            string item = "Order " + order.OrderRef + " / " + order.OrderDate.ToShortDateString();

            //paypal vars
            //https://developer.paypal.com/docs/classic/paypal-payments-standard/integration-guide/Appx_websitestandard_htmlvariables/
            //https://cms.paypal.com/it/cgi-bin/?cmd=_render-content&content_ID=developer/e_howto_html_Appx_websitestandard_htmlvariables
            var fields = new NameValueCollection();
            fields.Add("cmd", "_xclick");
            fields.Add("item_name", item);
            fields.Add("item_number", order.OrderRef);//used as ipn
            fields.Add("quantity", "1");
            fields.Add("shipping", "0");
            fields.Add("no_shipping", "1"); //1 – non richiede un indirizzo
            fields.Add("amount", tot.ToString("0.00").Replace(',', '.'));
            fields.Add("business", base.PaymentData.PayAccount);    //idpaypal o indirizzo email
            //fields.Add("H_PhoneNumber", book.RefTelefono);//--> non trovato in doc paypal
            fields.Add("first_name", "");
            fields.Add("last_name", order.OrdName);
            fields.Add("address1", order.OrdAddress);
            fields.Add("address2", "");
            fields.Add("city", order.OrdCity);
            fields.Add("zip", order.OrdZipCode);
            fields.Add("state", order.OrdState);
            fields.Add("country", order.OrdNation);
            fields.Add("email", order.OrdEmail);//?
            fields.Add("currency_code", order.Currency);
            fields.Add("no_note", "1");//1 – nasconde la casella di testo e la richiesta da parte dell utente
            //fields.Add("cbt", "Back to website");//Imposta il testo del pulsante Ritorna al commerciante nella pagina Pagamento

            fields.Add("notify_url", ParseUrl(PaymentData.PayCallbackUrl));
            fields.Add("return", ParseUrl(PaymentData.SiteOkUrl));
            fields.Add("cancel_return", ParseUrl(PaymentData.SiteKoUrl));
            
            fields.Add("charset", "ISO-8859-1");
            //fields.Add("lc", "IT");


            RedirHelper.RedirectAndPOST(base.Page, base.PaymentData.PaySubmitUrl, fields);
        }

        public override void Confirm()
        {
            var args = new ConfirmEventArgs();
            args.PaymentData = base.PaymentData;
            args.Success = true;

            string orderRef = HttpContext.Current.Request.Form["item_number"];
            var man = new OrdersManager<Order, OrdersFilter, OrderRow, OrderRowsFilter>(); //new OM();
            args.OrderToPay = man.GetByOrderRef(orderRef);

            if (args.OrderToPay.Id == 0)
            {
                args.Success = false;
                args.MessageName = "InvalidOrderRef";

                OnPaymentConfirmed(args);
                return;
            }

            string strResponse = "";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(base.PaymentData.PaySubmitUrl);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                byte[] param = HttpContext.Current.Request.BinaryRead(HttpContext.Current.Request.ContentLength);
                string strRequest = Encoding.ASCII.GetString(param);
                strRequest += "&cmd=_notify-validate";
                req.ContentLength = strRequest.Length;

                StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
                streamOut.Write(strRequest);
                streamOut.Close();
                StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
                strResponse = streamIn.ReadToEnd();
                streamIn.Close();
            }
            catch (Exception ex)
            {
                args.MessageName = "RequestErr";
                args.Success = false;
                args.Exception = ex;

                OnPaymentConfirmed(args);
                return;
            }

            if (strResponse == "VERIFIED")
            {

                var allowedStatus = new List<string>();
                allowedStatus.Add("completed");
                if (base.PaymentData.IsDebug)
                    allowedStatus.Add("pending");


                args.TotalPaid = 0;
                Decimal.TryParse(
                    HttpContext.Current.Request.Form["mc_gross"].Replace(',', '.'),
                    NumberStyles.Any,
                    new CultureInfo("en-US"),
                    out args.TotalPaid);


                string payment_status = HttpContext.Current.Request.Form["payment_status"].ToString();
                string receiver_email = HttpContext.Current.Request.Form["receiver_email"].ToString();

                if (!allowedStatus.Contains(payment_status.ToLower()))
                {
                    args.Success = false;
                    args.MessageName = "InvalidPaymentStatus";

                    OnPaymentConfirmed(args);
                    return;
                }

                if (base.PaymentData.PayAccount != receiver_email)
                {
                    args.Success = false;
                    args.MessageName = "InvalidReceiverEmail";

                    OnPaymentConfirmed(args);
                    return;
                }

            }
            else
            {
                args.Success = false;
                args.MessageName = "InvalidRespone:["+ strResponse +"] ";

                OnPaymentConfirmed(args);
                return;
            }

            args.MessageName = "OK";
            OnPaymentConfirmed(args);

        }

        public override void OnPaymentConfirmed(ConfirmEventArgs e)
        {
            // Do any circle-specific processing here. 

            // Call the base class event invocation method. 
            base.OnPaymentConfirmed(e);
        }

        //private void raiseOnConfirm(ConfirmEventArgs args)
        //{
        //    //OnPaymentConfirmed(args);
        //    //    this.OnConfirm(null, args);
        //}

    }

}//ns
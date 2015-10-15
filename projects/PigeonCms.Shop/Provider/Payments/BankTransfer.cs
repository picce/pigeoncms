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


    public class BankTransfer : BasePayment
    {
        //override is mandatory to use event in derived class
        //public override event EventHandler<BasePayment.ConfirmEventArgs> OnConfirm;


        public override void Submit(IOrder order, NameValueCollection data = null)
        {
            //submit and redir to banktransfer summary page
            var fields = new NameValueCollection();
            fields.Add("p", "bank-transfer");
            fields.Add("item_number", order.OrderRef);

            RedirHelper.RedirectAndPOST(base.Page, base.PaymentData.PaySubmitUrl, fields);
        }

        public override void Confirm()
        {
            var args = new ConfirmEventArgs();
            args.PaymentData = base.PaymentData;
            args.Success = true;
            args.TotalPaid = 0;

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

            args.MessageName = "OK";
            OnPaymentConfirmed(args);
        }

        public override void OnPaymentConfirmed(ConfirmEventArgs e)
        {
            // Do any circle-specific processing here. 

            // Call the base class event invocation method. 
            base.OnPaymentConfirmed(e);
        }

    }

}//ns
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using PigeonCms;
using System.Globalization;


namespace PigeonCms.Shop
{
    /// <summary>
    /// base control for module PigeonCms.Shop.OrdersAdmin
    /// </summary>
    public class OrdersAdminControl<OM, OT, OF, RT, RF> : PigeonCms.BaseModuleControl
        where OM : IOrdersManager<OT, OF, RT, RF>, new()
        where OT : IOrder, new()
        where OF : PigeonCms.Shop.IOrderFilter, new()
        where RT : PigeonCms.Shop.OrderRow, new()
        where RF : PigeonCms.Shop.OrderRowsFilter, new()
    {

        /// <summary>
        /// filter on owner from xml module settings
        /// </summary>
        public string OwnerUser
        {
            get
            {
                string ownerUser = "";
                return GetStringParam("OwnerUser", ownerUser);
            }
        }

        /// <summary>
        /// filter from xml setting
        /// 2 - not set
        /// 1 - done
        /// 0 - pending
        /// </summary>
        protected Utility.TristateBool PaymentFilter
        {
            get
            {
                int value = (int)Utility.TristateBool.NotSet;
                value = GetIntParam("PaymentFilter", value);
                return (Utility.TristateBool)value;
            }
        }

        /// <summary>
        /// filter from xml setting
        /// </summary>
        protected Utility.TristateBool OrderConfirmedFilter
        {
            get
            {
                int value = (int)Utility.TristateBool.NotSet;
                value = GetIntParam("OrderConfirmedFilter", value);
                return (Utility.TristateBool)value;
            }
        }

        /// <summary>
        /// current order row in edit mode
        /// </summary>
        protected int CurrentRowId
        {
            get
            {
                int res = 0;
                if (ViewState["CurrentRowId"] != null)
                    res = (int)ViewState["CurrentRowId"];
                return res;
            }
            set { ViewState["CurrentRowId"] = value; }
        }


        protected OM OrdMan = new OM();
        protected OF OrdFilter = new OF();
        protected RF RowsFilter = new RF();

        protected void Page_Load(object sender, EventArgs e)
        { }

        protected void SetDate(TextBox text, DateTime date)
        {
            text.Text = "";
            if (date != DateTime.MinValue)
                text.Text = date.ToString("dd/MM/yyyy");
        }

        protected DateTime GetDate(TextBox text)
        {
            CultureInfo culture;
            DateTimeStyles styles;
            culture = CultureInfo.CreateSpecificCulture("it-IT");
            styles = DateTimeStyles.None;
            DateTime res;
            DateTime.TryParse(text.Text, culture, styles, out res);
            return res.Date;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using PigeonCms;
using System.Diagnostics;


namespace PigeonCms.Shop
{

    public class Payment : ITable
    {
        public Payment()
        {
        }

        private string payCode = "";
        [DataObjectField(true)]
        public string PayCode
        {
            [DebuggerStepThrough()]
            get { return payCode; }
            [DebuggerStepThrough()]
            set { payCode = value; }
        }


        private string name = "";
        public string Name
        {
            [DebuggerStepThrough()]
            get { return name; }
            [DebuggerStepThrough()]
            set { name = value; }
        }

        private string assemblyName = "";
        public string AssemblyName
        {
            [DebuggerStepThrough()]
            get { return assemblyName; }
            [DebuggerStepThrough()]
            set { assemblyName = value; }
        }

        private string cssClass = "";
        public string CssClass
        {
            [DebuggerStepThrough()]
            get { return cssClass; }
            [DebuggerStepThrough()]
            set { cssClass = value; }
        }

        private bool isDebug = false;
        public bool IsDebug
        {
            [DebuggerStepThrough()]
            get { return isDebug; }
            [DebuggerStepThrough()]
            set { isDebug = value; }
        }

        private bool enabled = false;
        public bool Enabled
        {
            [DebuggerStepThrough()]
            get { return enabled; }
            [DebuggerStepThrough()]
            set { enabled = value; }
        }

        private string payAccount = "";
        public string PayAccount
        {
            [DebuggerStepThrough()]
            get { return payAccount; }
            [DebuggerStepThrough()]
            set { payAccount = value; }
        }

        private string paySubmitUrl = "";
        public string PaySubmitUrl
        {
            [DebuggerStepThrough()]
            get { return paySubmitUrl; }
            [DebuggerStepThrough()]
            set { paySubmitUrl = value; }
        }

        private string payCallbackUrl = "";
        public string PayCallbackUrl
        {
            [DebuggerStepThrough()]
            get { return payCallbackUrl; }
            [DebuggerStepThrough()]
            set { payCallbackUrl = value; }
        }

        private string siteOkUrl = "";
        public string SiteOkUrl
        {
            [DebuggerStepThrough()]
            get { return siteOkUrl; }
            [DebuggerStepThrough()]
            set { siteOkUrl = value; }
        }

        private string siteKoUrl = "";
        public string SiteKoUrl
        {
            [DebuggerStepThrough()]
            get { return siteKoUrl; }
            [DebuggerStepThrough()]
            set { siteKoUrl = value; }
        }

        private decimal minAmount = 0;
        public decimal MinAmount
        {
            [DebuggerStepThrough()]
            get { return minAmount; }
            [DebuggerStepThrough()]
            set { minAmount = value; }
        }

        private decimal maxAmount = 0;
        public decimal MaxAmount
        {
            [DebuggerStepThrough()]
            get { return maxAmount; }
            [DebuggerStepThrough()]
            set { maxAmount = value; }
        }

        private string payParams = "";
        public string PayParams
        {
            [DebuggerStepThrough()]
            get { return payParams; }
            [DebuggerStepThrough()]
            set { payParams = value; }
        }

    }

    [Serializable]
    public class PaymentsFilter
    {
        private string payCode = "";
        public string PayCode
        {
            [DebuggerStepThrough()]
            get { return payCode; }
            [DebuggerStepThrough()]
            set { payCode = value; }
        }

        private decimal currentAmount = 0;
        /// <summary>
        /// current order amount.
        /// when >0  Results => MinAmount <= CurrentAmount <= MaxAmount
        /// </summary>
        public decimal CurrentAmount
        {
            [DebuggerStepThrough()]
            get { return currentAmount; }
            [DebuggerStepThrough()]
            set { currentAmount = value; }
        }

        bool? isDebug = null;
        public bool? IsDebug
        {
            [DebuggerStepThrough()]
            get { return isDebug; }
            [DebuggerStepThrough()]
            set { isDebug = value; }
        }

        bool? enabled = null;
        public bool? Enabled
        {
            [DebuggerStepThrough()]
            get { return enabled; }
            [DebuggerStepThrough()]
            set { enabled = value; }
        }
    }


}

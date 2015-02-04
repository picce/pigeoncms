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
using PigeonCms;

namespace PigeonCms
{
    [DebuggerDisplay("Id={id}, OwnerUser={ownerUser}, FromUser={fromUser}, ToUser={ToUser}, Content={content}")]
    public class Customer: ITable
    {
        private int id = 0;
        /// <summary>
        /// IDENTITY Id as PKey
        /// </summary>
        [DataObjectField(true)]
        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        private string companyName = "";
        public string CompanyName
        {
            [DebuggerStepThrough()]
            get { return companyName; }
            [DebuggerStepThrough()]
            set { companyName = value; }
        }

        private string vat = "";
        public string Vat
        {
            [DebuggerStepThrough()]
            get { return vat; }
            [DebuggerStepThrough()]
            set { vat = value; }
        }

        private DateTime dateInserted;
        public DateTime DateInserted
        {
            [DebuggerStepThrough()]
            get { return dateInserted; }
            [DebuggerStepThrough()]
            set { dateInserted = value; }
        }

        private string userInserted = "";
        public string UserInserted
        {
            [DebuggerStepThrough()]
            get { return userInserted; }
            [DebuggerStepThrough()]
            set { userInserted = value; }
        }

        private DateTime dateUpdated;
        public DateTime DateUpdated
        {
            [DebuggerStepThrough()]
            get { return dateUpdated; }
            [DebuggerStepThrough()]
            set { dateUpdated = value; }
        }

        private string userUpdated = "";
        public string UserUpdated
        {
            [DebuggerStepThrough()]
            get { return userUpdated; }
            [DebuggerStepThrough()]
            set { userUpdated = value; }
        }

    }


    [Serializable]
    public class CustomersFilter
    {
        private int id = 0;
        private string companyNameLike = "";
        private string vat = "";
        private string vatLike = "";

        [DataObjectField(true)]
        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        public string CompanyNameLike
        {
            [DebuggerStepThrough()]
            get { return companyNameLike; }
            [DebuggerStepThrough()]
            set { companyNameLike = value; }
        }

        [DataObjectField(false)]
        public string Vat
        {
            [DebuggerStepThrough()]
            get { return vat; }
            [DebuggerStepThrough()]
            set { vat = value; }
        }

        [DataObjectField(false)]
        public string VatLike
        {
            [DebuggerStepThrough()]
            get { return vatLike; }
            [DebuggerStepThrough()]
            set { vatLike = value; }
        }
    }
}

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Diagnostics;
using System.ComponentModel;
using PigeonCms;


namespace PigeonCms.Shop
{
    [DebuggerDisplay("Id={id}, CompanyName={companyName}, OwnerUser={ownerUser}")]
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

        private string ownerUser = "";
        public string OwnerUser
        {
            [DebuggerStepThrough()]
            get { return ownerUser; }
            [DebuggerStepThrough()]
            set { ownerUser = value; }
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

        private string companyName = "";
        public string CompanyName
        {
            [DebuggerStepThrough()]
            get { return companyName; }
            [DebuggerStepThrough()]
            set { companyName = value; }
        }

        private string firstName = "";
        public string FirstName
        {
            [DebuggerStepThrough()]
            get { return firstName; }
            [DebuggerStepThrough()]
            set { firstName = value; }
        }

        private string secondName = "";
        public string SecondName
        {
            [DebuggerStepThrough()]
            get { return secondName; }
            [DebuggerStepThrough()]
            set { secondName = value; }
        }

        private string ssn = "";
        /// <summary>
        /// ssn, cf in italy
        /// indexed field
        /// </summary>
        public string Ssn
        {
            [DebuggerStepThrough()]
            get { return ssn; }
            [DebuggerStepThrough()]
            set { ssn = value; }
        }

        private string vat = "";
        /// <summary>
        /// value added tax, piva in italy
        /// indexed field
        /// </summary>
        public string Vat
        {
            [DebuggerStepThrough()]
            get { return vat; }
            [DebuggerStepThrough()]
            set { vat = value; }
        }

        private string address = "";
        public string Address
        {
            [DebuggerStepThrough()]
            get { return address; }
            [DebuggerStepThrough()]
            set { address = value; }
        }

        private string city = "";
        public string City
        {
            [DebuggerStepThrough()]
            get { return city; }
            [DebuggerStepThrough()]
            set { city = value; }
        }

        private string state = "";
        /// <summary>
        /// state or prov in italy
        /// </summary>
        public string State
        {
            [DebuggerStepThrough()]
            get { return state; }
            [DebuggerStepThrough()]
            set { state = value; }
        }

        private string zipCode = "";
        /// <summary>
        /// zip or cap in italy
        /// </summary>
        public string ZipCode
        {
            [DebuggerStepThrough()]
            get { return zipCode; }
            [DebuggerStepThrough()]
            set { zipCode = value; }
        }

        private string nation = "";
        public string Nation
        {
            [DebuggerStepThrough()]
            get { return nation; }
            [DebuggerStepThrough()]
            set { nation = value; }
        }

        private string tel1 = "";
        public string Tel1
        {
            [DebuggerStepThrough()]
            get { return tel1; }
            [DebuggerStepThrough()]
            set { tel1 = value; }
        }

        private string mobile1 = "";
        public string Mobile1
        {
            [DebuggerStepThrough()]
            get { return mobile1; }
            [DebuggerStepThrough()]
            set { mobile1 = value; }
        }

        private string website1 = "";
        public string Website1
        {
            [DebuggerStepThrough()]
            get { return website1; }
            [DebuggerStepThrough()]
            set
            {
                if (value.ToLower().StartsWith("www."))
                    value = "http://" + value;
                website1 = value;
            }
        }

        private string email = "";
        public string Email
        {
            [DebuggerStepThrough()]
            get { return email; }
            [DebuggerStepThrough()]
            set { email = value; }
        }

        private bool enabled = true;
        [DataObjectField(false)]
        public bool Enabled
        {
            [DebuggerStepThrough()]
            get { return enabled; }
            [DebuggerStepThrough()]
            set { enabled = value; }
        }

        private string notes = "";
        public string Notes
        {
            [DebuggerStepThrough()]
            get { return notes; }
            [DebuggerStepThrough()]
            set 
            {
                if (value.Length > 5000)
                    notes = value.Substring(0, 5000);
                else
                    notes = value; 
            }
        }

        private string jsData = "";
        /// <summary>
        /// json serialized obj
        /// </summary>
        public string JsData
        {
            [DebuggerStepThrough()]
            get { return jsData; }
            [DebuggerStepThrough()]
            set { jsData = value; }
        }

        private string custom1 = "";
        public string Custom1
        {
            [DebuggerStepThrough()]
            get { return custom1; }
            [DebuggerStepThrough()]
            set { custom1 = value; }
        }

        private string custom2 = "";
        public string Custom2
        {
            [DebuggerStepThrough()]
            get { return custom2; }
            [DebuggerStepThrough()]
            set { custom2 = value; }
        }

        private string custom3 = "";
        public string Custom3
        {
            [DebuggerStepThrough()]
            get { return custom3; }
            [DebuggerStepThrough()]
            set { custom3 = value; }
        }

    }


    [Serializable]
    public class CustomersFilter
    {
        private int id = 0;
        private string ownerUser = "";
        private string companyNameLike = "";
        private string ssn = "";
        private string ssnLike = "";
        private string vat = "";
        private string vatLike = "";
        private Utility.TristateBool enabled = Utility.TristateBool.NotSet;

        [DataObjectField(true)]
        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        public string OwnerUser
        {
            [DebuggerStepThrough()]
            get { return ownerUser; }
            [DebuggerStepThrough()]
            set { ownerUser = value; }
        }

        public string CompanyNameLike
        {
            [DebuggerStepThrough()]
            get { return companyNameLike; }
            [DebuggerStepThrough()]
            set { companyNameLike = value; }
        }

        public string Ssn
        {
            [DebuggerStepThrough()]
            get { return ssn; }
            [DebuggerStepThrough()]
            set { ssn = value; }
        }

        public string SsnLike
        {
            [DebuggerStepThrough()]
            get { return ssnLike; }
            [DebuggerStepThrough()]
            set { ssnLike = value; }
        }

        public string Vat
        {
            [DebuggerStepThrough()]
            get { return vat; }
            [DebuggerStepThrough()]
            set { vat = value; }
        }

        public string VatLike
        {
            [DebuggerStepThrough()]
            get { return vatLike; }
            [DebuggerStepThrough()]
            set { vatLike = value; }
        }

        public Utility.TristateBool Enabled
        {
            [DebuggerStepThrough()]
            get { return enabled; }
            [DebuggerStepThrough()]
            set { enabled = value; }
        }
    }
}

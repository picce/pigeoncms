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
using PigeonCms;
using System.Collections.Generic;
using System.Threading;
using System.Web.Compilation;
using System.Reflection;



namespace PigeonCms
{
    public class Seo: ITable
    {
        private DateTime dateUpdated;
        private string userUpdated = "";
		private bool noIndex = false;
		private bool noFollow = false;
        private Dictionary<string, string> titleTranslations = new Dictionary<string, string>();
        private Dictionary<string, string> descriptionTranslations = new Dictionary<string, string>();



        #region fields

        /// <summary>
        /// Identity Id as PKey
        /// </summary>
        [DataObjectField(true)]
        public int Id { get; set; }

		/// <summary>
		/// keep table human readable; avoid to access wrong resources
		/// </summary>
		public string ResourceSet { get; set; }


        /// <summary>
        /// record updated date
        /// </summary>
        public DateTime DateUpdated
        {
            [DebuggerStepThrough()]
            get { return dateUpdated; }
            [DebuggerStepThrough()]
            set { dateUpdated = value; }
        }

        /// <summary>
        /// record updated user
        /// </summary>
        public string UserUpdated
        {
            [DebuggerStepThrough()]
            get { return userUpdated; }
            [DebuggerStepThrough()]
            set { userUpdated = value; }
        }

		/// <summary>
		/// true for noindex robots meta
		/// </summary>
		public bool NoIndex
        {
            [DebuggerStepThrough()]
            get { return noIndex; }
            [DebuggerStepThrough()]
            set { noIndex = value; }
        }

		/// <summary>
		/// true for nofollow robots meta
		/// </summary>
		public bool NoFollow
        {
            [DebuggerStepThrough()]
            get { return noFollow; }
            [DebuggerStepThrough()]
            set { noFollow = value; }
        }


        /// <summary>
        /// Title in current culture
        /// </summary>
        [DataObjectField(false)]
        public string Title
        {
            get
            {
				string res = LabelsProvider.GetLocalizedTextFromDictionary(titleTranslations);
                return res;
            }
        }

        /// <summary>
        /// Title in different culture
        /// </summary>
        [DataObjectField(false)]
        public Dictionary<string, string> TitleTranslations
        {
            [DebuggerStepThrough()]
            get { return titleTranslations; }
            [DebuggerStepThrough()]
            set { titleTranslations = value; }
        }


        /// <summary>
        /// Raw Description in current culture
        /// </summary>
        [DataObjectField(false)]
        public string Description
        {
            get
            {
				string res = LabelsProvider.GetLocalizedTextFromDictionary(descriptionTranslations);
                return res;
            }
        }


        /// <summary>
        /// Raw Description in different culture
        /// </summary>
        [DataObjectField(false)]
        public Dictionary<string, string> DescriptionTranslations
        {
            [DebuggerStepThrough()]
            get { return descriptionTranslations; }
            [DebuggerStepThrough()]
            set { descriptionTranslations = value; }
        }




        #endregion

	}


    [Serializable]
    public class SeoFilter: ITableFilter
    {
    }

}

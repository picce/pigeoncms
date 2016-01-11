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



namespace PigeonCms
{
    public class TagType : ITableWithOrdering, ITableExternalId
    {
        private Dictionary<string, string> titleTranslations = new Dictionary<string, string>();
        private Dictionary<string, string> descriptionTranslations = new Dictionary<string, string>();

        /// <summary>
        /// identity Id as PKey
        /// </summary>
        [DataObjectField(true)]
        public int Id { get; set;}

        public string ItemType { get; set; }

        public int Ordering { get; set; }

        /// <summary>
        /// external identifier to allow import/export from external datasource
        /// </summary>
        public string ExtId { get; set; }

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
        /// Description in current culture
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
        /// Title in different culture
        /// </summary>
        [DataObjectField(false)]
        public Dictionary<string, string> DescriptionTranslations
        {
            [DebuggerStepThrough()]
            get { return descriptionTranslations; }
            [DebuggerStepThrough()]
            set { descriptionTranslations = value; }
        }
    }


    [Serializable]
    public class TagTypesFilter : ITableExternalId
    {
        [DataObjectField(true)]
        public int Id { get; set; }

        public string ItemType { get; set; }

        public string ExtId { get; set; }
    }
}
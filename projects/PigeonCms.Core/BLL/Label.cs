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
    
    [DebuggerDisplay("ResourceSet={resourceSet}, ResourceId={resourceId}")]
    public class ResLabelTrans : ITable
    {
        private string resourceSet = "";
        private string resourceId = "";
        private ContentEditorProvider.Configuration.EditorTypeEnum textMode = ContentEditorProvider.Configuration.EditorTypeEnum.Text;
        private bool isLocalized = true;
        //private Dictionary<string, string> valueTranslations = new Dictionary<string, string>();


        public string ResourceSet
        {
            [DebuggerStepThrough()]
            get { return resourceSet; }
            [DebuggerStepThrough()]
            set { resourceSet = value; }
        }

        public string ResourceId
        {
            [DebuggerStepThrough()]
            get { return resourceId; }
            [DebuggerStepThrough()]
            set { resourceId = value; }
        }

        public ContentEditorProvider.Configuration.EditorTypeEnum TextMode
        {
            [DebuggerStepThrough()]
            get { return this.textMode; }
            [DebuggerStepThrough()]
            set { this.textMode = value; }
        }

        public bool IsLocalized
        {
            get { return isLocalized; }
            set { isLocalized = value; }
        }

    }

    [Serializable]
    [DebuggerDisplay("ResourceSet={resourceSet}, ResourceId={resourceId}")]
    public class LabelTransFilter
    {
        private string resourceSet = "";
        private string resourceId = "";

        public string ResourceSet
        {
            [DebuggerStepThrough()]
            get { return resourceSet; }
            [DebuggerStepThrough()]
            set { resourceSet = value; }
        }

        public string ResourceId
        {
            [DebuggerStepThrough()]
            get { return resourceId; }
            [DebuggerStepThrough()]
            set { resourceId = value; }
        }
    }

    [DebuggerDisplay("CultureName={cultureName}, ResourceSet={resourceSet}, ResourceId={resourceId}")]
    public class ResLabel: ITable
    {
        private int id = 0;
        private string cultureName = "";
        private string resourceSet = "";
        private string resourceId = "";
        private string value = "";
        private string comment = "";
        private ContentEditorProvider.Configuration.EditorTypeEnum textMode = ContentEditorProvider.Configuration.EditorTypeEnum.Text;
        private bool isLocalized = true;

        /// <summary>
        /// Automatic Id as PKey
        /// </summary>
        [DataObjectField(true)]
        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        public string CultureName
        {
            [DebuggerStepThrough()]
            get { return cultureName; }
            [DebuggerStepThrough()]
            set { cultureName = value; }
        }

        public string ResourceSet
        {
            [DebuggerStepThrough()]
            get { return resourceSet; }
            [DebuggerStepThrough()]
            set { resourceSet = value; }
        }

        public string ResourceId
        {
            [DebuggerStepThrough()]
            get { return resourceId; }
            [DebuggerStepThrough()]
            set { resourceId = value; }
        }

        public string Value
        {
            [DebuggerStepThrough()]
            get { return this.value; }
            [DebuggerStepThrough()]
            set { this.value = value; }
        }

        public string Comment
        {
            [DebuggerStepThrough()]
            get { return comment; }
            [DebuggerStepThrough()]
            set { comment = value; }
        }

        /// <summary>
        /// editor type used in backend area.
        /// integration with PigeonCms.Controls.Label server control
        /// value is propagated for all translations
        /// </summary>
        public ContentEditorProvider.Configuration.EditorTypeEnum TextMode
        {
            [DebuggerStepThrough()]
            get { return this.textMode; }
            [DebuggerStepThrough()]
            set { this.textMode = value; }
        }

        /// <summary>
        /// manage or not translations
        /// </summary>
        public bool IsLocalized
        {
            get { return isLocalized; }
            set { isLocalized = value; }
        }

        public ResLabel() { }
    }


    [Serializable]
    public class LabelsFilter
    {
        private int id = 0;
        private string cultureName = "";
        private string resourceSet = "";
        private string resourceId = "";

        /// <summary>
        /// Automatic Id as PKey
        /// </summary>
        [DataObjectField(true)]
        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        public string CultureName
        {
            [DebuggerStepThrough()]
            get { return cultureName; }
            [DebuggerStepThrough()]
            set { cultureName = value; }
        }

        public string ResourceSet
        {
            [DebuggerStepThrough()]
            get { return resourceSet; }
            [DebuggerStepThrough()]
            set { resourceSet = value; }
        }

        public string ResourceId
        {
            [DebuggerStepThrough()]
            get { return resourceId; }
            [DebuggerStepThrough()]
            set { resourceId = value; }
        }
    }
}
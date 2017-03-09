using System;
using System.Collections.Generic;
using System.Text;
using PigeonCms;
using System.ComponentModel;
using System.Diagnostics;

namespace PigeonCms
{
    public enum FormFieldTypeEnum
    {
        Text = 0,
        List,
        Combo,
        Check,
        Calendar,
        Custom,
        Spacer,
        Hidden,
        Error,
        Html,
        Image,
        File,
        Numeric
    }

    [DebuggerDisplay("Name={name}, DefaultValue={defaultValue}, Type={type}")]
    [AttributeUsage(AttributeTargets.Property)]
    [Serializable]
    public class FileFormField : FormField
    {
        public string AllowedFileTypes { get; set; }

        public FileFormField(bool localized = false, string allowedFileTypes = "")
            :base(localized, FormFieldTypeEnum.Image)
        {
            this.AllowedFileTypes = allowedFileTypes;
        }
    }

    [DebuggerDisplay("Name={name}, DefaultValue={defaultValue}, Type={type}")]
    [AttributeUsage(AttributeTargets.Property)]
    [Serializable]
    public class ImageFormField : FileFormField
    {
    }

    [DebuggerDisplay("Name={name}, DefaultValue={defaultValue}, Type={type}")]
	[AttributeUsage(AttributeTargets.Property)]
    [Serializable]
    public class FormField : System.Attribute, ITable
    {
        int formId = 0;
        bool enabled = true;
        string group = "";
        string name = "";
        string defaultValue = "";
        int minValue = 0;
        int maxValue = 0;
        int rows = 0;
        int cols = 0;
        string labelValue = "";
        string description = "";
        string cssClass = "";
        string cssStyle = "";
        List<FormFieldOption> options = new List<FormFieldOption>();
        FormFieldTypeEnum type = FormFieldTypeEnum.Text;
        bool localized = false;

        //public FormField() { }

        public FormField(
            bool localized = false,
            FormFieldTypeEnum type = FormFieldTypeEnum.Text,
            string value = "")
        {
            this.localized = localized;
            this.type = type;
            this.defaultValue = value;

            if (this.type == FormFieldTypeEnum.Combo
                || this.type == FormFieldTypeEnum.List)
            {
                this.options = new List<FormFieldOption>();
                if (!string.IsNullOrEmpty(this.defaultValue))
                {
                    var list = new List<string>(this.defaultValue.Split(';'));
                    foreach(string s in list)
                    {
                        options.Add(new FormFieldOption(s, s));
                    }
                }
            }
        }

        public int Id { get; set; } //used when from db

        public int FormId
        {
            [DebuggerStepThrough()]
            get { return formId; }
            [DebuggerStepThrough()]
            set { formId = value; }
        }

        public bool Enabled
        {
            [DebuggerStepThrough()]
            get { return enabled; }
            [DebuggerStepThrough()]
            set { enabled = value; }
        }

        public string Group
        {
            [DebuggerStepThrough()]
            get { return group; }
            [DebuggerStepThrough()]
            set { group = value; }
        }

        public string Name
        {
            [DebuggerStepThrough()]
            get { return name; }
            [DebuggerStepThrough()]
            set { name = value; }
        }

        public string DefaultValue
        {
            [DebuggerStepThrough()]
            get { return defaultValue; }
            [DebuggerStepThrough()]
            set { defaultValue = value; }
        }

        public int MinValue
        {
            [DebuggerStepThrough()]
            get { return minValue; }
            [DebuggerStepThrough()]
            set { minValue = value; }
        }

        public int MaxValue
        {
            [DebuggerStepThrough()]
            get { return maxValue; }
            [DebuggerStepThrough()]
            set { maxValue = value; }
        }

        public int Rows
        {
            [DebuggerStepThrough()]
            get { return rows; }
            [DebuggerStepThrough()]
            set { rows = value; }
        }

        public int Cols
        {
            [DebuggerStepThrough()]
            get { return cols; }
            [DebuggerStepThrough()]
            set { cols = value; }
        }

        public string LabelValue
        {
            [DebuggerStepThrough()]
            get { return labelValue; }
            [DebuggerStepThrough()]
            set { labelValue = value; }
        }

        public string Description
        {
            [DebuggerStepThrough()]
            get { return description; }
            [DebuggerStepThrough()]
            set { description = value; }
        }

        public string CssClass
        {
            [DebuggerStepThrough()]
            get { return cssClass; }
            [DebuggerStepThrough()]
            set { cssClass = value; }
        }

        public string CssStyle
        {
            [DebuggerStepThrough()]
            get { return cssStyle; }
            [DebuggerStepThrough()]
            set { cssStyle = value; }
        }

        public List<FormFieldOption> Options
        {
            get 
            {
                if (options == null)
                {
                    //20170309 NEVER USED
                    //null when from db
                    //var man = new FormFieldOptionsManager();
                    //var filter = new FormFieldOptionFilter();
                    //filter.FormFieldId = this.Id;
                    //options = man.GetByFilter(filter, "");
                }
                return options; 
            }
            [DebuggerStepThrough()]
            set { options = value; }
        }

        public FormFieldTypeEnum Type
        {
            [DebuggerStepThrough()]
            get { return type; }
            [DebuggerStepThrough()]
            set { type = value; }
        }

        /// <summary>
        /// this field will be splitted for different cultures
        /// </summary>
        public bool Localized
        {
            [DebuggerStepThrough()]
            get { return localized; }
            [DebuggerStepThrough()]
            set { localized = value; }
        }            
    }


    [Serializable]
    public class FormFieldFilter
    {
        private int id = 0;
        private int formId = 0;
        private string name = "";
        private string namePart = "";
        private Utility.TristateBool enabled = Utility.TristateBool.NotSet;

        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        public int FormId
        {
            [DebuggerStepThrough()]
            get { return formId; }
            [DebuggerStepThrough()]
            set { formId = value; }
        }

        public string Name
        {
            [DebuggerStepThrough()]
            get { return name; }
            [DebuggerStepThrough()]
            set { name = value; }
        }

        public string NamePart
        {
            [DebuggerStepThrough()]
            get { return namePart; }
            [DebuggerStepThrough()]
            set { namePart = value; }
        }

        public Utility.TristateBool Enabled
        {
            [DebuggerStepThrough()]
            get { return enabled; }
            [DebuggerStepThrough()]
            set { enabled = value; }
        }
    }


    [Serializable]
    public class FormFieldOption : ITableWithOrdering
    {
        public FormFieldOption()
        {
        }

        public FormFieldOption(string label, string value)
        {
            this.label = label;
            this.value = value;
        }


        private int id = 0;
        [DataObjectField(false)]
        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        private int formFieldId = 0;
        [DataObjectField(false)]
        public int FormFieldId
        {
            [DebuggerStepThrough()]
            get { return formFieldId; }
            [DebuggerStepThrough()]
            set { formFieldId = value; }
        }

        private string label = "";
        public string Label
        {
            [DebuggerStepThrough()]
            get { return label; }
            [DebuggerStepThrough()]
            set { label = value; }
        }

        private string value = "";
        public string Value
        {
            [DebuggerStepThrough()]
            get { return this.value; }
            [DebuggerStepThrough()]
            set { this.value = value; }
        }

        private int ordering = 0;
        public int Ordering
        {
            [DebuggerStepThrough()]
            get { return ordering; }
            [DebuggerStepThrough()]
            set { ordering = value; }
        }
    }


    [Serializable]
    public class FormFieldOptionFilter
    {
        private int id = 0;
        private int formFieldId = 0;

        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        public int FormFieldId
        {
            [DebuggerStepThrough()]
            get { return formFieldId; }
            [DebuggerStepThrough()]
            set { formFieldId = value; }
        }
    }
}

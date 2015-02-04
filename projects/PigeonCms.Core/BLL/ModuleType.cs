using System;
using System.Diagnostics;
using System.Collections.Generic;


namespace PigeonCms
{
    public class ModuleType: XmlType
    {
        public class EditContent
        {
            private string menuType = "";
            private string menuName = "";
            private string moduleFullName = "";
            private List<string> editParamsList = new List<string>();

            public EditContent() { }

            public EditContent(string menuType, string menuName,
                string moduleFullName, List<string> editParamsList)
            {
                this.menuType = menuType;
                this.menuName = menuName;
                this.moduleFullName = moduleFullName;
                this.editParamsList = editParamsList;
            }

            public string MenuType
            {
                [DebuggerStepThrough()]
                get { return this.menuType; }
                [DebuggerStepThrough()]
                set { this.menuType = value; }
            }

            public string MenuName
            {
                [DebuggerStepThrough()]
                get { return this.menuName; }
                [DebuggerStepThrough()]
                set { this.menuName = value; }
            }

            public string ModuleFullName
            {
                [DebuggerStepThrough()]
                get { return this.moduleFullName; }
                [DebuggerStepThrough()]
                set { this.moduleFullName = value; }
            }

            public List<string> EditParamsList
            {
                [DebuggerStepThrough()]
                get { return this.editParamsList; }
                [DebuggerStepThrough()]
                set { this.editParamsList = value; }
            }
        }

        public ModuleType() { }

        private string templateBlockName = "";
        public string TemplateBlockName
        {
            [DebuggerStepThrough()]
            get { return templateBlockName; }
            [DebuggerStepThrough()]
            set { templateBlockName = value; }
        }

        private List<string> views = new List<string>();
        public List<string> Views
        {
            [DebuggerStepThrough()]
            get { return views; }
            [DebuggerStepThrough()]
            set { views = value; }
        }

        private EditContent editContentTag = new EditContent();
        public EditContent EditContentTag
        {
            [DebuggerStepThrough()]
            get { return editContentTag; }
            [DebuggerStepThrough()]
            set { editContentTag = value; }
        }
        
    }


    [Serializable]
    public class ModuleTypeFilter: XmlTypeFilter 
    {
        private string templateBlockName = "";
        public string TemplateBlockName
        {
            [DebuggerStepThrough()]
            get { return templateBlockName; }
            [DebuggerStepThrough()]
            set { templateBlockName = value; }
        }
    }
}
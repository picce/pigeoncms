using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

/// <summary>
/// Base class for module controls
/// </summary>
/// 
namespace PigeonCms
{
    /// <summary>
    /// Use this attribute to specify that a static method should be exposed as an AJAX PageMethod call
    /// through the owning page.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class UserControlScriptMethodAttribute : System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserControlScriptMethodAttribute"/> class.
        /// </summary>
        public UserControlScriptMethodAttribute() { }
    }

    public class BaseModuleControl : System.Web.UI.UserControl
    {
        private List<ResLabel> labelsList;
        private Menu menuTarget = null;

        #region properties

        private bool standAlone = false;
        /// <summary>
        /// set to true to use as standalone control
        /// eg. in EngineBasePage pages
        /// </summary>
        public bool StandAlone
        {
            get { return standAlone; }
            set { standAlone = value; }
        }

        /// <summary>
        /// int pkey, current record Id (usually used by admin modules to keep current record state)
        /// </summary>
        protected int CurrentId
        {
            get
            {
                int res = 0;
                if (ViewState["CurrentId"] != null)
                    res = (int)ViewState["CurrentId"];
                return res;
            }
            set { ViewState["CurrentId"] = value; }
        }

        /// <summary>
        /// string pkey, current record Id (usually used by admin modules to keep current record state)
        /// </summary>
        protected string CurrentKey
        {
            get
            {
                string res = "";
                if (ViewState["CurrentKey"] != null)
                    res = (string)ViewState["CurrentKey"];
                return res;
            }
            set { ViewState["CurrentKey"] = value; }
        }

        public PigeonCms.Module BaseModule { get; set; }

        
        private PigeonCms.Menu currMenu = new PigeonCms.Menu();
        public PigeonCms.Menu CurrMenu 
        {
            get { return currMenu; }
            set { currMenu = value; } 
        }



        /// <summary>
        /// absolute path of current module 
        /// eg: /modules/pigeoncms.item/views/
        /// </summary>
        public string CurrModulePath
        {
            get
            {
                return VirtualPathUtility.ToAbsolute(
                    Config.ModulesPath + this.BaseModule.ModuleFullName) + "/views/";
            }
        }

        /// <summary>
        /// absolute path of current module resources folder
        /// eg: /modules/pigeoncms.item/views/myview
        /// </summary>
        public string CurrViewPath
        {
            get 
            {
                return CurrModulePath + this.BaseModule.CurrViewFolder + "/";
            }
        }

        /// <summary>
        /// dictionary list to use in module admin area (combo)
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetCssFiles(/*string controlFullName, string currView*/)
        {
            throw new NotImplementedException("Not used, css files depends by the current view");

            //Dictionary<string, string> res = new Dictionary<string, string>();
            //if (!string.IsNullOrEmpty(this.BaseModule.CurrViewFolder))
            //{
            //    try
            //    {
            //        DirectoryInfo dir = new DirectoryInfo(this.CurrViewPath);
            //        if (dir.Exists)
            //        {
            //            FileInfo[] files = dir.GetFiles("*.css");
            //            foreach (FileInfo file in files)
            //            {
            //                res.Add(file.Name, file.Name);
            //            }
            //        }
            //    }
            //    finally
            //    {
            //    }
            //}
            //return res;
        }

        public Dictionary<string, string> Params
        {
            get
            {
                string[] splitter = { ":=" };
                var paramsDict = new Dictionary<string, string>();
                if (this.BaseModule != null)
                {
                    List<string> paramsList = Utility.String2List(this.BaseModule.ModuleParams);
                    foreach (string item in paramsList)
                    {
                        string[] arr = item.Split(splitter, StringSplitOptions.None);
                        string key = arr[0];
                        string value = "";
                        if (arr.Length > 1) value = arr[1];
                        if (!string.IsNullOrEmpty(key))
                            paramsDict.Add(key, value);
                    }
                }
                return paramsDict;
            }
        }

        #endregion


        #region methods

        public BaseModuleControl(){}

        //read param from module params
        protected string GetStringParam(string paramName, string defaultValue)
        {
            return GetStringParam(paramName, defaultValue, "");
        }

        /// <summary>
        /// read param in order of priority from:
        /// -current Context.Items collection
        /// -querystring
        /// -from module params
        /// </summary>
        /// <param name="paramName">param name</param>
        /// <param name="defaultValue">default value</param>
        /// <param name="requestParamName">context or querystring param name</param>
        /// <returns>string param value</returns>
        protected string GetStringParam(string paramName, string defaultValue, string requestParamName)
        {
            //design param
            string res = defaultValue;

            //backend param 
            //20111031 works on droidcatalogue
            string backendValue = defaultValue;
            {
                this.Params.TryGetValue(paramName, out backendValue);
                if (string.IsNullOrEmpty(backendValue))
                    backendValue = defaultValue;
            }

            if (backendValue != defaultValue)
            {
                res = backendValue;
            }
            else
            {
                if (!string.IsNullOrEmpty(requestParamName))
                {
                    //context param
                    if (Context.Items[requestParamName] != null)
                    {
                        res = Context.Items[requestParamName].ToString().Replace(".aspx", "");
                    }
                    else if (Request[requestParamName] != null)  //querystring param
                    {
                        res = Request[requestParamName].ToString();
                    }
                }
            }
            
            //if (res == defaultValue)    //##20100302
            //{
            //    //backend param 
            //    if (!this.Params.TryGetValue(paramName, out res))
            //        res = defaultValue;
            //}
            return res;
        }

        //read param from module params
        protected int GetIntParam(string paramName, int defaultValue)
        {
            return GetIntParam(paramName, defaultValue, "");
        }

        /// <summary>
        /// read param in order of priority from:
        /// -from backend module params
        /// -current Context.Items collection
        /// -querystring
        /// </summary>
        /// <param name="paramName">param name</param>
        /// <param name="defaultValue">default value</param>
        /// <param name="requestParamName">context or querystring param name</param>
        /// <returns>int param value</returns>
        protected int GetIntParam(string paramName, int defaultValue, string requestParamName)
        {
            //design param
            int res = defaultValue; 
            
            //backend param 
            //20111031 works on droidcatalogue
            int backendValue = defaultValue;
            {
                string parValue = "";
                if (this.Params.TryGetValue(paramName, out parValue))
                {
                    int.TryParse(parValue, out backendValue);
                }
            }

            if (backendValue != defaultValue)
                res = backendValue;
            else
            {
                if (!string.IsNullOrEmpty(requestParamName))
                {
                    //context param
                    if (Context.Items[requestParamName] != null)
                    {
                        int.TryParse(Context.Items[requestParamName].ToString().Replace(".aspx", ""), out res);
                    }
                    else if (Request[requestParamName] != null) //querystring param
                    {
                        int.TryParse(Request[requestParamName].ToString(), out res);
                    }
                }
            }

            //if (res == defaultValue)
            //{
            //    //backend param 
            //    string parValue = "";
            //    if (this.Params.TryGetValue(paramName, out parValue))
            //    {
            //        int.TryParse(parValue, out res);
            //    }
            //}
            return res;
        }

        protected bool GetBoolParam(string paramName, bool defaultValue)
        {
            bool res = defaultValue;
            string parValue = "";
            if (this.Params.TryGetValue(paramName, out parValue))
            {
                if (parValue == "0")
                    res = false;
                if (parValue == "1")
                    res = true;
            }
            return res;
        }

        protected string GetLabel(string resourceId, string defaultValue, Control targetControl, bool getTitle)
        {
            string title = "";
            if (getTitle)
            {
                title = GetLabel(resourceId + "Description");
            }
            string res = GetLabel(resourceId, defaultValue, targetControl, title);
            return res;
        }

        protected string GetLabel(string resourceId, string defaultValue, Control targetControl, string title)
        {
            bool visible = true;
            if (!string.IsNullOrEmpty(title))
                title = "title='" + title + "'";
            string clientID = "";
            if (targetControl != null)
            {
                clientID = targetControl.ClientID;
                visible = targetControl.Visible;
            }
            string res = GetLabel(resourceId, defaultValue);
            res = "<label for='" + clientID + "' " + title + ">" + res + "</label>";
            if (!visible) res = "";
            return res;
        }

        protected string GetLabel(string resourceId, string defaultValue, bool getTitle)
        {
            return GetLabel(resourceId, defaultValue, null, getTitle);
        }

        protected string GetLabel(string resourceId, string defaultValue, Control targetControl)
        {
            return GetLabel(resourceId, defaultValue, targetControl, "");
        }

        protected string GetLabel(string resourceId)
        {
            return GetLabel(resourceId, "");
        }

        private bool? isMobileDevice = null;
        protected bool? IsMobileDevice
        {
            get
            {
                if (isMobileDevice == null)
                    isMobileDevice = Utility.Mobile.IsMobileDevice(Context);
                return isMobileDevice;
            }

        }

        /// <summary>
        /// retrieve localized label value
        /// </summary>
        public string GetLabel(string resourceId, string defaultValue)
        {
            return GetLabel(resourceId, defaultValue, ContentEditorProvider.Configuration.EditorTypeEnum.Text);
        }

        /// <summary>
        /// retrieve localized label value
        /// </summary>
        /// <param name="resourceId">the label key</param>
        /// <returns>the localized label value</returns>
        public string GetLabel(
            string resourceId, 
            string defaultValue,
            ContentEditorProvider.Configuration.EditorTypeEnum textMode)

        {
            string res = "";
            if (labelsList == null)
            {
                //preload all labels of current moduletype
                labelsList = LabelsProvider.GetLabelsByResourceSet(this.BaseModule.ModuleFullName);
            }
            res = LabelsProvider.GetLocalizedLabelFromList(
                this.BaseModule.ModuleFullName,
                labelsList, 
                resourceId, 
                defaultValue, 
                textMode, "");
            if (string.IsNullOrEmpty(res))
            {
                res = defaultValue;
            }
            if (HttpContext.Current.Request.QueryString["tp"] == "1")
            {
                res = "[" + resourceId + "]" + res;
            }
            return res;
        }

        /// <summary>
        /// create link between current entry and another menu entry
        /// </summary>
        /// <param name="queryStringParamName">ex. itemid or itemname</param>
        /// <param name="queryStringParamValue">ex. 7 or 13 or cheese or apple</param>
        /// <param name="targetMenuId">id of target menu entry</param>
        /// <returns>the anchor herf attribute</returns>
        protected string GetLinkAddress(string queryStringParamName, string queryStringParamValue, int targetMenuId)
        {
            string res = "javascript:void(0);";

            if (menuTarget == null)
            {
                menuTarget = new MenuManager().GetByKey(targetMenuId);
            }

            if (!string.IsNullOrEmpty(menuTarget.RoutePattern))
            {
                res = Utility.GetRoutedUrl(
                    menuTarget, queryStringParamName + "=" + queryStringParamValue, 
                    Config.AddPageSuffix);
            }
            //else
            //{
            //    if (!string.IsNullOrEmpty(this.DetailHandlerPath))
            //    {
            //        res = VirtualPathUtility.ToAbsolute(this.DetailHandlerPath) + "?" + queryStringParamName + "=" + queryStringParamValue;
            //    }
            //}
            return res;
        }

        protected string RenderAccessTypeSummary(ITableWithPermissions item, string readLabel = "", string writeLabel = "")
        {
            string res = "";

            //permissions
            //read
            string readAccessLevel = item.ReadAccessCode;
            if (item.ReadAccessLevel > 0)
                readAccessLevel += " " + item.ReadAccessLevel.ToString();
            if (!string.IsNullOrEmpty(readAccessLevel))
                readAccessLevel = " - " + readAccessLevel;

            //write
            string writeAccessLevel = item.WriteAccessCode;
            if (item.WriteAccessLevel > 0)
                writeAccessLevel += " " + item.WriteAccessLevel.ToString();
            if (!string.IsNullOrEmpty(writeAccessLevel))
                writeAccessLevel = " - " + writeAccessLevel;

            res += readLabel + item.ReadAccessType.ToString();
            if (item.ReadAccessType != MenuAccesstype.Public)
            {
                string roles = "";
                foreach (string role in item.ReadRolenames)
                {
                    roles += role + ", ";
                }
                if (roles.EndsWith(", ")) roles = roles.Substring(0, roles.Length - 2);
                if (roles.Length > 0)
                    roles = " (" + roles + ")";
                res += Utility.Html.GetTextPreview(roles, 60, "");
                res += readAccessLevel;
            }
            if (res != "") res += "<br />";

            //write
            res += writeLabel + item.WriteAccessType.ToString();
            if (item.WriteAccessType != MenuAccesstype.Public)
            {
                string roles = "";
                foreach (string role in item.WriteRolenames)
                {
                    roles += role + ", ";
                }
                if (roles.EndsWith(", ")) roles = roles.Substring(0, roles.Length - 2);
                if (roles.Length > 0)
                    roles = " (" + roles + ")";
                res += Utility.Html.GetTextPreview(roles, 60, "");
                res += writeAccessLevel;
            }

            return res;
        }

        private BaseMasterPage currentMaster = null;
        protected BaseMasterPage CurrentMaster
        {
            get
            {
                if (currentMaster == null)
                {
                    currentMaster = (BaseMasterPage)this.Page.Master;
                }
                return currentMaster;
            }
        }

        public string RenderSuccess(string content)
        {
            return this.CurrentMaster.RenderSuccess(content);
        }

        public string RenderError(string content)
        {
            return this.CurrentMaster.RenderError(content);
        }

        /// <summary>
        /// Registers the user control web methods tagged with the UserControlWebMethodAttribute
        /// </summary>
        private void registerUserControlWebMethods()
        {
            foreach (MethodInfo method in this.GetType().GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod))
                if (method.GetCustomAttributes(typeof(UserControlScriptMethodAttribute), true).Length > 0)
                    registerUserControlWebMethod(method);

            Type baseType = this.GetType().BaseType;
            if (baseType != null && (baseType.Namespace == null || !baseType.Namespace.StartsWith("System")))
                foreach (MethodInfo method in baseType.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod))
                    if (method.GetCustomAttributes(typeof(UserControlScriptMethodAttribute), true).Length > 0)
                        registerUserControlWebMethod(method);
        }

        /// <summary>
        /// Registers a user control web method based on the methodInfo signature through reflection.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <remarks>AJAX Voodoo!</remarks>
        private void registerUserControlWebMethod(MethodInfo method)
        {
            string blockName = string.Concat(method.Name, "_webMethod_uc");

            StringBuilder funcBuilder = new StringBuilder();
            funcBuilder.Append("function ");
            funcBuilder.Append(method.Name);
            funcBuilder.Append("(successCallback,failureCallback");
            foreach (var par in method.GetParameters())
                funcBuilder.AppendFormat(",{0}", par.Name);
            funcBuilder.Append("){if(PageMethods.PageServiceRequest){try{var parms=[];for(var i=2;i<arguments.length;i++){parms.push(arguments[i]);}PageMethods.PageServiceRequest(");
            funcBuilder.AppendFormat("'{0}','{1}'", method.DeclaringType.AssemblyQualifiedName, method.Name);
            funcBuilder.Append(",parms,successCallback,failureCallback);}catch(e){alert(e.toString());}}}");

            ScriptManager.RegisterClientScriptBlock(this, GetType(), blockName, funcBuilder.ToString(), true);
        }

        #endregion


        #region events

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            registerUserControlWebMethods();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            string cssHref = "";

            //add css file
            if (BaseModule != null && !string.IsNullOrEmpty(BaseModule.CssFile))
            {
                cssHref = this.CurrViewPath + BaseModule.CssFile;
                Literal css1 = new Literal();
                css1.Text = "<link href='" + cssHref + "' rel='stylesheet' type='text/css' media='screen' />";
                Page.Header.Controls.Add(css1);
            }
        }

        #endregion
    }
}
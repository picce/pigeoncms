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
using System.Collections.Generic;
using System.IO;
using PigeonCms;
using System.Xml;
using System.Reflection;
using System.Web.Compilation;
//using FredCK.FCKeditorV2;


namespace PigeonCms
{
    /// <summary>
    /// static class with methods to help controls rendering, controls reading, etc..
    /// </summary>
    public static class FormBuilder
    {
        #region public methods

        public static Control RenderControl(FormField field, string currentValue)
        {
            return RenderControl(field, currentValue, "");
        }

        /// <summary>
        /// render the aspnet control parsing param, to admin module
        /// </summary>
        /// <param name="field"></param>
        /// <param name="currentValue"></param>
        /// <param name="defaultCssClass"></param>
        /// <returns></returns>
        public static Control RenderControl(FormField field, string currentValue, string defaultCssClass)
        {
            Control result = null;
            switch (field.Type)
            {
                case FormFieldTypeEnum.Text:
                case FormFieldTypeEnum.Numeric:
                case FormFieldTypeEnum.Html://20150805 - TOCHECK
                    result = (Control)getTextControl(field, currentValue, defaultCssClass);
                    break;
                case FormFieldTypeEnum.List:
                    result = (Control)getHiddenControl(field, currentValue);    //TODO
                    //result = (Control)getListControl(param, currentValue);
                    break;
                case FormFieldTypeEnum.Combo:
                    result = (Control)getComboControl(field, currentValue, defaultCssClass);
                    break;
                case FormFieldTypeEnum.Radio:
                    result = (Control)getHiddenControl(field, currentValue);    //TODO
                    break;
                case FormFieldTypeEnum.Check:
                    result = (Control)getHiddenControl(field, currentValue);    //TODO
                    break;
                case FormFieldTypeEnum.Calendar:
                    result = (Control)getHiddenControl(field, currentValue);    //TODO
                    break;
                case FormFieldTypeEnum.Custom:
                    result = (Control)getHiddenControl(field, currentValue);    //TODO
                    break;
                case FormFieldTypeEnum.Spacer:
                    result = (Control)getSpacerControl(field, currentValue);
                    break;
                case FormFieldTypeEnum.Hidden:
                    result = (Control)getHiddenControl(field, currentValue);
                    break;
                case FormFieldTypeEnum.Error:
                    result = (Control)getHiddenControl(field, currentValue);    //todo
                    break;
                //case FormFieldTypeEnum.Html:
                //    result = (Control)getHtmlControl(field, currentValue, defaultCssClass);
                //    break;
                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// render the html controls on panel
        /// </summary>
        /// <param name="panel">the container for generated controls</param>
        /// <param name="currentItem">current Item</param>
        public static void RenderParamsOnPanel(Panel panel, Item currentItem, List<ResLabel> labelsList)
        {
            PigeonCms.ItemType itemType = null;
            Dictionary<string, string> fieldsValues = new Dictionary<string,string>();
            PropertyInfo pi = null;
            Type type = null;

            try
            {
                type = BuildManager.GetType(currentItem.ItemTypeName, false);
                itemType = currentItem.ItemType;
                foreach (FormField currParam in itemType.Fields)
                {
                    object value = null;
                    string baseClassFieldName = "";
                    string paramName = currParam.Name;
                    string culture = "";
                    //if (currParam.IsTranslationField)
                    //{
                    //    int sepIdx = paramName.LastIndexOf("__");
                    //    if (sepIdx > 0)
                    //    {
                    //        culture = paramName.Substring(sepIdx + 2);
                    //        paramName = paramName.Substring(0, sepIdx);
                    //    }
                    //}
                    pi = type.GetProperty(paramName);    //inerithed class field
                    if (pi != null)
                    {
                        foreach (ItemFieldMapAttribute attr in pi.GetCustomAttributes(typeof(ItemFieldMapAttribute), false))
                        {
                            if (attr != null)
                            {
                                baseClassFieldName = attr.FieldName.ToString(); //base class mapped field
                                break;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(baseClassFieldName))
                    {
                        pi = type.GetProperty(baseClassFieldName); //base class field
                        if (pi != null)
                        {
                            value = pi.GetValue(currentItem, null);
                            if (value != null)
                            {
                                string stringValue = value.ToString();
                                //if (currParam.IsTranslationField && !string.IsNullOrEmpty(culture))
                                //{
                                //    if (value.GetType() == typeof(Dictionary<string, string>))
                                //    {
                                //        var dict = (Dictionary<string, string>)value;
                                //        dict.TryGetValue(culture, out stringValue);
                                //        if (stringValue == null)
                                //            stringValue = "";
                                //    }
                                //}
                                //else
                                //    stringValue = value.ToString();
                                fieldsValues.Add(currParam.Name, stringValue);
                            }
                        }
                    }
                }
                RenderParamsOnPanel(panel, fieldsValues, itemType.Fields, labelsList);
            }
            catch (Exception ex)
            {
                Tracer.Log("RenderParamsOnPanel(): error loading item fields", TracerItemType.Error, ex);
            }
        }

        public static void RenderParamsOnPanel(Panel panel, Dictionary<int, string> fieldsValues,
            List<FormField> fieldsList, List<ResLabel> labelsList)
        {
            var values = new Dictionary<string, string>();
            foreach (var item in fieldsValues)
            {
                values.Add(item.Key.ToString(), item.Value);
            }
            RenderParamsOnPanel(panel, values, fieldsList, labelsList);
        }

        /// <summary>
        /// render the html controls on panel
        /// </summary>
        /// <param name="panel">the container for generated controls</param>
        /// <param name="fieldsValues">list of params keys and values</param>
        /// <param name="fieldsList">fields list for the current item (from xml file)</param>
        public static void RenderParamsOnPanel(Panel panel, Dictionary<string, string> fieldsValues,
            List<FormField> fieldsList, List<ResLabel> labelsList)
        {
            string currGroup = "not set";  //current param group label
            Panel panelContainer = new Panel();
            Panel panelBody = new Panel();

            foreach (FormField currParam in fieldsList)
            {
                //add a pane foreach different param group
                if (currParam.Group != currGroup)
                {
                    currGroup = currParam.Group;
                    var litGroupName = new Literal();
                    if (string.IsNullOrEmpty(currParam.Group))
                    {
                        litGroupName.Text = "<div class='panel-heading'>" + Utility.GetLabel("LblModule") + "</div>";
                    }
                    else
                    {
                        litGroupName.Text = "<div class='panel-heading'>" + currParam.Group + "</div>";
                    }

                    panelContainer = new Panel();   //render table
                    panelContainer.CssClass = "panel panel-info";
                    panelContainer.Controls.Add(litGroupName);

                    panelBody = new Panel();
                    panelBody.CssClass = "panel-body";
                    panelContainer.Controls.Add(panelBody);
                    panel.Controls.Add(panelContainer);
                }

                //add params to current pane
                Control control2Add = null;
                try
                {
                    string currentValue = "";
                    if (currParam.Id > 0)
                    {
                        if (fieldsValues.ContainsKey(currParam.Id.ToString()))
                            currentValue = fieldsValues[currParam.Id.ToString()];
                    }
                    else
                    {
                        if (fieldsValues.ContainsKey(currParam.Name))
                            currentValue = fieldsValues[currParam.Name];
                    }
                    control2Add = FormBuilder.RenderControl(currParam, currentValue, "form-control");
                }
                catch (Exception)
                { }
                finally
                {
                    if (control2Add == null)
                    {
                        Literal litErrorParsing = new Literal();
                        litErrorParsing.Text = Utility.GetErrorLabel("ParamParsing", "Error parsing [" + currParam.Name + "] param");
                        control2Add = (Control)litErrorParsing;
                    }
                }
                addTableRow(panelBody, currParam, control2Add, labelsList);
            }
        }

        /// <summary>
        /// Get the string with params value to store in db reading controls inside container
        /// </summary>
        /// <param name="moduleParams">list of moduleParam</param>
        /// <param name="container">the parent control</param>
        /// <returns>the moduleParams string</returns>
        public static string GetParamsString(List<FormField> formFieldList, Control container)
        {
            string res = "";
            string currValue = "";
            foreach (FormField currField in formFieldList)
            {
                currValue = getControlValue(currField, container);

                if (!string.IsNullOrEmpty(currField.Name))
                    res += currField.Name + ":=" + currValue + "|";
            }
            if (res.Length > 1)
            {
                if (res[res.Length - 1] == '|')
                    res = res.Substring(0, res.Length - 1);
            }
            return res;
        }

        /// <summary>
        /// Get the string with params value to store in db reading controls inside container
        /// </summary>
        /// <param name="moduleParams">list of moduleParam</param>
        /// <param name="container">the parent control</param>
        /// <returns>the moduleParams string</returns>
        public static Dictionary<int, string> GetParamsDictionary(List<FormField> formFieldList, Control container)
        {
            var res = new Dictionary<int,string>();
            string currValue = "";
            foreach (FormField currField in formFieldList)
            {
                currValue = getControlValue(currField, container);

                if (!string.IsNullOrEmpty(currField.Name))
                    res.Add(currField.Id, currValue);
            }
            return res;
        }

        public static FormField GetFormFieldFromXmlNode(XmlNode nodeParam, XmlNode nodeParams)
        {
            FormField item = new FormField();
            try
            {
                item.Type = (FormFieldTypeEnum)Enum.Parse(
                    typeof(FormFieldTypeEnum),
                    nodeParam.Attributes["type"].Value, true);
            }
            catch (Exception)
            {
                item.Type = FormFieldTypeEnum.Error;
            }

            if (nodeParam.Attributes["name"] != null)
            {
                item.Name = nodeParam.Attributes["name"].Value;
            }
            if (nodeParam.Attributes["default"] != null)
            {
                item.DefaultValue = nodeParam.Attributes["default"].Value;
            }
            if (nodeParam.Attributes["rows"] != null)
            {
                int value = 0;
                int.TryParse(nodeParam.Attributes["rows"].Value, out value);
                item.Rows = value;
            }
            if (nodeParam.Attributes["cols"] != null)
            {
                int value = 0;
                int.TryParse(nodeParam.Attributes["cols"].Value, out value);
                item.Cols = value;
            }
            if (nodeParam.Attributes["label"] != null)
            {
                item.LabelValue = nodeParam.Attributes["label"].Value;
            }
            if (nodeParam.Attributes["description"] != null)
            {
                item.Description = nodeParam.Attributes["description"].Value;
            }
            if (nodeParam.Attributes["cssStyle"] != null)
            {
                item.CssStyle = nodeParam.Attributes["cssStyle"].Value;
            }
            if (nodeParam.Attributes["cssClass"] != null)
            {
                item.CssClass = nodeParam.Attributes["cssClass"].Value;
            }
            if (nodeParams.Attributes["group"] != null)
            {
                item.Group = nodeParams.Attributes["group"].Value;
            }

            //options list
            XmlNodeList optionsList = nodeParam.SelectNodes("option");
            foreach (XmlNode nodeOption in optionsList)
            {
                string label = "";
                string value = "";
                if (nodeOption.Attributes["label"] != null)
                {
                    label = nodeOption.Attributes["label"].Value;
                }
                if (nodeOption.Attributes["value"] != null)
                {
                    value = nodeOption.Attributes["value"].Value;
                }
                item.Options.Add(new FormFieldOption(label, value));
            }
            //other options from datasource list
            XmlNodeList dsList = nodeParam.SelectNodes("datasource");
            foreach (XmlNode ds in dsList)
            {
                try
                {
                    parseTagDatasource(ds, item);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(
                        "Parsing field " + item.Name + " datasource typename ", ex);
                }
            }
            //other options from enum datasource list
            XmlNodeList enumList = nodeParam.SelectNodes("enum");
            foreach (XmlNode ds in enumList)
            {
                try
                {
                    parseTagEnum(ds, item);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(
                        "Parsing field " + item.Name + " enum typename ", ex);
                }
            }
            return item;
        }

        public static string GetControlValue(FormField currField, Control container)
        {
            return getControlValue(currField, container);
        }

        #endregion


        #region private methods

        private static string getControlValue(FormField currField, Control container)
        {
            string res = "";
            switch (currField.Type)
            {
                case FormFieldTypeEnum.Text:
                case FormFieldTypeEnum.Numeric:
                {
                    var t1 = new TextBox();
                    t1 = Utility.FindControlRecursive<TextBox>(container, t1.GetType().Name + currField.Name);
                    if (t1 != null)
                        res = t1.Text;
                }
                break;

                case FormFieldTypeEnum.Html:
                {
                    //var t1 = new FCKeditor();
                    var t1 = new TextBox();
                    t1 = Utility.FindControlRecursive<TextBox /*FCKeditor*/>(container, t1.GetType().Name + currField.Name);
                    if (t1 != null)
                        res = t1.Text;
                }
                break;

                case FormFieldTypeEnum.Combo:
                {
                    var t1 = new DropDownList();
                    t1 = Utility.FindControlRecursive<DropDownList>(container, t1.GetType().Name + currField.Name);
                    if (t1 != null)
                        res = t1.SelectedValue;
                }
                break;

                case FormFieldTypeEnum.Hidden:
                case FormFieldTypeEnum.Error:
                {
                    var t1 = new HiddenField();
                    t1 = Utility.FindControlRecursive<HiddenField>(container, t1.GetType().Name + currField.Name);
                    if (t1 != null)
                        res = t1.Value;
                }
                break;
            }
            return res;
        }

        private static Literal getSpacerControl(FormField param, string currentValue)
        {
            Literal ctrl = new Literal();
            //ctrl.ID = ctrl.GetType().ToString() + param.Name;
            if (currentValue == null)
                ctrl.Text = param.DefaultValue;
            else
                ctrl.Text = currentValue;
            return ctrl;
        }

        private static TextBox getTextControl(FormField param, string currentValue)
        {
            return getTextControl(param, currentValue, "");
        }

        private static TextBox getTextControl(FormField param, string currentValue, string defaultCssClass)
        {
            TextBox ctrl = new TextBox();
            ctrl.ID = ctrl.GetType().Name + param.Name;
            ctrl.ToolTip = param.Description;
            ctrl.Style.Value = param.CssStyle;
            ctrl.CssClass = defaultCssClass + " " + param.CssClass;
            if (string.IsNullOrEmpty(currentValue))
                ctrl.Text = param.DefaultValue;
            else
                ctrl.Text = currentValue;
            if (param.Rows > 0) //textarea
            {
                ctrl.TextMode = TextBoxMode.MultiLine;
                ctrl.Rows = param.Rows;
                if (param.Cols > 0) ctrl.Columns = param.Cols;

            }
            //txt1.MaxLength = 50;
            //txt1.Width = new Unit(255);
            return ctrl;
        }

        //private static FCKeditor getHtmlControl(FormField param, string currentValue)
        //{
        //    return getHtmlControl(param, currentValue, "");
        //}

        //private static FCKeditor getHtmlControl(FormField param, string currentValue, string defaultCssClass)
        //{
        //    FCKeditor ctrl = new FCKeditor();
        //    ctrl.ID = ctrl.GetType().Name + param.Name;
        //    ctrl.ToolbarStartExpanded = false;
        //    ctrl.ToolbarSet = "Basic";

        //    //if (!string.IsNullOrEmpty(param.CssClass))
        //    //    ctrl.EditorAreaCSS = param.CssClass;
        //    //else
        //    //    ctrl.EditorAreaCSS = defaultCssClass;//"adminMediumText";

        //    if (string.IsNullOrEmpty(currentValue))
        //        ctrl.Value = param.DefaultValue;
        //    else
        //        ctrl.Value = currentValue;

        //    if (param.Rows > 0)
        //        ctrl.Height = new Unit(100 * param.Rows);
        //    if (param.Cols > 0)
        //        ctrl.Width = new Unit(10 * param.Cols);

        //    return ctrl;
        //}

        private static DropDownList getComboControl(FormField param, string currentValue)
        {
            return getComboControl(param, currentValue, "");
        }

        private static DropDownList getComboControl(FormField param, string currentValue, string defaultCssClass)
        {
            DropDownList ctrl = new DropDownList();
            ctrl.ID = ctrl.GetType().Name + param.Name;
            ctrl.ToolTip = param.Description;
            ctrl.Style.Value = param.CssStyle;
            ctrl.CssClass = defaultCssClass + " " + param.CssClass;
            foreach (FormFieldOption item in param.Options)
            {
                ctrl.Items.Add(new ListItem(item.Label, item.Value));
                if (string.IsNullOrEmpty(currentValue))
                {
                    if (ctrl.Items[ctrl.Items.Count - 1].Value == param.DefaultValue)
                        ctrl.Items[ctrl.Items.Count - 1].Selected = true;
                }
                else
                {
                    if (ctrl.Items[ctrl.Items.Count - 1].Value == currentValue)
                        ctrl.Items[ctrl.Items.Count - 1].Selected = true;
                }
            }
            return ctrl;
        }

        private static HiddenField getHiddenControl(FormField param, string currentValue)
        {
            HiddenField ctrl = new HiddenField();
            ctrl.ID = ctrl.GetType().Name + param.Name;
            if (string.IsNullOrEmpty(currentValue))
                ctrl.Value = param.DefaultValue;
            else
                ctrl.Value = currentValue;
            return ctrl;
        }

        //private static TextBox getListControl(ModuleParam param, string currentValue)
        //{
        //    ListBox ctrl = new ListBox();
        //    ctrl.ID = ctrl.GetType().Name + param.Name;
        //    ctrl.ToolTip = param.Description;
        //    ctrl.CssClass = "adminMediumText";
        //    if (currentValue == null)
        //        ctrl.Text = param.DefaultValue;
        //    else
        //        ctrl.Text = currentValue;
        //    if (param.Rows > 0) //textarea
        //    {
        //        ctrl.TextMode = TextBoxMode.MultiLine;
        //        ctrl.Rows = param.Rows;
        //        if (param.Cols > 0) ctrl.Columns = param.Cols;

        //    }
        //    //txt1.MaxLength = 50;
        //    //txt1.Width = new Unit(255);
        //    return ctrl;
        //}

        /// <summary>
        /// parse datasource tag
        /// </summary>
        /// <param name="nodeDatasource"></param>
        /// <param name="item"></param>
        private static void parseTagDatasource(XmlNode nodeDatasource, FormField item)
        {
            string selectMethod = "";
            string selectParams = "";
            string typeName = "";

            if (nodeDatasource != null)
            {
                if (nodeDatasource.Attributes["SelectMethod"] != null)
                {
                    selectMethod = nodeDatasource.Attributes["SelectMethod"].Value;
                }
                if (nodeDatasource.Attributes["SelectParams"] != null)
                {
                    selectParams = nodeDatasource.Attributes["SelectParams"].Value;
                }
                if (nodeDatasource.Attributes["TypeName"] != null)
                {
                    typeName = nodeDatasource.Attributes["TypeName"].Value;
                }

                object obj;
                if (!string.IsNullOrEmpty(selectParams))
                {
                    var paramsList = Utility.String2List(selectParams);
                    object[] parameters = paramsList.ToArray();
                    //object[] parameters = new object[0];
                    //parameters[0] = selectParams;
                    obj = PigeonCms.Reflection.Process(typeName, selectMethod, parameters);
                }
                else
                {
                    obj = PigeonCms.Reflection.Process(typeName, selectMethod, null);
                }

                Dictionary<string, string> res = (Dictionary<string, string>)obj;
                foreach (KeyValuePair<string, string> pair in res)
                {
                    item.Options.Add(new FormFieldOption(pair.Value, pair.Key));
                }
            }
        }


        /// <summary>
        /// parse enum datasource tag
        /// </summary>
        /// <param name="nodeEnum"></param>
        /// <param name="item"></param>
        private static void parseTagEnum(XmlNode nodeEnum, FormField item)
        {
            string typeName = "";

            if (nodeEnum != null)
            {
                if (nodeEnum.Attributes["TypeName"] != null)
                {
                    typeName = nodeEnum.Attributes["TypeName"].Value;
                }

                var type = BuildManager.GetType(typeName, true);
                foreach (string name in Enum.GetNames(type))
                {
                    int value = (int)Enum.Parse(type, name);
                    item.Options.Add(new FormFieldOption(name, value.ToString()));
                }
            }
        }

        //private static void addTableRow(Panel tableParams, FormField currParam,
        //    Control control2Add, List<ResLabel> labelsList)
        //{
        //    var controls2Add = new List<Control>();
        //    controls2Add.Add(control2Add);
        //    addTableRow(tableParams, currParam, controls2Add, labelsList); 
        //}

        /// <summary>
        /// add a row with the label and the control for the current param
        /// </summary>
        /// <param name="tableParams">table in which add the row</param>
        /// <param name="currParam">ModuleParam instance of the param to add</param>
        /// <param name="control2Add">control to add</param>
        private static void addTableRow(Panel tableParams, FormField currParam,
            Control control2Add, List<ResLabel> labelsList)
        {
            //HtmlTableRow row = new HtmlTableRow();
            //HtmlTableCell cell1 = new HtmlTableCell();
            //HtmlTableCell cell2 = new HtmlTableCell();
            Literal litParamError = new Literal();
            Label lblParamLabel = new Label();

            //litParamLabel.Text = "<label for='"+ control2Add.ClientID +"' title='"+ currParam.Description +"'>"+ currParam.LabelValue + "</label>";
            lblParamLabel.Text = LabelsProvider.GetLocalizedVarFromList(labelsList, currParam.LabelValue); //currParam.LabelValue;
            lblParamLabel.ToolTip = LabelsProvider.GetLocalizedVarFromList(labelsList, currParam.Description); //currParam.Description;
            lblParamLabel.AssociatedControlID = control2Add.ClientID;

            tableParams.Controls.Add(lblParamLabel);
            tableParams.Controls.Add(control2Add);
            if (currParam.Type == FormFieldTypeEnum.Error)
            {
                litParamError.Text = Utility.GetLabel("ErrParamParsing", "Error parsing [" + currParam.Name + "] param");
                tableParams.Controls.Add(litParamError);
                //and add an hidden control
            }
            if (currParam.Type == FormFieldTypeEnum.Hidden)
            {
                //if hidden param hide row
                //row.Style["display"] = "none";
            }
        }

        #endregion
    }
}
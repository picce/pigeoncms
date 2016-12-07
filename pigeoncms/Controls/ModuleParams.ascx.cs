using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using PigeonCms;

public partial class Controls_ModuleParams : PigeonCms.BaseModuleControl
{
    public string CssFile 
    { 
        get { return TxtCssFile.Text; } 
    }

    public string CssClass 
    { 
        get { return TxtCssClass.Text; } 
    }

    public string SystemMessagesTo
    {
        get { return TxtSystemMessagesTo.Text; } 
    }

    public Utility.TristateBool UseCache 
    {
        get { return (Utility.TristateBool)int.Parse(DropUseCache.SelectedValue); } 
    }

    public Utility.TristateBool UseLog
    {
        get { return (Utility.TristateBool)int.Parse(DropUseLog.SelectedValue); }
    }

    //NEW METHODS
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

        if (currParam.Type == FormFieldTypeEnum.Combo)
        {
            Panel pnlContainerCombo = new Panel();
            pnlContainerCombo.CssClass = "form-group form-select-wrapper form-select-detail-item";
            pnlContainerCombo.Controls.Add(lblParamLabel);
            pnlContainerCombo.Controls.Add(control2Add);
            tableParams.Controls.Add(pnlContainerCombo);
        }
        else {
            tableParams.Controls.Add(lblParamLabel);
            tableParams.Controls.Add(control2Add);
        }

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
    /////////////////////////////

    public bool DirectEditMode
    {
        get 
        {
            bool res = false;
            if (ChkDirectEditMode.Enabled)
            {
                res = ChkDirectEditMode.Checked;
            }
            return res;
        }
    }

    protected new void Page_Init(object sender, EventArgs e)
    {
        this.BaseModule = new Module();
        this.BaseModule.ModuleNamespace = "PigeonCms";
        this.BaseModule.ModuleName = "ModuleParams";
        base.Page_Init(sender, e);
    }

    /// <summary>
    /// loads the right params list for current module
    /// </summary>
    /// <param name="moduleId"></param>
    public void LoadParams(Module currentModule)
    {
        PigeonCms.ModuleType modType = null;
        PigeonCms.ModuleType viewType = null;
        
        try
        {
            modType = currentModule.ModuleType;
        }
        catch (Exception ex)
        {
            Tracer.Log("LoadParams(): error loading module params", TracerItemType.Error, ex);
        }

        TxtCssFile.Text = currentModule.CssFile;
        TxtCssClass.Text = currentModule.CssClass;
        TxtSystemMessagesTo.Text = currentModule.SystemMessagesTo;
        Utility.SetDropByValue(DropUseCache, ((int)currentModule.UseCache).ToString());
        Utility.SetDropByValue(DropUseLog, ((int)currentModule.UseLog).ToString());
        ChkDirectEditMode.Checked = currentModule.DirectEditMode;
        ChkDirectEditMode.Enabled = true;
        if (!modType.AllowDirectEditMode)
        {
            ChkDirectEditMode.Enabled = false;
        }

        List<ResLabel> labelsList;  //localized labels for module params
        labelsList = LabelsProvider.GetLabelsByResourceSet(currentModule.ModuleFullName);
        if (!string.IsNullOrEmpty(currentModule.CurrView))
        {
            //loads current view specific params
            viewType = new ModuleTypeManager().GetByFullName(
                currentModule.ModuleFullName, currentModule.CurrView.Replace(".ascx", ".xml"));
        }

        //clear previous params list
        while (PanelParams.Controls.Count > 0) 
        {
            PanelParams.Controls.RemoveAt(0);
        }

        //NEW METHOD
        //FormBuilder.RenderParamsOnPanel(PanelParams, currentModule.Params, modType.Params, labelsList);
        RenderParamsOnPanel(PanelParams, currentModule.Params, modType.Params, labelsList);
        if (viewType != null)
            //NEW METHOD
            //FormBuilder.RenderParamsOnPanel(PanelParams, currentModule.Params, viewType.Params, labelsList);
            RenderParamsOnPanel(PanelParams, currentModule.Params, viewType.Params, labelsList);
    }
}

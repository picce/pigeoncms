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
        FormBuilder.RenderParamsOnPanel(PanelParams, currentModule.Params, modType.Params, labelsList);
        if (viewType != null)
            FormBuilder.RenderParamsOnPanel(PanelParams, currentModule.Params, viewType.Params, labelsList);
    }
}

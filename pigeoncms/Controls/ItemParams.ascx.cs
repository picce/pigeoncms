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
using PigeonCms;
using System.Collections.Generic;

public partial class Controls_ItemParams : PigeonCms.BaseModuleControl
{
    public string Title
    {
        get
        {
            var res = "";
            if (this.Visible)
            {
                if (ViewState["Title"] != null)
                    res = (string)ViewState["Title"];
            }
            return res;
        }
        set { ViewState["Title"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void ClearParams()
    {
        try
        {
            PanelParams.Controls.Clear();
        }
        catch (Exception ex)
        {
            Tracer.Log("ClearParams(): error loading item params", TracerItemType.Error, ex);
        }
    }

    /// <summary>
    /// loads params list for current item
    /// </summary>
    public void LoadParams(Item currentItem)
    {
        PigeonCms.ItemType itemType = null;
        try
        {
            itemType = currentItem.ItemType;
        }
        catch (Exception ex)
        {
            Tracer.Log("LoadParams(): error loading item params", TracerItemType.Error, ex);
        }
        FormBuilder.RenderParamsOnPanel(PanelParams, currentItem.Params, itemType.Params, null);
    }

    /// <summary>
    /// loads fields list for current item
    /// </summary>
    public void LoadFields(Item currentItem)
    {
        //List<ResLabel> labelsList;  //localized labels for module params
        //labelsList = LabelsProvider.GetLabelsByResourceSet(currentModule.ModuleFullName);
        FormBuilder.RenderParamsOnPanel(PanelParams, currentItem, null);
    }
}

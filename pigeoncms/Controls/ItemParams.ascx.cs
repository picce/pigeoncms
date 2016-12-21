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


public partial class Controls_ItemParams : PigeonCms.Controls.ItemParamsControl
{
	public override string Title
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

	public override void ClearParams()
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
	public override void LoadParams(Item currentItem)
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
	public override void LoadFields(Item currentItem)
    {
        FormBuilder.RenderParamsOnPanel(PanelParams, currentItem, null);
    }
}

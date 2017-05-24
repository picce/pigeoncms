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

public partial class Controls_ContentEditorControl : 
	PigeonCms.BaseModuleControl,
	PigeonCms.Controls.IContentEditorControl
{
    private ContentEditorProvider.Configuration configuration = new ContentEditorProvider.Configuration();
    public ContentEditorProvider.Configuration Configuration
    {
        get { return configuration; }
        set { configuration = value; }
    }

    /// <summary>
    /// show readmore button
    /// </summary>
    public bool ReadMoreButton
    {
        get { return this.Configuration.ReadMoreButton; }
        set { this.Configuration.ReadMoreButton = value; }
    }

    /// <summary>
    /// show pageBreak button
    /// </summary>
    public bool PageBreakButton
    {
        get { return this.Configuration.ReadMoreButton; }
        set { this.Configuration.PageBreakButton = value; }
    }

    /// <summary>
    /// show file button to open filemanager 
    /// </summary>
    public bool FileButton
    {
        get { return this.Configuration.FileButton; }
        set { this.Configuration.FileButton = value; }
    }

    /// <summary>
    /// content editor text
    /// </summary>
    public string Text
    {
        get { return Txt1.Text; }
        set { Txt1.Text = value; }
    }

	/// <summary>
	/// css class for editor text
	/// </summary>
	public string CssClass
	{
		get { return Txt1.CssClass; }
		set { Txt1.CssClass = value; }
	}


    protected new void Page_Init(object sender, EventArgs e)
    {
        this.BaseModule = new Module();
        this.BaseModule.ModuleNamespace = "PigeonCms";
        this.BaseModule.ModuleName = "ContentEditorControl";

        if (this.Configuration.ReadMoreButton)
        {
            var btn = new HtmlInputButton();
            btn.Value = base.GetLabel("ReadMore", "Read more");
            btn.Attributes["onclick"] = "insertReadmore();";
            btn.Attributes["class"] = "button";
            this.PanelButtons.Controls.Add(btn);
        }
        if (this.Configuration.PageBreakButton)
        {
            var btn = new HtmlInputButton();
            btn.Value = base.GetLabel("Pagebreak", "Page break");
            btn.Attributes["onclick"] = "insertPagebreak();";
            btn.Attributes["class"] = "button";
            this.PanelButtons.Controls.Add(btn);
        }
        if (this.Configuration.FileButton)
        {
            var btn = new HtmlInputButton();
            btn.Value = base.GetLabel("File", "File");
			btn.Attributes["data-href"] = configuration.FilesUploadUrl;
			btn.Attributes["class"] = "button js-open-fancy";
            this.PanelButtons.Controls.Add(btn);
        }
		if (!string.IsNullOrEmpty(this.Configuration.CssClass))
		{
			this.CssClass = this.Configuration.CssClass;
		}
        //if (this.Configuration.ToggleEditor)
        //{
        //    var btn = new HtmlInputButton();
        //    btn.Value = base.GetLabel("Html", "Html");
        //    btn.Attributes["onclick"] = "toggleEditor();";
        //    btn.Attributes["class"] = "button";
        //    this.PanelButtons.Controls.Add(btn);
        //}

        base.Page_Init(sender, e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
}

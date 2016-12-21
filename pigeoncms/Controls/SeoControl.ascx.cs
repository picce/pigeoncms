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


public partial class Controls_SeoControl : PigeonCms.Controls.SeoControl
{
	public bool ShowOnlyDefaultCulture { get; set; }

    protected new void Page_Init(object sender, EventArgs e)
    {
        this.BaseModule = new Module();
        this.BaseModule.ModuleNamespace = "PigeonCms";
        this.BaseModule.ModuleName = "SeoControl";
        
		if (!Page.IsPostBack)
        {
        }

        base.Page_Init(sender, e);

		foreach (KeyValuePair<string, string> item in Config.CultureList)
		{
			//title
			var txt1 = new TextBox();
			txt1.ID = "TxtSeoTitle" + item.Value;
			txt1.MaxLength = 200;
			txt1.CssClass = "form-control";
			txt1.ToolTip = item.Key;
			LabelsProvider.SetLocalizedControlVisibility(this.ShowOnlyDefaultCulture, item.Key, txt1);
			var group = new Panel();
			group.CssClass = "form-group input-group";
			group.Controls.Add(txt1);

			Literal lit = new Literal();
			if (!this.ShowOnlyDefaultCulture)
				lit.Text = "<div class=\"input-group-addon\"><span>" + item.Value.Substring(0, 3) + "</span></div>";
			group.Controls.Add(lit);
			PanelTitle.Controls.Add(group);


			var txt2 = new TextBox();
			txt2.ID = "TxtSeoDescription" + item.Value;
			//txt2.TextMode = TextBoxMode.MultiLine;
			txt2.Rows = 2;
			txt2.CssClass = "form-control";
			txt2.ToolTip = item.Key;
			LabelsProvider.SetLocalizedControlVisibility(this.ShowOnlyDefaultCulture, item.Key, txt2);
			var group2 = new Panel();
			group2.CssClass = "form-group input-group";
			group2.Controls.Add(txt2);

			Literal lit2 = new Literal();
			if (!this.ShowOnlyDefaultCulture)
				lit2.Text = "<div class=\"input-group-addon\"><span>" + item.Value.Substring(0, 3) + "</span></div>";
			group2.Controls.Add(lit2);
			PanelDescription.Controls.Add(group2);

		}
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

	public override void ClearForm()
    {

		ChkNoFollow.Checked = false;
		ChkNoIndex.Checked = false;

		foreach (KeyValuePair<string, string> item in Config.CultureList)
		{
			TextBox t1 = new TextBox();
			t1 = (TextBox)PanelTitle.FindControl("TxtSeoTitle" + item.Value);
			t1.Text = "";

			TextBox t2 = new TextBox();
			t2 = (TextBox)PanelTitle.FindControl("TxtSeoDescription" + item.Value);
			t2.Text = "";
		}
    }

	public override void Form2obj(ITableWithSeo obj)
    {
		obj.Seo.NoFollow = ChkNoFollow.Checked;
		obj.Seo.NoIndex = ChkNoIndex.Checked;

		obj.Seo.TitleTranslations.Clear();
		obj.Seo.DescriptionTranslations.Clear();
		foreach (KeyValuePair<string, string> item in Config.CultureList)
		{
			TextBox t1 = new TextBox();
			t1 = (TextBox)PanelTitle.FindControl("TxtSeoTitle" + item.Value);
			obj.Seo.TitleTranslations.Add(item.Key, t1.Text);

			var txt2 = new TextBox();
			txt2 = (TextBox)PanelTitle.FindControl("TxtSeoDescription" + item.Value);
			obj.Seo.DescriptionTranslations.Add(item.Key, txt2.Text);
		}
    }

	public override void Obj2form(ITableWithSeo obj)
    {
		ChkNoFollow.Checked = obj.Seo.NoFollow;
		ChkNoIndex.Checked = obj.Seo.NoIndex;

		foreach (KeyValuePair<string, string> item in Config.CultureList)
		{
			string sTitleTranslation = "";
			TextBox t1 = new TextBox();
			t1 = (TextBox)PanelTitle.FindControl("TxtSeoTitle" + item.Value);
			obj.Seo.TitleTranslations.TryGetValue(item.Key, out sTitleTranslation);
			t1.Text = sTitleTranslation;

			string sDescriptionTraslation = "";
			var txt2 = new TextBox();
			txt2 = (TextBox)PanelTitle.FindControl("TxtSeoDescription" + item.Value);
			obj.Seo.DescriptionTranslations.TryGetValue(item.Key, out sDescriptionTraslation);
			txt2.Text = sDescriptionTraslation;
		}
    }


}

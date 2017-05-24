using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PigeonCms;
using Acme;

public partial class contents_detail : Acme.BasePage
{
    protected int CurrentId
    {
        get
        {
            int id = 0;
            int.TryParse(PigeonCms.Utility._QueryString("id"), out id);
            return id;
        }
    }

    protected string CodeSource = @"
        public void test(){};";

    protected Item SingleItem;
    protected string DescriptionItem;

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMasterPage.DataSection = "detail";
        CurrentMasterPage.LinkFooter = "javascript:history.back()";
        CurrentMasterPage.TextLinkFooter = "back";

        //example source
        CodeSource = HttpContext.Current.Server.HtmlEncode(CodeSource);

        //get item detail
        var itemMan = new PigeonCms.ItemsManager<PigeonCms.Item, PigeonCms.ItemsFilter>(true, false);
        SingleItem = itemMan.GetByKey(CurrentId);

        //get item description
        DescriptionItem = SingleItem.Description;
        var list = PigeonCms.Utility.String2List(SingleItem.Description, PigeonCms.ContentEditorProvider.SystemReadMoreTag);
        if (list.Count > 1)
            DescriptionItem = list[1];
        DescriptionItem = PigeonCms.Utility.Html.StripTagsRegexCompiled(DescriptionItem);

    }
}
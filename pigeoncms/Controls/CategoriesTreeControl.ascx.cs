using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using PigeonCms;
using PigeonCms.Controls;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using PigeonCms;
using System.Collections.Generic;
using PigeonCms.Controls;

public partial class Controls_CategoriesTreeControl : PigeonCms.Modules.CategoriesAdminControl
{

    protected new void Page_Init(object sender, EventArgs e)
    {
        this.BaseModule = new Module();
        this.BaseModule.ModuleNamespace = "PigeonCms";
        this.BaseModule.ModuleName = "CategoriesAdmin";
        base.Page_Init(sender, e);
    }

    //protected void Tree_NodeClick(object sender, NodeClickEventArgs e)
    //{
    //    LblOk.Text = RenderSuccess(e.Command.ToString() + " " + e.CategoryId.ToString());
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        //base.NodeClick += new NodeClickDelegate(Tree_NodeClick);

        if (Page.IsPostBack)
        {

            string eventArg = HttpContext.Current.Request["__EVENTARGUMENT"];
            if (eventArg.Contains("action-cat-select"))
            {
                var args = Utility.String2List(eventArg);
                base.NodeCommand(NodeClickCommandEnum.Select, int.Parse(args[1]));
            }
            else if (eventArg.Contains("action-cat-edit"))
            {
                var args = Utility.String2List(eventArg);
                base.NodeCommand(NodeClickCommandEnum.Edit, int.Parse(args[1]));
            }
            else if (eventArg.Contains("action-cat-enabled"))
            {
                var args = Utility.String2List(eventArg);
                int recordId = int.Parse(args[1]);
                bool enabledValue = bool.Parse(args[2]);
                base.NodeCommand(NodeClickCommandEnum.Enabled,
                    int.Parse(args[1]), args[2]);
            }
            else if (eventArg.Contains("action-cat-moveup"))
            {
                var args = Utility.String2List(eventArg);
                base.NodeCommand(NodeClickCommandEnum.MoveUp, int.Parse(args[1]));
            }
            else if (eventArg.Contains("action-cat-movedown"))
            {
                var args = Utility.String2List(eventArg);
                base.NodeCommand(NodeClickCommandEnum.MoveDown, int.Parse(args[1]));
            }
            else if (eventArg.Contains("action-cat-delete"))
            {
                var args = Utility.String2List(eventArg);
                base.NodeCommand(NodeClickCommandEnum.Delete, int.Parse(args[1]));
            }

        }
    }

    protected void Tree1_SelectedNodeChanged(object sender, EventArgs e)
    {
    }

    protected void Tree1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
    }

    public void BindTree(int sectionId)
    {
        var man = new CategoriesManager(true, true);
        var filter = new CategoriesFilter();

        filter.Enabled = Utility.TristateBool.NotSet;
        filter.SectionId = (sectionId > 0 ? sectionId : -1);
        var list = man.GetByFilter(filter, "");
        Tree1.Nodes.Clear();
        bindTree(list, null);
    }

    private void bindTree(List<Category> list, TreeViewLinksNode parentNode)
    {
        var nodes = list.Where(x => parentNode == null ? x.ParentId == 0 : x.ParentId == int.Parse(parentNode.Value));
        foreach (var node in nodes)
        {
            //var newNode = new TreeNode(node.Title, node.Id.ToString());
            string text = "";

            if (this.ShowItemsCount)
            {
                int itemsCount = getItemsCount(node);
                if (itemsCount > 0)
                {
                    text += "<div>";
                    text += itemsCount + (itemsCount == 1 ? " item" : " items");
                    text += "</div>";
                }
            }

            if ((this.TargetImagesUpload > 0))
            {
                int filesCount = node.Files.Count;
                if (filesCount > 0)
                {
                    text += "<div>";
                    text += filesCount + (filesCount == 1 ? " file" : " files");
                    text += "</div>";
                }
            }

            if ((this.TargetImagesUpload > 0))
            {
                int imagesCount = node.Images.Count;
                if (imagesCount > 0)
                {
                    text += "<div>";
                    text += imagesCount + (imagesCount == 1 ? " image" : " images");
                    text += "</div>";
                }
            }

            if (!string.IsNullOrEmpty(node.CssClass))
                text += "class: " + node.CssClass;

            if (this.ShowSecurity)
            {
                string extIdString = "";
                if (!string.IsNullOrEmpty(node.ExtId))
                    extIdString = " / ExtID: " + node.ExtId;
                text += "<div>"
                    + "ID: " + node.Id.ToString() + extIdString + "<br>"
                    + RenderAccessTypeSummary(node, "read: ", "write: ")
                    + "</div>";
            }

            var textContainer = new TreeViewLinksNode.TextContainer()
            {
                CssClass = "treeview__text-container small text-muted",
                Text = text
            };


            var linksContainer = new TreeViewLinksNode.LinksContainer()
            {
                CssClass = "treeview__links-container"
            };

            if (this.AllowEdit)
            {
                //edit
                linksContainer.Links.Add(new TreeViewLinksNode.Link()
                {
                    CssClass = "fa fa-pgn_edit fa-fw action-cat-edit",
                    DataId = "",
                    DataCommand = "action-cat-edit|" + node.Id.ToString(),
                    Text = "",
                    Title = "edit item",
                    Url = "javascript:void(0);"
                });

                //enable
                string chkClass = (node.Enabled ? "fa-pgn_checked" : "fa-pgn_unchecked");
                linksContainer.Links.Add(new TreeViewLinksNode.Link()
                {
                    CssClass = "fa " + chkClass + " fa-fw action-cat-enabled",
                    DataId = "",
                    DataCommand = "action-cat-enabled|" + node.Id.ToString() + "|" + (!node.Enabled).ToString(),
                    Text = "",
                    Url = "javascript:void(0);"
                });
            }

            if (this.AllowOrdering)
            {
                //up
                linksContainer.Links.Add(new TreeViewLinksNode.Link()
                {
                    CssClass = "fa fa-pgn_up fa-fw action-cat-moveup",
                    DataId = "",
                    DataCommand = "action-cat-moveup|" + node.Id.ToString(),
                    Text = "",
                    Url = "javascript:void(0);"
                });

                //down
                linksContainer.Links.Add(new TreeViewLinksNode.Link()
                {
                    CssClass = "fa fa-pgn_down fa-fw action-cat-movedown",
                    DataId = "",
                    DataCommand = "action-cat-movedown|" + node.Id.ToString(),
                    Text = "",
                    Url = "javascript:void(0);"
                });
            }

            //files
            if (this.TargetFilesUpload > 0)
            {
                linksContainer.Links.Add(new TreeViewLinksNode.Link()
                {
                    CssClass = "fancyRefresh",
                    DataId = "",
                    DataCommand = "",
                    Text = "<i class='fa fa-pgn_attach fa-fw'></i>",
                    Title = "",
                    Url = this.FilesUploadUrl + "?type=categories&id=" + node.Id.ToString()
                });
            }

            //images
            if ((this.TargetImagesUpload > 0))
            {
                linksContainer.Links.Add(new TreeViewLinksNode.Link()
                {
                    CssClass = "fancyRefresh",
                    DataId = "",
                    DataCommand = "",
                    Text = "<i class='fa fa-pgn_image fa-fw'></i>",
                    Title = "",
                    Url = this.ImagesUploadUrl + "?type=categories&id=" + node.Id.ToString()
                });
            }

            if (this.AllowDelete)
            {
                //delete
                linksContainer.Links.Add(new TreeViewLinksNode.Link()
                {
                    CssClass = "fa fa-pgn_delete fa-fw action-cat-delete",
                    DataId = "",
                    DataCommand = "action-cat-delete|" + node.Id.ToString(),
                    Text = "",
                    Title = "delete item",
                    Url = "javascript:void(0);"
                });

            }

            string nodeTitle = node.Title;
            if (base.AllowSelection)
                nodeTitle = "<a class='treeview__node-text action-cat-select' data-command='action-cat-select|" + node.Id.ToString() + "'>"
                    + node.Title
                    + "</a>";

            var newNode = new TreeViewLinksNode(nodeTitle, node.Id.ToString(),
                linksContainer, textContainer);

            if (parentNode == null)
                Tree1.Nodes.Add(newNode);
            else
                parentNode.ChildNodes.Add(newNode);

            bindTree(list, newNode);
        }
    }

    private int getItemsCount(Category category)
    {
        int res = 0;
        var itemsMan = new ItemsManager<Item, ItemsFilter>();
        var itemsFilter = new ItemsFilter();
        itemsFilter.CategoryId = category.Id;
        res = itemsMan.GetByFilter(itemsFilter, "").Count;
        return res;
    }

}

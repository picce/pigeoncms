using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PigeonCms;

namespace PigeonCms.Controls
{
    public class TreeViewLinks : TreeView
    {
        protected override TreeNode CreateNode()
        {
            return new TreeViewLinksNode();
        }
    }

    public class TreeViewLinksNode: TreeNode
    {
        [Serializable]
        public class TextContainer
        {
            public string CssClass = "";
            public string Text = "";
        }

        [Serializable]
        public class LinksContainer
        {
            public string CssClass = "";
            public List<Link> Links = new List<Link>();
        }

        [Serializable]
        public class Link
        {
            public string Text = "";
            public string Title = "";
            public string CssClass = "";
            public string DataId = "";
            public string DataCommand = "";
            public string Url = "";
        }

        private string nodeText = "";
        private string nodeValue = "";
        private LinksContainer linksContainer = new LinksContainer();
        private TextContainer textContainer = new TextContainer();

        public TreeViewLinksNode()
        {
        }

        public TreeViewLinksNode(string nodeText, string nodeValue, 
            LinksContainer linksContainer,
            TextContainer textContainer)
        {
            this.nodeText = nodeText;
            this.nodeValue = nodeValue;
            this.linksContainer = linksContainer;
            this.textContainer = textContainer;

            this.Text = "";
            this.Value = nodeValue;
            this.SelectAction = TreeNodeSelectAction.Select;
        }

        protected override void RenderPreText(HtmlTextWriter writer)
        {
            bool bShowlink = false;
            if (linksContainer.Links.Count > 0)
                bShowlink = true;

            bool bShowText = false;
            if (!string.IsNullOrEmpty(textContainer.Text))
                bShowText = true;

            // start span
            writer.RenderBeginTag((HtmlTextWriterTag.Span));
            writer.Write(this.nodeText);
            writer.RenderEndTag();
            
            if (bShowlink)
            {
                //init div
                writer.AddAttribute("class", this.linksContainer.CssClass);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                foreach (var link in this.linksContainer.Links)
                {
                    //init link
                    writer.AddAttribute("href", link.Url);
                    writer.AddAttribute("class", link.CssClass);
                    writer.AddAttribute("data-id", link.DataId);
                    writer.AddAttribute("data-command", link.DataCommand);
                    if (!string.IsNullOrEmpty(link.Title))
                        writer.AddAttribute("title", link.Title);
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(link.Text);
                    writer.RenderEndTag();
                }
                //end div
                writer.RenderEndTag();
            }

            if (bShowText)
            {
                writer.AddAttribute("class", this.textContainer.CssClass);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(textContainer.Text);
                writer.RenderEndTag();
            }

            base.RenderPreText(writer);
        }

        protected override object SaveViewState()
        {
            object[] arrState = new object[5];
            arrState[0] = base.SaveViewState();
            arrState[1] = this.nodeText;
            arrState[2] = this.nodeValue;
            arrState[3] = this.linksContainer;
            arrState[4] = this.textContainer;

            return arrState;
        }

        protected override void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                object[] arrState = savedState as object[];

                this.nodeText = (string)arrState[1];
                this.nodeValue = (string)arrState[2];
                this.linksContainer = (LinksContainer)arrState[3];
                this.textContainer = (TextContainer)arrState[4];
                base.LoadViewState(arrState[0]);
            }
        }
    }

}

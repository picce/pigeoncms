using PigeonCms.Controls.ItemFields;
using System;

namespace AQuest.PigeonCMS.Controls
{
    public partial class TextAreaFieldContainer : AbstractFieldContainer
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            litColumnSizes.Text = Sizes;
            litClass.Text = CSSClass;
            //litControlSizes.Text = ControlSizes;

            litLabel.Text = Label;

            if (InnerControl != null)
                plhInnerControl.Controls.Add(InnerControl);
        }
    }
}
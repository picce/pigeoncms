using AQuest.PigeonCMS.ItemsAdmin;
using System;

namespace AQuest.PigeonCMS.Controls
{
    public partial class FieldContainer : AbstractFieldContainer
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            litColumnSizes.Text = Sizes;
            //litControlSizes.Text = ControlSizes;
            litClass.Text = CSSClass;

            litLabel.Text = Label;

            if (InnerControl != null)
                plhInnerControl.Controls.Add(InnerControl);
        }
    }
}
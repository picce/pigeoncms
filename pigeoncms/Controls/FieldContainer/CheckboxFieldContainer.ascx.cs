using AQuest.PigeonCMS.ItemsAdmin;
using System;

namespace AQuest.PigeonCMS.Controls
{
    public partial class CheckboxFieldContainer : AbstractFieldContainer
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            litColumnSizes.Text = Sizes;
            litClass.Text = CSSClass;
            litControlSizes.Text = ControlSizes;            

            if (InnerControl != null)
                plhInnerControl.Controls.Add(InnerControl);

            litLabel.Text = string.Format("<label for=\"{0}\">{1}</label>", InnerControl == null ? "" : InnerControl.ClientID, Label);
        }
    }
}
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using PigeonCms;


public partial class Controls_EmailContactFormControl: PigeonCms.EmailContactFormControl
{
    protected new void Page_Init(object sender, EventArgs e)
    {
        //tnx to http://cfouquet.blogspot.com/2006/07/validated-check-box-in-aspnet.html
        TxtShowPrivacy.Style.Add("display", "none");
        ChkShowPrivacy.Attributes["onclick"] = "document.getElementById('" + TxtShowPrivacy.ClientID + "').value=this.checked;";
        TxtShowPrivacy.Text = ChkShowPrivacy.Checked.ToString().ToLower();

        CaptchaControl1.Visible = base.ShowCaptcha;
        CaptchaControl1.Enabled = base.ShowCaptcha;
        CaptchaControl1.Text = base.GetLabel("LblCaptchaText", "enter the code shown");
        CaptchaControl1.CaptchaFontWarping = WebControlCaptcha.CaptchaImage.FontWarpFactor.Medium;
        CaptchaControl1.CaptchaBackgroundNoise = WebControlCaptcha.CaptchaImage.BackgroundNoiseLevel.High;
        CaptchaControl1.CaptchaLineNoise = WebControlCaptcha.CaptchaImage.LineNoiseLevel.Low;
        CaptchaControl1.CaptchaMaxTimeout = 240;
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!this.ShowPrivacyCheck)
        {
            ChkShowPrivacy.Checked = true;
            PanelPrivacy.Visible = false;
        }
    }

    protected void BtnSend_Click(object sender, EventArgs e)
    {
        string body = "nome e cognome: " + TxtNomeCognome.Text + "<br />"
                + "azienda: " + TxtCompanyName.Text + "<br />"
                + "città: " + TxtCity.Text + "<br />"
                + "e-mail: " + TxtEmail.Text + "<br />"
                + "telefono: " + TxtPhone.Text + "<br />"
                + "messaggio: " + TxtMessage.Text;
        if (CaptchaControl1.UserValidated)
        {
            try
            {
                LogProvider.Write(this.BaseModule, body, TracerItemType.Info);
                SendEmail(body);
                disableFields();
            }
            catch (Exception e1)
            {
                LogProvider.Write(this.BaseModule, e1.ToString(), TracerItemType.Error);
            }
        }
        else
        {
            body = "Captcha:" + CaptchaControl1.ErrorMessage + "<br />" + body;
            LogProvider.Write(this.BaseModule, body, TracerItemType.Warning);
            LblErroreInfo = CaptchaControl1.ErrorMessage;
        }
    }

    private void disableFields()
    {
        BtnSend.Enabled = false;
    }
}

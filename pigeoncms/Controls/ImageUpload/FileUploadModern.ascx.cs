using Newtonsoft.Json;
using PigeonCms;
using System;
using System.Web.UI;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using PigeonCms.Controls;
using PigeonCms.Controls.ItemsAdmin;
using PigeonCms.Core.Helpers;

public partial class FileUploadModern : UserControl, IUploadControl
{
    public string AllowedFileTypes { get; set; }
    public int MaxFileSize { get; set; }
    public string FilePath { get; set; }

    protected bool deleted = false;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        fileUpload.Attributes["accept"] = AcceptedMime;
        fileUpload.Attributes["data-max-file-size"] = MaxFileSize.ToString();
        litDataMaxSize.Text = string.Format(" data-max-file-size='{0}' ", MaxFileSize);
        litTranslations.Text = string.Format(" data-lbl-FileTooBig='{0}' data-lbl-FileNotAllowed='{1}' ",
            Utility.GetLabel("UploadFile_FileTooBig", "File exceed size limits").Replace("'", ""),
            Utility.GetLabel("UploadFile_FileNotAllowed", "File type is not allowed").Replace("'", "")
        );

        LitRestrictions.Text = string.Format("{0}: <strong>{1}</strong> KB, {2}: <strong>{3}</strong>",
            Utility.GetLabel("UploadFile_MaxFileSize", "Dim. max file"),
            MaxFileSize,
            Utility.GetLabel("UploadFile_FileTypes", "Tipo file"),
            AllowedFileTypes
        );

        hidParameters.Value = EncodedParameters;
        BtnDel.Click += btnDel_Click;
        LoadInfo();
    }

    protected string AcceptedMime
    {
        get
        {
            if (string.IsNullOrWhiteSpace(AllowedFileTypes))
                return "*";

            List<string> mimes = new List<string>();
            string[] allowedTypes = AllowedFileTypes.Split(',');

            foreach (string allowedType in allowedTypes)
            {
                string mime = FilesHelper.GetMimeFromExtension(allowedType);
                if (string.IsNullOrWhiteSpace(mime))
                    continue;

                if (mimes.Contains(mime))
                    continue;

                mimes.Add(mime);
            }

            if (mimes.Count() <= 0)
                return "*";

            return string.Join(",", mimes);
        }
    }

    protected string EncodedParameters
    {
        get
        {
            string parameters = JsonConvert.SerializeObject(new UploadParameters
            {
                UniqueID = UniqueID,
                AllowedFileTypes = AllowedFileTypes,
                MaxFileSize = MaxFileSize
            });

            return UrlUtils.Base64Encode(Utility.Encryption.Encrypt(parameters, Config.EncryptKey));
        }
    }

    private void btnDel_Click(object sender, EventArgs e)
    {       
        BoxPreview.Style.Add("opacity", "0");
        Session["FileUpload_" + UniqueID + "_Deleted"] = "__deleted__";
    }

    protected void LoadInfo()
    {
        if ((string.IsNullOrWhiteSpace(FilePath) || !File.Exists(Server.MapPath(FilePath))) && Session["FileUpload_" + UniqueID] == null)
        {
            BoxPreview.Style.Add("opacity", "0");
            return;
        }

        ActionsLayer.Visible = true;

        lnkPreview.NavigateUrl = PreviewUrl;
        lnkPreview.Text = PreviewName;

        lblDelete.Attributes.Add("title", Utility.GetLabel("delete_image_title", "Conferma eliminazione file"));
        lblDelete.Attributes.Add("data-msg-title", Utility.GetLabel("delete_image_title", "Conferma eliminazione file"));
        lblDelete.Attributes.Add("data-msg-subtitle", Utility.GetLabel("delete_image", "Sei sicuro di voler eliminare il file ?"));
        lblDelete.Attributes.Add("data-msg-cancel", Utility.GetLabel("cancel", "cancel"));
        lblDelete.Attributes.Add("data-msg-confirm", Utility.GetLabel("confirm", "confirm"));
    }
    
    protected string PreviewUrl
    {
        get
        {
            if (Session["FileUpload_" + UniqueID] != null)
                return "/Controls/ImageUpload/FileUploadModernHandler.ashx?action=preview&parameters=" + EncodedParameters + "&ts=" + new DateTime().Ticks;
            else if (!string.IsNullOrWhiteSpace(FilePath))
                return VirtualPathUtility.ToAbsolute(FilePath);

            return string.Empty;
        }
    }

    protected string PreviewName
    {
        get
        {
            if (Session["FileUpload_" + UniqueID] != null)
                return Convert.ToString(Session["FileUpload_" + UniqueID + "_RealName"]);
            else if (!string.IsNullOrWhiteSpace(FilePath))
                return new FileInfo(FilePath).Name;

            return string.Empty;
        }
    }

    public void SaveTo(string filePath)
    {
        string tmpFilePath = /*Path.GetTempPath() +*/ Convert.ToString(Session["FileUpload_" + UniqueID]);
        if (File.Exists(tmpFilePath))
        {
            string directory = FilesHelper.GetDirectory(filePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.Move(tmpFilePath, filePath);

            FileSecurity fs = File.GetAccessControl(filePath);
            fs.SetAccessRuleProtection(false, false);
            File.SetAccessControl(filePath, fs);

            Session.Remove("FileUpload_" + UniqueID);
            Session.Remove("FileUpload_" + UniqueID + "_Deleted");
            Session.Remove("FileUpload_" + UniqueID + "_RealName");
        }
    }

    public void CleanSession ()
    {
        if (Session["FileUpload_" + UniqueID] == null)
            return;

        Session.Remove("FileUpload_" + UniqueID);
        Session.Remove("FileUpload_" + UniqueID + "_Deleted");
        Session.Remove("FileUpload_" + UniqueID + "_RealName");
    }

    public string GetExtension()
    {
        string tmpFilePath = /*Path.GetTempPath() +*/ Convert.ToString(Session["FileUpload_" + UniqueID]);
        if (!File.Exists(tmpFilePath))
            return string.Empty;

        return tmpFilePath.Split('.').Last();
    }

    public bool Deleted
    {
        get
        {
            return (Session["FileUpload_" + UniqueID + "_Deleted"] != null);
        }
    }

    public void PerformDelete ()
    {
        if (Session["FileUpload_" + UniqueID] != null)
        {
            string tmpFilePath = /*Path.GetTempPath() + */Convert.ToString(Session["FileUpload_" + UniqueID]);
            if (File.Exists(tmpFilePath))
                File.Delete(tmpFilePath);                

            Session.Remove("FileUpload_" + UniqueID);
            Session.Remove("FileUpload_" + UniqueID + "_Deleted");
            Session.Remove("FileUpload_" + UniqueID + "_RealName");
        }
        else
        {
            if (string.IsNullOrWhiteSpace(FilePath))
                return;

            string filePath = FilePath;
            if (filePath.StartsWith("~") || filePath.StartsWith("/"))
                filePath = Server.MapPath(filePath);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }

    public bool HasChanged
    {
        get
        {
            return Session["FileUpload_" + UniqueID] != null;
        }
    }

    public bool HasFile
    {
        get
        {
            return !string.IsNullOrWhiteSpace(FilePath) || Session["FileUpload_" + UniqueID] != null;
        }
    }
}
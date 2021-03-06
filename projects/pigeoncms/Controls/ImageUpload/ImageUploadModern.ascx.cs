﻿using Newtonsoft.Json;
using PigeonCms;
using System;
using System.Web.UI;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Collections.Generic;
using System.Configuration;
using PigeonCms.Controls;
using PigeonCms.Controls.ItemsAdmin;
using PigeonCms.Core.Helpers;


public partial class ImageUploadModern : BaseModuleControl, IUploadControl
{
    public event UploadControlFileDeletedDelegate FileDeleted;


    public string AllowedFileTypes { get; set; }
    public int MaxFileSize { get; set; }
    public string FilePath { get; set; }
    public string Name { get; set; }
    protected bool deleted = false;

    private Module fakeModule = new Module();

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!PgnUserCurrent.IsAuthenticated)
            throw new Exception("Not allowed");

        fakeModule.ModuleNamespace = "PigeonCms";
        fakeModule.ModuleName = "ImageUploadModernControl";
        fakeModule.UseLog = Utility.TristateBool.True;
        this.BaseModule = fakeModule;

        fileUpload.Attributes["accept"] = AcceptedMime;
        fileUpload.Attributes["data-max-file-size"] = MaxFileSize.ToString();
        litDataMaxSize.Text = string.Format(" data-max-file-size='{0}' ", MaxFileSize);
        litTranslations.Text = string.Format(" data-lbl-FileTooBig='{0}' data-lbl-FileNotAllowed='{1}' ",
            GetLabel("UploadFile_FileTooBig", "File exceed size limits").Replace("'", ""),
            GetLabel("UploadFile_FileNotAllowed", "File type is not allowed").Replace("'", "")
        );

        LitRestrictions.Text = string.Format("{0}: <strong>{1}</strong> KB, {2}: <strong>{3}</strong>",
            GetLabel("UploadFile_MaxFileSize", "Dim. max file"),
            MaxFileSize,
            GetLabel("UploadFile_FileTypes", "File type"),
            AllowedFileTypes
        );

        LitName.Text = "name='" + this.Name + "'";

        litPreview.Text = PreviewUrl;

        hidParameters.Value = EncodedParameters;
        BtnDel.Click += btnDel_Click;
        LoadInfo();
    }

    protected string AcceptedMime
    {
        get
        {
            if (string.IsNullOrWhiteSpace(AllowedFileTypes))
                return "image/*";

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
                return "image/*";

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
        Session["ImageUpload_" + UniqueID + "_Deleted"] = "__deleted__";

        if (this.FileDeleted != null)
            this.FileDeleted.Invoke(this, e);
    }

    protected void LoadInfo()
    {
        BoxPreview.Style.Add("background-size", string.Format("{0}px {1}px", ConfigurationManager.AppSettings["thumbWidth"], ConfigurationManager.AppSettings["thumbHeight"]));

        if ((string.IsNullOrWhiteSpace(FilePath) || !File.Exists(Server.MapPath(FilePath))) && Session["ImageUpload_" + UniqueID] == null)
        {
            BoxPreview.Style.Add("opacity", "0");
            return;
        }

        ActionsLayer.Visible = true;

        BoxPreview.Style.Add("background-image", "url('" + PreviewUrl + "')");        

        lblDelete.Attributes.Add("title", GetLabel("delete_image_title", "Confirm delete"));
        lblDelete.Attributes.Add("data-msg-title", GetLabel("delete_image_title", "Confirm delete"));
        lblDelete.Attributes.Add("data-msg-subtitle", GetLabel("delete_image", "Delete image?"));
        lblDelete.Attributes.Add("data-msg-cancel", GetLabel("cancel", "cancel"));
        lblDelete.Attributes.Add("data-msg-confirm", GetLabel("confirm", "confirm"));
    }
    
    protected string PreviewUrl
    {
        get
        {
            if (Session["ImageUpload_" + UniqueID] != null)
                return "/Controls/ImageUpload/ImageUploadModernHandler.ashx?action=preview&parameters=" + EncodedParameters + "&ts=" + new DateTime().Ticks;
            else if (!string.IsNullOrWhiteSpace(FilePath))
            {
                string p = FilePath;
                if (p.StartsWith("~"))
                    p = p.Substring(1);
                return p + "?w=300";
            }

            return string.Empty;
        }
    }

    public void SaveTo(string filePath)
    {
        string tmpFilePath = /*Path.GetTempPath() +*/ Convert.ToString(Session["ImageUpload_" + UniqueID]);
        if (File.Exists(tmpFilePath))
        {
            string directory = FilesHelper.GetDirectory(filePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.Move(tmpFilePath, filePath);

            FileSecurity fs = File.GetAccessControl(filePath);
            fs.SetAccessRuleProtection(false, false);
            File.SetAccessControl(filePath, fs);

            Session.Remove("ImageUpload_" + UniqueID);
        }
    }

    public void CleanSession()
    {
        if (Session["ImageUpload_" + UniqueID] == null)
            return;

        Session.Remove("ImageUpload_" + UniqueID);
    }

    public string GetExtension()
    {
        string tmpFilePath = /*Path.GetTempPath() +*/ Convert.ToString(Session["ImageUpload_" + UniqueID]);
        if (!File.Exists(tmpFilePath))
            return string.Empty;

        return tmpFilePath.Split('.').Last();
    }

    public bool Deleted
    {
        get
        {
            return AbstractUploadHandler.IsDeleted(Context, "ImageUpload", UniqueID);            
        }
    }

    public void PerformDelete()
    {
        if (Session["ImageUpload_" + UniqueID] != null)
        {
            string tmpFilePath = /*Path.GetTempPath() +*/ Convert.ToString(Session["ImageUpload_" + UniqueID]);
            if (File.Exists(tmpFilePath))
                File.Delete(tmpFilePath);                

            Session.Remove("ImageUpload_" + UniqueID);
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
            return Session["ImageUpload_" + UniqueID] != null;
        }
    }

    public bool HasFile
    {
        get
        {
            return !string.IsNullOrWhiteSpace(FilePath) || Session["ImageUpload_" + UniqueID] != null;
        }
    }
}
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
using System.Collections.Generic;
using PigeonCms;
using System.Globalization;
using System.IO;
using System.Diagnostics;


public partial class Controls_FilesManagerModern_default : PigeonCms.BaseModuleControl
{
	private List<string> imagesExtensions = new List<string>(
		new string[] { ".png", ".gif", ".jpg" });


	private FileUploadModernProvider fileUploadProvider = null;


    protected string FolderName
    {
        get
        {
            string res = "";
            if (ViewState["FolderName"] != null)
                res = (string)ViewState["FolderName"];
            else
            {
                    string id = Utility._QueryString("id").ToLower();
                    res = id;
            }
            return res;
        }
        set { ViewState["FolderName"] = value; }
    }


    protected new void Page_Init(object sender, EventArgs e)
    {
        base.Page_Init(sender, e);

		Utility.Script.RegisterClientScriptInclude(
			this, "pgn.FilesManagerModern.uploadfiles", base.CurrViewPath + "UploadFiles.js");


		//foreach (KeyValuePair<string, string> item in Config.CultureList)
		//{
		//	//title
		//	TextBox txt1 = new TextBox();
		//	txt1.ID = "TxtTitle" + item.Value;
		//	txt1.MaxLength = 50;
		//	txt1.CssClass = "adminMediumText";
		//	txt1.ToolTip = item.Key;
		//	PanelTitle.Controls.Add(txt1);
		//	Literal lit1 = new Literal();
		//	lit1.Text = "&nbsp;[<i>" + item.Value + "</i>]<br /><br />";
		//	PanelTitle.Controls.Add(lit1);

		//	//description
		//	TextBox txt2 = new TextBox();
		//	txt2.ID = "TxtDescription" + item.Value;
		//	txt2.TextMode = TextBoxMode.MultiLine;
		//	txt2.Rows = 3;
		//	txt2.CssClass = "adminMediumText";
		//	PanelDescription.Controls.Add(txt2);
		//	Literal lit2 = new Literal();
		//	lit2.Text = "&nbsp;[<i>" + item.Value + "</i>]<br /><br />";
		//	PanelDescription.Controls.Add(lit2);
		//}


		Page.Form.Attributes.Add("enctype", "multipart/form-data");

    }


    protected void Page_Load(object sender, EventArgs e)
    {
        LblOk.Text = "";
        LblErr.Text = "";


		string folderId = Utility._QueryString("id").ToLower();
		if (!string.IsNullOrEmpty(folderId))
			this.FolderName = folderId;

		fileUploadProvider = new FileUploadModernProvider(base.BaseModule.Id, this.FolderName);

		if (!fileUploadProvider.AllowFilesUpload)
		{
			BoxContainer.Visible = false;
		}

		BtnNewFolder.Visible = fileUploadProvider.AllowNewFolder;
		TxtNewFolder.Visible = fileUploadProvider.AllowNewFolder;
		BtnParentFolder.Visible = fileUploadProvider.AllowFoldersNavigation;

		if (fileUploadProvider.AllowFoldersNavigation)
		{
			BtnNewFolder.CssClass += " short-width";
			BtnParentFolder.CssClass += " short-width";
			TxtNewFolder.CssClass += " short-width";
		}

		if (!fileUploadProvider.AllowNewFolder)
		{
			BtnParentFolder.Attributes["class"] += " btn-alone";
		}


        if (!Page.IsPostBack)
        {
			loadFormData();
            loadGrid();
        }
		else
		{
			string eventArg = HttpContext.Current.Request["__EVENTARGUMENT"];
			if (eventArg == "grid")
				loadGrid();
		}

    }


	protected void BtnNewFolder_Click(object sender, EventArgs e)
	{
		try
		{
			//string path = Utility.Encryption.Decrypt(encodedPath, PigeonCms.Config.EncryptKey);
			string path = fileUploadProvider.GetFinalPath;
			string foldername = TxtNewFolder.Text;

			if (!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(foldername))
			{
				new FilesGallery(path, "").CreateFolder(foldername);
				loadGrid();
				TxtNewFolder.Text = "";
			}
		}
		catch (Exception ex)
		{
		}
	}


	protected void BtnParentFolder_Click(object sender, EventArgs e)
	{
		try
		{
			navigateFolder("..");
		}
		catch (Exception ex)
		{
			LblErr.Text = RenderError(ex.ToString());
		}
	}


	protected void repImages_ItemCommand(object source, RepeaterCommandEventArgs e)
	{
		if (fileUploadProvider.AllowFilesDel && e.CommandName == "delete")
		{
			try
			{
				string path = fileUploadProvider.GetFinalPath;
				string fileName = e.CommandArgument.ToString();
				bool isFolder = !fileName.Contains(".");
				if (!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(fileName))
				{
					new FilesGallery(path, "").DeleteByFileName(fileName, isFolder);
				}
				loadGrid();
			}
			catch (Exception ex)
			{
				LblErr.Text = RenderError(ex.ToString());
			}
		}
		else if (fileUploadProvider.AllowFoldersNavigation && e.CommandName == "navigate")
		{
			try
			{
				string foldername = e.CommandArgument.ToString();
				if (!string.IsNullOrEmpty(foldername))
				{
					navigateFolder(foldername);
				}
			}
			catch (Exception ex)
			{
				LblErr.Text = RenderError(ex.ToString());
			}
		}
	}


    protected void repImages_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            FileMetaInfo file = (FileMetaInfo)e.Item.DataItem;

			var BoxContainer = (Panel)e.Item.FindControl("BoxContainer");
            var Box = (Panel)e.Item.FindControl("Box");
			var TxtName = (TextBox)e.Item.FindControl("TxtName");
            var lblSize = (Label)e.Item.FindControl("lblSize");
			var lblDelete = (Label)e.Item.FindControl("lblDelete");
			var BtnDel = (Button)e.Item.FindControl("BtnDel");
			var BtnNavigate = (Button)e.Item.FindControl("BtnNavigate");
			var lblFileName = (Label)e.Item.FindControl("lblFileName");

			string fileTitle = file.Title;
			if (string.IsNullOrEmpty(fileTitle))
				fileTitle = file.FileName;

			if (file.IsFolder)
			{
				//folder
				lblFileName.Text = file.FileName;
				Box.CssClass += " box--folder";
				if (fileUploadProvider.AllowFoldersNavigation)
				{
					//lblFileName.Attributes.Add("data-foldername", file.FileName);
					lblFileName.CssClass += " js-btn-navigatefolder";
					BtnNavigate.CommandArgument = file.FileName;
				}

			}
			else if (imagesExtensions.Contains(file.FileExtension.ToLower()))
            {
                //20160801 - closed issue #63 - FileUploadModern problem with JPG images (uppercase ext)
				//image
				lblFileName.Text = file.FileExtension.ToUpper().Replace(".", "");
				Box.Style.Add("background-image", "url('" + file.FileUrl + "')");
				if (fileUploadProvider.AllowFilesSelection)
				{
					lblFileName.Attributes.Add("data-filename", file.FileName);
					lblFileName.Attributes.Add("data-fileurl", file.FileUrl);
					lblFileName.Attributes.Add("data-filetitle", fileTitle);
					lblFileName.CssClass += " js-selectfile";
				}
            }
            else
            {
				//other files
				lblFileName.Text = file.FileExtension.ToUpper().Replace(".", "");
				Box.CssClass += " box--file";
				if (fileUploadProvider.AllowFilesSelection)
				{
					lblFileName.Attributes.Add("data-filename", file.FileName);
					lblFileName.Attributes.Add("data-fileurl", file.FileUrl);
					lblFileName.Attributes.Add("data-filetitle", fileTitle);
					lblFileName.CssClass += " js-selectfile";
				}
            }

			lblSize.Text = "&nbsp;"; 
			if (!string.IsNullOrEmpty(file.HumanLength)) 
				lblSize.Text = file.HumanLength; 

			//select action
			if (fileUploadProvider.AllowFilesSelection)
			{
				lblFileName.CssClass += " box__filename--linkable";
			}

			//delete action
			lblDelete.Attributes.Add("title", base.GetLabel("delete", "delete"));
			lblDelete.Attributes.Add("data-msg-title", base.GetLabel("delete", "delete"));
			lblDelete.Attributes.Add("data-msg-subtitle", PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION"));
			lblDelete.Attributes.Add("data-msg-cancel", base.GetLabel("cancel", "cancel"));
			lblDelete.Attributes.Add("data-msg-confirm", base.GetLabel("confirm", "confirm"));
			BtnDel.CommandArgument = file.FileName;
			
			if (!fileUploadProvider.AllowFilesDel)
			{
				lblDelete.CssClass += " visibility-hidden";
				BtnDel.Visible = false;
			}

			//edit action
			//TxtName.Attributes.Add("value", file.FileName);
			TxtName.Text = file.FileName;
			TxtName.Attributes.Add("data-filename", file.FileName);
			if (!fileUploadProvider.AllowFilesEdit)
			{
				TxtName.Enabled = false;
			}

			if (file.IsFolder)
			{
				BtnDel.Attributes.Add("data-isfolder", "true");

				if (!fileUploadProvider.AllowFoldersNavigation)
				{
					BoxContainer.Visible = false;
				}
			}
			else
			{
				BtnDel.Attributes.Add("data-isfolder", "false");
			}

        }
    }


    private void loadGrid()
    {
        if (fileUploadProvider.Allowed)
        {
            var files = new List<FileMetaInfo>();
            string searchPattern = "";
			if (fileUploadProvider.FileNameType == FileUploadModernProvider.FileNameTypeEnum.ForceFileName
				&& !string.IsNullOrEmpty(fileUploadProvider.ForcedFilename))
			{
				searchPattern = fileUploadProvider.ForcedFilename + ".*";
			}

            files = new FilesGallery(fileUploadProvider.GetFinalPath, "", searchPattern).GetAll();
            repImages.DataSource = files;
            repImages.DataBind();

            int count = files.Count;

			if (fileUploadProvider.NumOfFilesAllowed > 0 && count >= fileUploadProvider.NumOfFilesAllowed)
				BoxContainer.Visible = false;
            else
				BoxContainer.Visible = true;
        }
    }


	private void loadFormData()
	{
		LitRestrictions.Text = "";
		LitFolder.Text = "";

		var extList = fileUploadProvider.ExtensionsList;
		if (extList.Count > 0)
		{
			foreach (var ext in extList)
			{
				LitRestrictions.Text += ext + "; ";
			}
			LitRestrictions.Text += "<br>";
		}

		if (fileUploadProvider.MaxFileSizeKB > 0)
			LitRestrictions.Text += GetLabel("MaxSize", "Max file size") + " "
				+ Utility.GetFileHumanLength(fileUploadProvider.MaxFileSizeKB * 1024) + "<br>";

		if (fileUploadProvider.MaxFileSizeKB < 0)
			LitRestrictions.Text += GetLabel("DiskQuotaExceeded", "Disk quota exceeded") + "<br>";

		if (fileUploadProvider.NumOfFilesAllowed > 0)
			LitRestrictions.Text += GetLabel("NumOfFilesAllowed", "Max files allowed") 
				+ " " + fileUploadProvider.NumOfFilesAllowed.ToString() + "<br>";

		if (string.IsNullOrEmpty(LitRestrictions.Text))
			LitRestrictions.Text = GetLabel("all", "all");

		if (fileUploadProvider.ShowWorkingPath)
			LitFolder.Text = base.GetLabel("Folder", "Folder") + ": " + fileUploadProvider.GetFinalPath + " <br />";

		// set parameters for upload handler
		hidCurrViewPath.Value = this.CurrViewPath;
		hidFolderName.Value = this.FolderName;
		hidMuduleId.Value = Utility.Encryption.Encrypt(this.BaseModule.Id.ToString(), Config.EncryptKey);
		hidPath.Value = Utility.Encryption.Encrypt(fileUploadProvider.GetFinalPath, PigeonCms.Config.EncryptKey);
	}


	private void navigateFolder(string folder)
	{
		try
		{
			fileUploadProvider = new FileUploadModernProvider(base.BaseModule.Id, this.FolderName);
			string path = fileUploadProvider.GetFinalPath;
			if (folder == "..")
			{
				string currentFolder = this.FolderName;
				string parentFolder = "";
				if (!string.IsNullOrEmpty(currentFolder))
				{
					int separatorPos = currentFolder.LastIndexOf("\\");
					if (separatorPos > -1)
						parentFolder = currentFolder.Substring(0, separatorPos);
				}
				this.FolderName = parentFolder;
			}
			else
			{
				this.FolderName = Path.Combine(this.FolderName, folder);
			}
			//reinit fileUploadProvider
			fileUploadProvider = new FileUploadModernProvider(base.BaseModule.Id, this.FolderName);
			loadFormData();
			loadGrid();
		}
		catch (Exception e)
		{
		}
	}


	private void renameFile(string sourceFileName, string destFileName)
	{
		if (sourceFileName != destFileName)
		{
			try
			{
				string path = fileUploadProvider.GetFinalPath;
				new FilesGallery(path, "")
					.RenameFile(sourceFileName, destFileName);
			}
			catch { }
		}
	}

	#region webmethod


	[PigeonCms.UserControlScriptMethod]
	public static string RenameFile(string encodedPath, string sourceFileName, string destFileName)
	{
		string res = "";
		if (sourceFileName != destFileName)
		{
			try
			{
				string path = Utility.Encryption.Decrypt(encodedPath, PigeonCms.Config.EncryptKey);
				new FilesGallery(path, "")
					.RenameFile(sourceFileName, destFileName);
			}
			catch { }
		}

		return res;
	}
	
	#endregion


	//protected void BtnSave_Click(object sender, EventArgs e)
	//{
	//	LblErr.Text = "";
	//	LblOk.Text = "";

	//	try
	//	{
	//		var o1 = new FileMetaInfo(base.CurrentKey);
	//		var originalFile = new FileMetaInfo(base.CurrentKey);

	//		form2obj(o1);
	//		o1.SaveData();

	//		if (originalFile.FileName != TxtFileName.Text)
	//		{
	//			new FilesGallery(fileUploadProvider.GetFinalPath, "")
	//				.RenameFile(originalFile.FileName, TxtFileName.Text);
	//		}

	//		loadGrid();
	//		LblOk.Text = Utility.GetLabel("RECORD_SAVED_MSG");
	//		MultiView1.ActiveViewIndex = 0;
	//	}
	//	catch (Exception e1)
	//	{
	//		LblErr.Text = Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString();
	//	}
	//	finally
	//	{
	//	}
	//}

	//public void clearForm()
	//{
	//	TxtFileName.Text = "";
	//	foreach (KeyValuePair<string, string> item in Config.CultureList)
	//	{
	//		TextBox t1 = new TextBox();
	//		t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
	//		t1.Text = "";

	//		TextBox t2 = new TextBox();
	//		t2 = (TextBox)PanelDescription.FindControl("TxtDescription" + item.Value);
	//		t2.Text = "";
	//	}
	//}

	//private void form2obj(FileMetaInfo obj)
	//{
	//	obj.TitleTranslations.Clear();
	//	obj.DescriptionTranslations.Clear();
	//	foreach (KeyValuePair<string, string> item in Config.CultureList)
	//	{
	//		TextBox t1 = new TextBox();
	//		t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
	//		obj.TitleTranslations.Add(item.Key, t1.Text);

	//		TextBox t2 = new TextBox();
	//		t2 = (TextBox)PanelDescription.FindControl("TxtDescription" + item.Value);
	//		obj.DescriptionTranslations.Add(item.Key, t2.Text);
	//	}
	//}

	//private void obj2form(FileMetaInfo obj)
	//{
	//	TxtFileName.Text = obj.FileName;
	//	foreach (KeyValuePair<string, string> item in Config.CultureList)
	//	{
	//		string sTitleTranslation = "";
	//		TextBox t1 = new TextBox();
	//		t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
	//		obj.TitleTranslations.TryGetValue(item.Key, out sTitleTranslation);
	//		t1.Text = sTitleTranslation;

	//		string sDescriptionTraslation = "";
	//		TextBox t2 = new TextBox();
	//		t2 = (TextBox)PanelDescription.FindControl("TxtDescription" + item.Value);
	//		obj.DescriptionTranslations.TryGetValue(item.Key, out sDescriptionTraslation);
	//		t2.Text = sDescriptionTraslation;
	//	}
	//}

	//private void editRow(string fileUrl)
	//{
	//	LblOk.Text = "";
	//	LblErr.Text = "";

	//	clearForm();
	//	base.CurrentKey = fileUrl;
	//	if (!string.IsNullOrEmpty(fileUrl))
	//	{
	//		var obj = new FileMetaInfo(base.CurrentKey);
	//		obj2form(obj);
	//	}
	//	MultiView1.ActiveViewIndex = 1;
	//}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web;
using System.Collections;
using System.Reflection;
using System.Globalization;
using System.Web.Security;
using System.Text.RegularExpressions;
using PigeonCms.Core;
using PigeonCms.Core.Helpers;
using PigeonCms.Controls;
using PigeonCms.Controls.ItemFields;
using System.IO;
using System.Web.UI.HtmlControls;

namespace PigeonCms.Modules
{
	public abstract class AutoLayoutItemsAdminControl : ItemsAdminControl
    {
        #region consts

        const int COL_ALIAS_INDEX = 1;
		const int COL_CATEGORY_INDEX = 3;
		const int COL_ACCESSTYPE_INDEX = 4;
		const int COL_ORDER_ARROWS_INDEX = 6;
		const int COL_UPLOADFILES_INDEX = 7;
		const int COL_UPLOADIMAGES_INDEX = 8;
		const int COL_DELETE_INDEX = 9;

        #endregion


        public string TitleItem = "";
        protected bool doEdit = false;
        IItem editingItem;


        #region Abstract Usercontrol (implemented in derived class)

        protected abstract DropDownList _DropSectionsFilter { get; }
		protected abstract DropDownList _DropCategoriesFilter { get; }
		protected abstract DropDownList _DropNew { get; }
		protected abstract DropDownList _DropEnabledFilter { get; }

		protected abstract DropDownList _DropCategories { get; }
		protected abstract DropDownList _DropItemTypesFilter { get; }

		protected abstract Panel _PanelInsert { get; }
		protected abstract Panel _PanelDescription { get; }
		protected abstract Literal _LitFieldsTabs { get; }
		protected abstract PlaceHolder _FieldsContainer { get; }

        protected abstract Literal _LitTemplateResources { get; }

        protected abstract Repeater _Rep1 { get; }
		protected abstract Repeater _RepPaging { get; }

		protected abstract UpdatePanel _Upd1 { get; }

		protected abstract ITextControl _LblErrInsert { get; }
		protected abstract ITextControl _LblErrSee { get; }
		protected abstract ITextControl _LblOkInsert { get; }
		protected abstract ITextControl _LblOkSee { get; }
		protected abstract ITextControl _LitItemType { get; }

		protected abstract Button _BtnNew { get; }
		protected abstract Button _BtnSave { get; }
		protected abstract Button _BtnCancel { get; }

		protected abstract ITextControl _LblId { get; }
		protected abstract ITextControl _LblOrderId { get; }
		protected abstract ITextControl _LblUpdated { get; }
		protected abstract ITextControl _LblCreated { get; }
		protected abstract ITextControl _LitSection { get; }
		protected abstract CheckBox _ChkEnabled { get; }
		protected abstract TextBox _TxtAlias { get; }
		protected abstract ITextControl _TxtCssClass { get; }
		protected abstract ITextControl _TxtExtId { get; }
		protected abstract Panel _PanelTitle { get; }

		protected abstract TextBox _TxtItemDate { get; }
		protected abstract TextBox _TxtValidFrom { get; }
		protected abstract TextBox _TxtValidTo { get; }

		protected abstract PigeonCms.Controls.PermissionsControl _PermissionsControl { get; }
		protected abstract PigeonCms.Controls.SeoControl _SeoControl { get; }
		protected abstract PigeonCms.Controls.ItemParamsControl _ItemParams { get; }
		protected abstract PigeonCms.Controls.ItemParamsControl _ItemFields { get; }

		protected abstract HiddenField _HidCurrentItemType { get; }
        protected abstract IPageComposer _PageComposer { get; }

        #endregion

        #region init

        protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			string titleId = "";
			foreach (KeyValuePair<string, string> item in Config.CultureList)
			{
				//title
				base.AddTransText("TxtTitle", _PanelTitle, base.ContentEditorConfig, item, 200, "form-control");

				//description
				Literal lit2 = new Literal();
				if (!this.ShowOnlyDefaultCulture)
					lit2.Text = "<span class='lang-description'>- <i>" + item.Value + "</i> -</span>";
				_PanelDescription.Controls.Add(lit2);

				Control txt2 = LoadControl("~/Controls/ContentEditorControl.ascx");
				txt2.ID = "TxtDescription" + item.Value;
				if (txt2 != null && txt2 is PigeonCms.Controls.IContentEditorControl)
				{
					((IContentEditorControl)txt2).Configuration = base.ContentEditorConfig;
				}
				LabelsProvider.SetLocalizedControlVisibility(this.ShowOnlyDefaultCulture, item.Key, txt2);
				_PanelDescription.Controls.Add(txt2);
			}

			if (this.BaseModule.DirectEditMode)
			{
			}

			//*** restrictions TODO
			
            //Grid1.AllowSorting = false;
			//Grid1.Columns[COL_ORDER_ARROWS_INDEX].Visible = this.AllowOrdering;
			//Grid1.Columns[COL_UPLOADFILES_INDEX].Visible = this.TargetFilesUpload > 0;
			//Grid1.Columns[COL_UPLOADIMAGES_INDEX].Visible = this.TargetImagesUpload > 0;
			//Grid1.Columns[COL_DELETE_INDEX].Visible = base.AllowDelete;
			//Grid1.Columns[COL_ALIAS_INDEX].Visible = this.ShowAlias;
			//Grid1.Columns[COL_ACCESSTYPE_INDEX].Visible = this.ShowSecurity;

			_TxtAlias.Attributes.Add("onfocus", "preloadAlias('" + titleId + "', this)");
			_TxtAlias.Visible = this.ShowAlias;

			_PermissionsControl.Visible = this.ShowSecurity;
			_SeoControl.Visible = this.ShowSeo;

			_TxtItemDate.Visible = this.ShowDates;
			_TxtValidFrom.Visible = this.ShowDates;
			_TxtValidTo.Visible = this.ShowDates;

			_DropEnabledFilter.Visible = this.ShowEnabledFilter;
			_ItemFields.Visible = this.ShowFieldsPanel;
			_ItemParams.Visible = this.ShowParamsPanel;

			_ItemFields.Title = base.GetLabel("LblFields", "Fields", null, true);
			_ItemParams.Title = base.GetLabel("LblParameters", "Parameters", null, true);


			//*** handle events
			_DropNew.SelectedIndexChanged += DropNew_SelectedIndexChanged;
			_DropEnabledFilter.SelectedIndexChanged += DropEnabledFilter_SelectedIndexChanged;
			_DropSectionsFilter.SelectedIndexChanged += DropSectionsFilter_SelectedIndexChanged;
			_DropCategoriesFilter.SelectedIndexChanged += DropCategoriesFilter_SelectedIndexChanged;
			_DropItemTypesFilter.SelectedIndexChanged += DropItemTypesFilter_SelectedIndexChanged;

            _Rep1.ItemDataBound += Rep1_ItemDataBound;
			_Rep1.ItemCommand += Rep1_ItemCommand;
			_RepPaging.ItemCommand += RepPaging_ItemCommand;
			_RepPaging.ItemDataBound += RepPaging_ItemDataBound;

            _BtnNew.Click += BtnNew_Click;
			_BtnSave.Click += BtnSave_Click;
			_BtnCancel.Click += BtnCancel_Click;


            // Recreate form to handle control events and viewstate
            // TODO: avoid on "list mode" postbacks
            ItemsProxy itemsProxy;
            if (!string.IsNullOrWhiteSpace(Request.Params["__EVENTARGUMENT"]))
            {
                if (Regex.IsMatch(Request.Params["__EVENTARGUMENT"], @"^[0-9]+\|.*"))
                {
                    string[] arguments = Request.Params["__EVENTARGUMENT"].Split('|');
                    int argItemId = Convert.ToInt32(arguments[0]);
                    string argItemType = arguments[1];

                    itemsProxy = new ItemsProxy(argItemType, true, true);
                    itemsProxy.LogExceptions = false;
                    itemsProxy.ThrowExceptions = false;
                    editingItem = itemsProxy.GetByKey(argItemId);
                    doEdit = true;

                    LoadItemDynamicFields(editingItem, false);

                }
            }
            else
            { 
                itemsProxy = new ItemsProxy(this.CurrentItemType, true, true);
                itemsProxy.LogExceptions = false;
                itemsProxy.ThrowExceptions = false;
                editingItem = itemsProxy.GetByKey(this.CurrentId);

                LoadItemDynamicFields(editingItem, false);
            }


            ItemsAdminHelper.RegisterCss("FieldContainer/FieldContainer", Page);
			//ItemsAdminHelper.RegisterCss("ImageUpload/ImageUpload", Page);

			ItemsAdminHelper.InsertJsIntoPageScriptManager("ImageUpload/ImageUploadModern", Page);
			ItemsAdminHelper.InsertJsIntoPageScriptManager("ImageUpload/FileUploadModern", Page);
            ContentEditorProvider.InitEditor(this, _Upd1, base.ContentEditorConfig);


            //allow file upload
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			setSuccess("");
			setError("");

			if (this.BaseModule.DirectEditMode)
			{
				if (base.CurrItem.Id == 0)
					throw new ArgumentException();
			}

			if (!Page.IsPostBack)
			{
				loadDropEnabledFilter();
				loadDropSectionsFilter(base.SectionId);
				{
					int secId = -1;
					int.TryParse(_DropSectionsFilter.SelectedValue, out secId);
					loadDropCategoriesFilter(secId);
				}
				loadDropsItemTypes();
				loadList();
			}

			if (Page.IsPostBack)
			{
				//var partial = Upd1.IsInPartialRendering;

				string eventArg = HttpContext.Current.Request["__EVENTARGUMENT"];
				if (eventArg == "items")
					loadList();
				else if (eventArg == "sortcomplete")
				{
					updateSortedTable();
					loadList();
				}
                else if (doEdit)
                {
                    editRow(editingItem, null);
                }

                //var 
            }
			OnAfterLoad();
		}

		protected virtual void OnAfterLoad()
		{
			if (BaseModule.DirectEditMode)
			{
				//DivDropNewContainer.Visible = false;
				_BtnNew.Visible = false;
				_BtnCancel.OnClientClick = "closePopup();";
				//editRow(base.CurrItem, null);
			}
		}

		#endregion


		protected void DropEnabledFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			loadList();
		}

		protected void DropSectionsFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			int secID = 0;
			int.TryParse(_DropSectionsFilter.SelectedValue, out secID);

			loadDropCategoriesFilter(secID);
			loadDropCategories(secID);
			loadList();

			base.LastSelectedSectionId = secID;
		}

		protected void DropCategoriesFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			int catID = 0;
			int.TryParse(_DropCategoriesFilter.SelectedValue, out catID);

			loadList();

			base.LastSelectedCategoryId = catID;
		}

		protected void DropItemTypesFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			loadList();
		}

		protected void DropNew_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!checkAddNewFilters())
			{
				Utility.SetDropByValue(_DropNew, "");
			}
			else
			{
				try
				{
					editRow(null, _DropNew.SelectedValue);
				}
				catch (Exception e1)
				{
					setError(e1.Message);
				}
			}
		}

		protected void RepPaging_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Header)
				return;

			int page = int.Parse(e.Item.DataItem.ToString());
			if (page - 1 == base.ListCurrentPage)
			{
				var BtnPage = (LinkButton)e.Item.FindControl("BtnPage");
				BtnPage.CssClass = "selected";
			}
		}

		protected void RepPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			if (e.CommandName == "Page")
			{
				base.ListCurrentPage = int.Parse(e.CommandArgument.ToString()) - 1;
				loadList();
			}
		}

		protected void Rep1_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Header)
				return;

			var item = (IItem)e.Item.DataItem;

			var LitEnabled = (Literal)e.Item.FindControl("LitEnabled");
			string enabledClass = "";
			if (item.Enabled)
				enabledClass = "checked";
			LitEnabled.Text = "<span class='table-modern--checkbox--square " + enabledClass + "'></span>";


            //20170523 only works if code behind
            var LnkEdit = (LinkButton)e.Item.FindControl("LnkEdit");
            if (LnkEdit != null)
            {
                LnkEdit.OnClientClick = "__doPostBack('" + LnkEdit.ClientID + "', '" + item.Id + "|" + item.ItemTypeName + "'); return false;";
            }


            var LnkEnabled = (LinkButton)e.Item.FindControl("LnkEnabled");
			LnkEnabled.CssClass = "table-modern--checkbox " + enabledClass;
			LnkEnabled.CommandName = item.Enabled ? "Enabled0" : "Enabled1";


			var LitTitle = (Literal)e.Item.FindControl("LitTitle");
			if (!item.IsThreadRoot)
				LitTitle.Text += "--";
			LitTitle.Text += Utility.Html.GetTextPreview(item.Title, 50, "");
			if (string.IsNullOrEmpty(item.Title))
				LitTitle.Text += Utility.GetLabel("NO_VALUE", "<no value>");
			if (base.ShowSecurity && Roles.IsUserInRole("debug"))
				LitTitle.Text += " [" + item.Id.ToString() + "]";


			var LitItemDate = (Literal)e.Item.FindControl("LitItemDate");
			LitItemDate.Text = item.DateUpdated.ToString();

			var LitItemInfo = (Literal)e.Item.FindControl("LitItemInfo");
			if (!string.IsNullOrEmpty(item.ExtId))
				LitItemInfo.Text += "extId: <strong>" + item.ExtId + "</strong><br>";
			if (this.ShowType)
				LitItemInfo.Text += item.ItemTypeName + "<br>";
			if (!string.IsNullOrEmpty(item.CssClass))
				LitItemInfo.Text += "class: " + item.CssClass;


			if (item.CategoryId > 0)
			{
				var mgr = new CategoriesManager();
				var cat = mgr.GetByKey(item.CategoryId);
				var LitCategoryTitle = (Literal)e.Item.FindControl("LitCategoryTitle");
				var parentCat = mgr.GetByKey(cat.ParentId);
				LitCategoryTitle.Text = (cat.ParentId > 0 ? parentCat.Title + @" \ " : "") + cat.Title;
			}

			//permissions
			var LitAccessTypeDesc = (Literal)e.Item.FindControl("LitAccessTypeDesc");
			LitAccessTypeDesc.Text = RenderAccessTypeSummary(item);

			//files upload
			var LnkUploadFiles = (HyperLink)e.Item.FindControl("LnkUploadFiles");
			LnkUploadFiles.NavigateUrl = this.FilesUploadUrl
				+ "?type=items&id=" + item.Id.ToString();
			LnkUploadFiles.Visible = this.TargetFilesUpload > 0;


			var LitFilesCount = (Literal)e.Item.FindControl("LitFilesCount");
			int filesCount = item.Files.Count;
			LitFilesCount.Text = "&nbsp;";
			if (filesCount > 0)
			{
				LitFilesCount.Text = filesCount.ToString();
				LitFilesCount.Text += filesCount == 1 ? " file" : " files";
				LitFilesCount.Text += " / " + Utility.GetFileHumanLength(item.FilesSize);
			}

			//images upload
			var LnkUploadImg = (HyperLink)e.Item.FindControl("LnkUploadImg");
			LnkUploadImg.NavigateUrl = this.ImagesUploadUrl
				+ "?type=items&id=" + item.Id.ToString();
			LnkUploadImg.Visible = this.TargetImagesUpload > 0;


			var LitImgCount = (Literal)e.Item.FindControl("LitImgCount");
			int imgCount = item.Images.Count;
			LitImgCount.Text = "&nbsp;";
			if (imgCount > 0)
			{
				LitImgCount.Text = imgCount.ToString();
				LitImgCount.Text += imgCount == 1 ? " file" : " files";
				LitImgCount.Text += " / " + Utility.GetFileHumanLength(item.ImagesSize);
			}
		}

		protected void Rep1_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			string argument = e.CommandArgument.ToString();
			if (string.IsNullOrWhiteSpace(argument))
				return;

			string[] idAndType = argument.Split('|');
			if (idAndType.Length < 1)
				return;

			int itemId = 0;
			if (!int.TryParse(idAndType[0], out itemId))
				return;

            string itemType = idAndType[1];

            try
			{
				switch (e.CommandName)
				{
					case "Select":
                        var itemsProxy = new ItemsProxy(itemType, true, true);
                        var item = itemsProxy.GetByKey(itemId);
						editRow(item, itemType);
						break;

					case "DeleteRow":
						deleteRow(itemId, itemType);
						break;

					case "Enabled0":
						setFlag(itemId, itemType, false, "enabled");
						loadList();
						break;

					case "Enabled1":
						setFlag(itemId, itemType, true, "enabled");
						loadList();
						break;
				}
			}
			catch (Exception e1)
			{
				setError(e1.Message);
			}
		}

        protected void BtnNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkAddNewFilters())
                {
                    string itemType = new Item().ItemTypeName;//default value
                    Utility.SetDropByValue(_DropNew, itemType);
                    if (this.AllowedItems.Count > 0)
                        itemType = this.AllowedItems.First().Key;

                    editRow(null,  itemType);
                }
            }
            catch (Exception e1)
            {
                setError(e1.Message);
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
		{
			if (checkForm())
			{
				if (saveForm())
					showInsertPanel(false);
			}
		}

		protected void BtnCancel_Click(object sender, EventArgs e)
		{
			OnBeforeCancel();

            foreach(var propDef in CurrItem.PropertiesList)
            {
                string propDefName = propDef.MapAttributeValue;

                List<PropertyInfo> properties = GetItemPropertiesInfo(propDef);
                if (properties == null || properties.Count == 0)
                    continue;

                foreach (PropertyInfo property in properties)
                {
                    IUploadControl imageUpload = Utility.Controls.FindControlRecursive<Control>(
                        _FieldsContainer, "property_" + propDefName + "-" + property.Name) as IUploadControl;

                    if (imageUpload != null)
                        imageUpload.CleanSession();
                }
            }

            setError("");
			setSuccess("");
			showInsertPanel(false);
			OnAfterCancel();
		}

		protected virtual void OnBeforeCancel()
		{

		}

		protected virtual void OnAfterCancel()
		{

		}

		protected virtual void OnAfterUpdate(IItem item)
		{

		}


		#region private methods

		private bool checkForm()
		{
			setError("");
			setSuccess("");
			bool res = true;
			string err = "";

			int catId = 0;
			int.TryParse(_DropCategories.SelectedValue, out catId);
			if (catId <= 0)
			{
				res = false;
				err += base.GetLabel("ChooseCategory", "Choose a category before") + "<br>";
			}

			//if (!string.IsNullOrEmpty(TxtAlias.Text))
			//{
			//	var filter = new ItemsFilter();
			//	var list = new List<PigeonCms.Item>();

			//	filter.Alias = TxtAlias.Text;
			//	list = new ItemsManager<Item, ItemsFilter>().GetByFilter(filter, "");
			//	if (list.Count > 0)
			//	{
			//		if (this.CurrentId == 0)
			//		{
			//			res = false;
			//			err += "alias in use<br />";
			//		}
			//		else
			//		{
			//			if (list[0].Id != this.CurrentId)
			//			{
			//				res = false;
			//				err += "alias in use<br />";
			//			}
			//		}
			//	}
			//}
			setError(err);
			return res;
		}

		protected virtual bool saveForm()
		{
			bool res = false;
			setError("");
			setSuccess("");

			try
			{
                var proxy = new ItemsProxy(this.CurrentItemType, true, true);
                var o1 = proxy.GetNewItem();
				
				if (CurrentId == 0)
				{
					form2obj(o1);
					o1 = proxy.Insert(o1);
                    //store after insert to save file resource in the right place
                    o1 = proxy.GetByKey(o1.Id);
                    StoreDynamicFields(o1);
                    proxy.Update(o1);
                }
				else
				{
                    o1 = proxy.GetByKey(this.CurrentId);
                    form2obj(o1);
                    StoreDynamicFields(o1);
                    proxy.Update(o1);
				}

				OnAfterUpdate(o1);
				removeFromCache();

				loadList();
				setSuccess(Utility.GetLabel("RECORD_SAVED_MSG"));
				res = true;
			}
			catch (CustomException e1)
			{
				if (e1.CustomMessage == ItemsManager.MaxItemsException)
					setError(base.GetLabel("LblMaxItemsReached", "you have reached the maximum number of elements"));
				else
					setError(e1.CustomMessage);
			}
			catch (Exception e1)
			{
				setError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
			}
			finally
			{
			}
			return res;
		}

		private void clearForm()
		{
			TitleItem = "";
			_LblId.Text = "";
			_LblOrderId.Text = "";
			_LitSection.Text = "";
			_LitItemType.Text = "";
			_LblCreated.Text = "";
			_LblUpdated.Text = "";
			_ChkEnabled.Checked = true;
			_TxtAlias.Text = "";
			_TxtCssClass.Text = "";
			_TxtExtId.Text = "";
			this.ItemDate = DateTime.MinValue;
			this.ValidFrom = DateTime.MinValue;
			this.ValidTo = DateTime.MinValue;

			foreach (KeyValuePair<string, string> item in Config.CultureList)
			{
				var t1 = (TextBox)_PanelTitle.FindControl("TxtTitle" + item.Value);
				t1.Text = "";

				//var txt2 = new Controls_ContentEditorControl();
				ITextControl txt2 = Utility.FindControlRecursive<Control>(this, "TxtDescription" + item.Value) as ITextControl;
				txt2.Text = "";
			}
			_PermissionsControl.ClearForm();
			_SeoControl.ClearForm();
		}

		private void form2obj(IItem obj)
		{
			obj.ItemParams = FormBuilder.GetParamsString(obj.ItemType.Params, _ItemParams);
			string fieldsString = FormBuilder.GetParamsString(obj.ItemType.Fields, _ItemFields);
			obj.LoadCustomFieldsFromString(fieldsString);

			obj.Id = CurrentId;
			obj.Enabled = _ChkEnabled.Checked;
			obj.TitleTranslations.Clear();
			obj.DescriptionTranslations.Clear();

			int catId = CategoryId;
			obj.CategoryId = int.TryParse(_DropCategories.SelectedValue, out catId) ? catId : CategoryId;

			obj.Alias = _TxtAlias.Text;
			obj.CssClass = _TxtCssClass.Text;
			obj.ExtId = _TxtExtId.Text;
			obj.ItemDate = this.ItemDate;
			obj.ValidFrom = this.ValidFrom;
			obj.ValidTo = this.ValidTo;

			if (CurrentId == 0)
				obj.ItemTypeName = _LitItemType.Text;

			foreach (KeyValuePair<string, string> item in Config.CultureList)
			{
				var t1 = (TextBox)_PanelTitle.FindControl("TxtTitle" + item.Value);
				obj.TitleTranslations.Add(item.Key, t1.Text);

				ITextControl txt2 = Utility.Controls.FindControlRecursive<Control>(this, "TxtDescription" + item.Value) as ITextControl;
				if (txt2 != null)
					obj.DescriptionTranslations.Add(item.Key, txt2.Text);
			}

			_PermissionsControl.Form2obj(obj);
			_SeoControl.Form2obj(obj);
			//storeDynamicFields(obj);

			//obj.Alias = GetAlias(obj as BaseItem);
			//CheckTitle(obj as BaseItem);
		}

		protected virtual void obj2form(IItem obj)
		{
			loadItemCommonData(obj);
			LoadItemDynamicFields(obj);

			_ItemParams.ClearParams();
			_ItemParams.LoadParams(obj);

			_ItemFields.ClearParams();
			_ItemFields.LoadFields(obj);

            _PageComposer.Load(obj);
		}

		protected virtual void editRow(IItem obj, string itemType)
		{
			setSuccess("");
			setError("");

			if (!PgnUserCurrent.IsAuthenticated)
				throw new Exception("user not authenticated");
            
            clearForm();

            var proxy = new ItemsProxy(itemType, true, true);

			if (obj == null || obj.Id == 0)
			{
				obj = proxy.GetNewItem();
				int sectionId = int.Parse(_DropSectionsFilter.SelectedValue);
				loadDropCategories(sectionId);

				obj.Enabled = true;
				obj.ItemTypeName = _DropNew.SelectedValue;
				obj.ItemDate = DateTime.Now;
				obj.ValidFrom = DateTime.Now;
				obj.ValidTo = DateTime.MinValue;
				int defaultCategoryId = 0;
				int.TryParse(_DropCategoriesFilter.SelectedValue, out defaultCategoryId);
				if (defaultCategoryId == 0)
				{
					//retrieve first category in selected section
					var cman = new CategoriesManager();
					var cfilter = new CategoriesFilter();
					cfilter.SectionId = sectionId;
					var clist = cman.GetByFilter(cfilter, "");
					if (clist.Count > 0)
						defaultCategoryId = clist[0].Id;
				}
				obj.CategoryId = defaultCategoryId;
				obj2form(obj);
				_LitItemType.Text = _DropNew.SelectedValue;
			}
			else
			{
				loadDropCategories(obj.SectionId);
				obj2form(obj);
			}

			Utility.SetDropByValue(_DropNew, "");
			this.CurrentId = obj.Id;
			this.CurrentItemType = obj.ItemTypeName;//set hidden field too
			_ItemParams.LoadParams(obj);
			_ItemFields.LoadFields(obj);

            //TOCHECK
            _PageComposer.RegisterScripts();

            showInsertPanel(true);

        }

		protected void deleteRow(int recordId, string itemType)
		{
			setSuccess("");
			setError("");

			try
			{
				if (!PgnUserCurrent.IsAuthenticated)
					throw new Exception("user not authenticated");

                var proxy = new ItemsProxy(itemType, true, true);
                proxy.DeleteById(recordId);

				removeFromCache();
			}
			catch (Exception e)
			{
				setError(e.Message);
			}
			loadList();
		}

		private void loadList()
		{
            //TODO - list with multiple itemtypes
            var itemsProxy = new ItemsProxy(this.ItemType, true, true);
            var filter = itemsProxy.GetNewItemFilter();

			filter.Enabled = Utility.TristateBool.NotSet;
			switch (_DropEnabledFilter.SelectedValue)
			{
				case "1":
					filter.Enabled = Utility.TristateBool.True;
					break;
				case "0":
					filter.Enabled = Utility.TristateBool.False;
					break;
				default:
					filter.Enabled = Utility.TristateBool.NotSet;
					break;
			}

            //TODO allora filter witu multiple itemtypes
			if (_DropItemTypesFilter.SelectedValue != "")
				filter.ItemType = _DropItemTypesFilter.SelectedValue;


			if (this.ItemId > 0)
				filter.Id = this.ItemId;

			int secId = -1;
			int.TryParse(_DropSectionsFilter.SelectedValue, out secId);

			int catId = -1;
			int.TryParse(_DropCategoriesFilter.SelectedValue, out catId);
			if (catId <= 0)
				catId = CategoryId;

			filter.SectionId = secId;
			if (base.SectionId > 0)
				filter.SectionId = base.SectionId;

			/*filter.CategoryId = catId;
			if (base.CategoryId > 0)
				filter.CategoryId = base.CategoryId;*/

			var list = itemsProxy.GetByFilter(filter, "");

			//post filter on itemTypes
			if (!string.IsNullOrWhiteSpace(ItemTypes))
			{
				var itemTypeList = new List<string>(ItemTypes.Split(','));
                list = (list.Where(
                        i => { return itemTypeList.Contains(i.ItemTypeName); })
                    ).ToList();
			}

            //TODO - to complete - filter catid and parent in GetByFilter method
			//post filter on catId 
			if (catId > 0)
			{
				list = (list.Where(i =>
				{
					return (i.CategoryId == catId /*|| i..Category.ParentId == catId*/);
				})).ToList();
			}

			var ds = new PagedDataSource();
			ds.DataSource = list;
			ds.AllowPaging = true;
			ds.PageSize = base.ListPageSize;
			ds.CurrentPageIndex = base.ListCurrentPage;

			_RepPaging.Visible = false;
			if (ds.PageCount > 1)
			{
				_RepPaging.Visible = true;
				ArrayList pages = new ArrayList();
				for (int i = 0; i <= ds.PageCount - 1; i++)
				{
					pages.Add((i + 1).ToString());
				}
				_RepPaging.DataSource = pages;
				_RepPaging.DataBind();
			}

			_Rep1.DataSource = ds;
			_Rep1.DataBind();
		}

		private void loadDropEnabledFilter()
		{
			_DropEnabledFilter.Items.Clear();
			_DropEnabledFilter.Items.Add(new ListItem(Utility.GetLabel("LblSelectState", "Select state"), ""));
			_DropEnabledFilter.Items.Add(new ListItem("On-line", "1"));
			_DropEnabledFilter.Items.Add(new ListItem("Off-line", "0"));
		}

		private void loadDropSectionsFilter(int sectionId)
		{
			_DropSectionsFilter.Items.Clear();

			var secFilter = new SectionsFilter();
			secFilter.Id = sectionId;
			var secList = new SectionsManager(true, true).GetByFilter(secFilter, "");

			foreach (var sec in secList)
			{
				_DropSectionsFilter.Items.Add(new ListItem(sec.Title, sec.Id.ToString()));
			}
			if (base.LastSelectedSectionId > 0)
				Utility.SetDropByValue(_DropSectionsFilter, base.LastSelectedSectionId.ToString());
		}

		private void loadDropCategoriesFilter(int sectionId)
		{
			_DropCategoriesFilter.Items.Clear();
			_DropCategoriesFilter.Items.Add(new ListItem(Utility.GetLabel("LblSelectCategory", "Select category"), "0"));

			var catFilter = new CategoriesFilter();
			catFilter.SectionId = sectionId;
			catFilter.Id = base.CategoryId;
			if (catFilter.SectionId == 0)
				catFilter.Id = -1;

			var list = new CategoriesManager(true, true).GetByFilter(catFilter, "");
			loadListCategories(_DropCategoriesFilter, list, 0, 0, base.ShowItemsCount);

			if (base.LastSelectedCategoryId > 0)
				Utility.SetDropByValue(_DropCategoriesFilter, base.LastSelectedCategoryId.ToString());

			if (base.CategoryId > 0)
				Utility.SetDropByValue(_DropCategoriesFilter, base.CategoryId.ToString());
		}

		private void loadDropCategories(int sectionId)
		{
			_DropCategories.Items.Clear();

			var catFilter = new CategoriesFilter();
			catFilter.SectionId = sectionId;
			catFilter.Id = base.CategoryId;
			if (catFilter.SectionId == 0)
				catFilter.Id = -1;
			var list = new CategoriesManager().GetByFilter(catFilter, "");
			loadListCategories(_DropCategories, list, 0, 0);
		}

		private void loadListCategories(DropDownList drop,
			List<Category> list, int parentId, int level, bool showItemsCount = false)
		{
			var nodes = list.Where(x => x.ParentId == parentId);
			foreach (var item in nodes)
			{
				string levelString = "";
				for (int i = 0; i < level; i++)
				{
					levelString += ". . ";
				}

				var listItem = new ListItem();
				listItem.Value = item.Id.ToString();
				listItem.Text = levelString + item.Title;
				if (showItemsCount)
				{
					var iman = new ItemsManager<Item, ItemsFilter>();
					var ifilter = new ItemsFilter();
					ifilter.CategoryId = item.Id;
					int count = iman.GetByFilter(ifilter, "").Count;
					if (count > 0)
						listItem.Text += " (" + count.ToString() + ")";
				}
				drop.Items.Add(listItem);

				loadListCategories(drop, list, item.Id, level + 1, showItemsCount);
			}
		}

		private void loadDropsItemTypes()
		{

			_DropNew.Items.Clear();
			_DropNew.Items.Add(new ListItem(Utility.GetLabel("LblCreateNew", "Create new"), ""));

			_DropItemTypesFilter.Items.Clear();
			_DropItemTypesFilter.Items.Add(new ListItem(Utility.GetLabel("LblSelectItem", "Select item"), ""));

			var filter = new ItemTypeFilter();
			List<ItemType> recordList = new ItemTypeManager().GetByFilter(filter, "FullName");
            if (this.AllowedItems.Count > 0)
            {
                recordList = new List<ItemType>(recordList.Where(
                    i => { return this.AllowedItems.ContainsKey(i.FullName); }));
            }

			
			foreach (ItemType record1 in recordList)
			{
				string itemFriendlyName = GetLabel("ItemTypeName." + record1.FullName, record1.FullName);

				_DropNew.Items.Add(new ListItem(itemFriendlyName, record1.FullName));
				_DropItemTypesFilter.Items.Add(new ListItem(itemFriendlyName, record1.FullName));
			}

			if (this.AllowedItems.Count == 1)
			{
				_BtnNew.Visible = true;
				_DropNew.Visible = false;
				_DropItemTypesFilter.Visible = false;
			}
			else
			{
				_BtnNew.Visible = false;
				_DropNew.Visible = true;
				_DropItemTypesFilter.Visible = true;
			}
		}

		private void setFlag(int recordId, string itemType, bool value, string flagName)
		{
			try
			{
				if (!PgnUserCurrent.IsAuthenticated)
					throw new Exception("user not authenticated");

                var proxy = new ItemsProxy(itemType, true, true);
                var o1 = proxy.GetByKey(recordId);
				switch (flagName.ToLower())
				{
					case "enabled":
						o1.Enabled = value;
						break;
					default:
						break;
				}
				proxy.Update(o1);
				removeFromCache();
			}
			catch (Exception e1)
			{
				setError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
			}
			finally { }
		}

		private void updateSortedTable()
		{
			string[] rowIds = Request.Form.GetValues("RowId");
			int ordering = 1;

			foreach (string sid in rowIds)
			{
				int id = 0;
				int.TryParse(sid, out id);
				sortRecord(id, ordering);
				ordering++;
			}
		}

		public void sortRecord(int id, int ordering)
		{
			var man = new ItemsManager<Item, ItemsFilter>(true, true);
			var item = man.GetByKey(id);
			item.Ordering = ordering;
			man.Update(item);
		}

		private bool checkAddNewFilters()
		{
			bool res = true;
			if (_DropSectionsFilter.SelectedValue == "0" || _DropSectionsFilter.SelectedValue == "")
			{
				setError(base.GetLabel("ChooseSection", "Choose a section before"));
				res = false;
			}

			return res;
		}

        /// <summary>
        /// function for display insert panel
        /// </summary>
        protected void showInsertPanel(bool toShow)
		{

			PigeonCms.Utility.Script.RegisterStartupScript(_Upd1, "bodyBlocked", "bodyBlocked(" + toShow.ToString().ToLower() + ");");

			if (toShow)
				_PanelInsert.Visible = true;
			else
				_PanelInsert.Visible = false;
		}

        /// <summary>
        /// load current item (obj) common field values in form
        /// </summary>
		protected void loadItemCommonData(IItem obj)
		{
			_LblId.Text = obj.Id.ToString();
			_LblOrderId.Text = obj.Ordering.ToString();
			_LblUpdated.Text = obj.DateUpdated.ToString() + " by " + obj.UserUpdated;
			_LblCreated.Text = obj.DateInserted.ToString() + " by " + obj.UserInserted;
			_ChkEnabled.Checked = obj.Enabled;
			_TxtAlias.Text = obj.Alias;
			_TxtCssClass.Text = obj.CssClass;
			_TxtExtId.Text = obj.ExtId;
			Utility.SetDropByValue(_DropCategories, obj.CategoryId.ToString());

			foreach (KeyValuePair<string, string> item in Config.CultureList)
			{
				string sTitleTranslation = "";
				TextBox t1 = (TextBox)_PanelTitle.FindControl("TxtTitle" + item.Value);
				obj.TitleTranslations.TryGetValue(item.Key, out sTitleTranslation);
				t1.Text = sTitleTranslation;

				//Set title edit item
				if (string.IsNullOrEmpty(TitleItem))
					TitleItem = sTitleTranslation;

				string sDescriptionTraslation = "";
				Control txt2 = Utility.FindControlRecursive<Control>(this, "TxtDescription" + item.Value);
				obj.DescriptionTranslations.TryGetValue(item.Key, out sDescriptionTraslation);
				if (txt2 is ITextControl)
					((ITextControl)txt2).Text = sDescriptionTraslation;
			}

			_PermissionsControl.Obj2form(obj);
			_SeoControl.Obj2form(obj);

			//retrieve category and section
			{
				var catMan = new CategoriesManager();
				var cat = catMan.GetByKey(obj.CategoryId);
				_LitSection.Text = cat.Section.Title;
			}
			_LitItemType.Text = obj.ItemTypeName;

			ItemDate = obj.ItemDate;
			ValidFrom = obj.ValidFrom;
			ValidTo = obj.ValidTo;
		}


		#endregion

		#region Dynamic properties management

		protected List<PropertyInfo> GetItemPropertiesInfo(ItemPropertiesDefs itemPropertyDef)
		{
            //var res = new Dictionary<string, PropertyInfo>();

            if (itemPropertyDef == null)
                return null;

            Type type = itemPropertyDef.GetType();
			var list = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
            return list;
		}


        /// <summary>
        /// load current item (obj) dynamic fields values in form
        /// item.PropertiesList --> form
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="setFieldValues"></param>
		protected void LoadItemDynamicFields(IItem obj, bool setFieldValues = true)
        {
            IItem item = obj as IItem;
            if (item == null)
                return;

            //remove all dynamic controls
            _FieldsContainer.Controls.Clear();

            var template = getItemTemplate(obj);

            //load template resources
            _LitTemplateResources.Text = "";
            _LitTemplateResources.Text += template.GetCssFilesContent()
                    .Replace("[[itemType]]", obj.ItemTypeName.Replace(".", "_"))
                    .Replace("[[templateName]]", template.Name);
            template.IncludeJsFilesContent(Page);



            //TODO re-order array using template order using ILookup
            //http://stackoverflow.com/questions/16926821/matching-the-order-of-one-array-to-another-using-linq
            //ILookup<string, FormField> resultLookup = template.Params.ToLookup(x => x.Name);
            //var propkeys = properties.Keys.SelectMany(key => resultLookup[key]).ToList();


            //load from template only
            _LitFieldsTabs.Text = "";
            if (template.Params.Count > 0)
            {
                //load groups as tabs
                var groups = template.Params
                    .GroupBy(t => t.Group)
                    .Select(g => g.First())
                    .ToList();
                foreach (var g in groups)
                {
                    //add tab title foreach group
                    string groupLabel = GetLabel( "AutoLayout." + item.ItemTypeName + "_tab-" + g.Group, g.Group);
                    _LitFieldsTabs.Text += $"<li><a href='#tab-{g.Group}' class='tab-{g.Group}' data-toggle='tab'>{groupLabel}</a></li>";

                    //add tab content foreach group
                    HtmlGenericControl tabContent = new HtmlGenericControl("div");
                    tabContent.ClientIDMode = ClientIDMode.Static;
                    tabContent.ID = "tab-" + g.Group;
                    tabContent.Attributes.Add("class", "tab-pane fade");
                    _FieldsContainer.Controls.Add(tabContent);

                    foreach (var field in template.Params.Where(t => t.Group == g.Group))
                    {
                        string propDefName = field.Name.Split('.')[0];
                        string propName = field.Name.Split('.')[1];

                        ItemPropertiesDefs propDef = obj.PropertiesList
                            .Where(t => t.GetType().Name == propDefName)
                            .FirstOrDefault();

                        //invalid param propdef
                        if (propDef == null)
                            continue;

                        var properties = GetItemPropertiesInfo(propDef);
                        if (properties == null || properties.Count == 0)
                            continue;

                        PropertyInfo property = properties.Where(t => t.Name == propName).FirstOrDefault();
                        //invalid param name
                        if (property == null)
                            continue;

                        object value = property.GetValue(propDef, null);

                        AbstractFieldContainer control = CreateEditorAndContainer(
                            g.Group, field, property.Name, propDefName, item, value, setFieldValues);
                        if (control == null)
                            continue;

                        tabContent.Controls.Add(control);

                    }
                }

            }
            else
            {

                //add tab content foreach group
                HtmlGenericControl tabContent = new HtmlGenericControl("div");
                if (obj.PropertiesList.Count > 0)
                {
                    string groupLabel = "Detail";
                    _LitFieldsTabs.Text += "<li><a href='#tab-autolayout' data-toggle='tab'>" + groupLabel + "</a></li>";

                    tabContent.ClientIDMode = ClientIDMode.Static;
                    tabContent.ID = "tab-autolayout";
                    tabContent.Attributes.Add("class", "tab-pane fade");
                    _FieldsContainer.Controls.Add(tabContent);
                }

                //load through reflection only
                foreach (var propDef in obj.PropertiesList)
                {
                    string propDefName = propDef.GetType().Name;
                    //string propDefName = propDef.MapAttributeValue;

                    var properties = GetItemPropertiesInfo(propDef);
                    if (properties == null || properties.Count == 0)
                        continue;

                    foreach (PropertyInfo property in properties)
                    {
                        object value = property.GetValue(propDef, null);

                        //default field definition from attributes
                        FormField field = (FormField)property.GetCustomAttribute(typeof(FormField));

                        //try to load field definition from template
                        FormField templateField = template.Params
                            .Where(a => a.Name == $"{propDefName}.{property.Name}")
                            .FirstOrDefault();

                        if (templateField != null)
                            field = templateField;

                        if (field == null)
                            continue;

                        AbstractFieldContainer control = CreateEditorAndContainer(
                            "autolayout", field, property.Name, propDefName, item, value, setFieldValues);
                        if (control == null)
                            continue;

                        tabContent.Controls.Add(control);
                    }
                }
            }



		}

        /// <summary>
        /// put form dynamic fields into item 
        /// form(dynamic fields) --> item.PropertiesList
        /// </summary>
        /// <param name="obj"></param>
        protected virtual void StoreDynamicFields(IItem obj)
		{
			IItem item = obj as IItem;
			if (item == null)
				return;

            var template = getItemTemplate(obj);

            _PageComposer.Store(obj);

            foreach (var propDef in obj.PropertiesList)
            {
                string propDefName = propDef.GetType().Name;

                var properties = GetItemPropertiesInfo(propDef);
                foreach (PropertyInfo property in properties)
                {
                    FormField field = (FormField)property.GetCustomAttribute(typeof(FormField));
                    
                    //try to load field definition from template
                    FormField templateField = template.Params
                        .Where(a => a.Name == $"{propDefName}.{property.Name}")
                        .FirstOrDefault();

                    if (templateField != null)
                        field = templateField;

                    ReadEditor(field, property, propDef, item);
                }
            }

			item.UpdatePropertiesStore();
		}

        /// <summary>
        /// create an instance of control linked to the item prop
        /// </summary>
        /// <param name="property">the item property to map in the control</param>
        /// <param name="propDefName">name of property</param>
        /// <param name="item">the current item</param>
        /// <param name="value">the current value of the control</param>
        /// <param name="setFieldValues">if true set the value</param>
        /// <returns>the control to add to the panel</returns>
        protected AbstractFieldContainer CreateEditorAndContainer(
            string group,
            FormField field, 
            string propertyName, 
            string propDefName, 
            IItem item, 
            object value, 
            bool setFieldValues)
		{

            if (field == null)
				return null;

            AbstractFieldContainer container = (AbstractFieldContainer)LoadControl("~/Controls/FieldContainer/FieldContainer.ascx");
            //used for in page linking
            string containerName = "container_" + propDefName + "-" + propertyName;

            Control innerControl = null;
			IUploadControl uploadControl = null;

			switch (field.Type)
			{
				case FormFieldTypeEnum.Combo:
					container.CSSClass = "form-select-wrapper form-select-detail-item";

					var dropDownList = new DropDownList();
					dropDownList.ID = "property_" + propDefName + "-" + propertyName;
					dropDownList.EnableViewState = true;
					dropDownList.Enabled = field.Enabled;
					dropDownList.CssClass = "form-control " + field.CssClass;
                    dropDownList.Attributes.Add("name", containerName);

                    foreach (var option in field.Options)
					{
                        dropDownList.Items.Add(new ListItem(option.Label, option.Value));

						if (setFieldValues && value != null && Convert.ToString(value) == option.Value)
							dropDownList.SelectedValue = option.Value;
					}

					innerControl = dropDownList;
					break;

				case FormFieldTypeEnum.Html:
					container = (AbstractFieldContainer)LoadControl("~/Controls/FieldContainer/TextAreaFieldContainer.ascx");
					container.CSSClass = "spacing-detail";

					var textareaPanel = new Panel();
                    textareaPanel.Attributes.Add("name", containerName);

                    IContentEditorControl editorControl = null;

					if (field.Localized)
					{
						Translation localizedValue = value as Translation;

						foreach (KeyValuePair<string, string> culture in Config.CultureList)
						{
							// TODO: wrap control in usercontrol

							Literal litCulture = new Literal();
							if (!ShowOnlyDefaultCulture)
								litCulture.Text = "<span class='lang-description'>- <i>" + culture.Value + "</i> -</span>";

							textareaPanel.Controls.Add(litCulture);

							Control htmlEditor = LoadControl("~/Controls/ContentEditorControl.ascx");
							htmlEditor.ID = "property_" + propDefName + "-" + propertyName + culture.Value;

                            editorControl = htmlEditor as IContentEditorControl;
							if (editorControl != null)
							{
                                editorControl.Configuration = new ContentEditorProvider.Configuration()
								{
                                    //TODO editortype
									EditorType = ContentEditorProvider.Configuration.EditorTypeEnum.BasicHtml,
									FileButton = false,
									PageBreakButton = false,
									ReadMoreButton = false,
                                    CssClass = $"field-{propDefName}.{propertyName}"
                                };

								if (setFieldValues && localizedValue != null)
								{
									string text = "";
									localizedValue.TryGetValue(culture.Key, out text);
									editorControl.Text = text;
								}
							}

							LabelsProvider.SetLocalizedControlVisibility(ShowOnlyDefaultCulture, culture.Key, htmlEditor);
							textareaPanel.Controls.Add(htmlEditor);
						}
					}
					else
					{
						Control htmlEditor = LoadControl("~/Controls/ContentEditorControl.ascx");
						htmlEditor.ID = "property_" + propDefName + "-" + propertyName;

						editorControl = htmlEditor as IContentEditorControl;
						if (editorControl != null)
						{
							editorControl.Configuration = new ContentEditorProvider.Configuration()
							{
								EditorType = ContentEditorProvider.Configuration.EditorTypeEnum.BasicHtml,
								FileButton = false,
								PageBreakButton = false,
								ReadMoreButton = false,
                                CssClass = $"field-{propDefName}.{propertyName}"
							};

							if (setFieldValues && value != null)
							{
								editorControl.Text = Convert.ToString(value);
							}
						}

						textareaPanel.Controls.Add(htmlEditor);
					}

					innerControl = textareaPanel;
					break;

				case FormFieldTypeEnum.Check:
					container = (AbstractFieldContainer)LoadControl("~/Controls/FieldContainer/CheckboxFieldContainer.ascx");
					container.ControlLargeSize = 3;

					CheckBox checkbox = new CheckBox();
					checkbox.ID = "property_" + propDefName + "-" + propertyName;
					checkbox.EnableViewState = true;
                    checkbox.Enabled = field.Enabled;

                    //removed cssClass because aspnet add a span wrapper
                    //checkbox.CssClass = field.CssClass;
                    //checkbox.Attributes.Add("name", containerName);

                    checkbox.Text = "";
					if (setFieldValues)
						checkbox.Checked = value == null ? false : Convert.ToBoolean(value);

					innerControl = checkbox;
					break;

				case FormFieldTypeEnum.Text:
					if (field.Localized)
					{
						Panel translationContainer = new Panel();
                        translationContainer.Attributes.Add("name", containerName);

                        Translation localizedValue = value as Translation;
						foreach (KeyValuePair<string, string> culture in Config.CultureList)
						{
							AddTransText("property_" + propDefName + "-" + propertyName, translationContainer, ContentEditorConfig, culture, /*TODO max len attr*/200, "form-control");
							if (setFieldValues)
							{
                                TextBox textbox = Utility.Controls.FindControlRecursive<TextBox>(translationContainer,
                                    "property_" + propDefName + "-" + propertyName + culture.Value);

								if (textbox != null && localizedValue != null)
								{
									string text = "";
									localizedValue.TryGetValue(culture.Key, out text);
									textbox.Text = text;
                                    textbox.Enabled = field.Enabled;
                                    textbox.CssClass = $"form-control {field.CssClass} field-{propDefName}_{propertyName}";
                                }
                            }
						}
						innerControl = translationContainer;
					}
					else
					{
						TextBox textbox = new TextBox();
						textbox.ID = "property_" + propDefName + "-" + propertyName;
                        textbox.Enabled = field.Enabled;
                        textbox.CssClass = $"form-control {field.CssClass} field-{propDefName}_{propertyName}";
                        textbox.EnableViewState = true;
                        textbox.Attributes.Add("name", containerName);

                        if (setFieldValues)
							textbox.Text = value == null ? "" : value.ToString();

						innerControl = textbox;
					}
					break;

				case FormFieldTypeEnum.Numeric:
					container.ControlLargeSize = 3;
					TextBox number = new TextBox();
					number.ID = "property_" + propDefName + "-" + propertyName;
                    number.Enabled = field.Enabled;
                    number.CssClass = "form-control " + field.CssClass;
                    number.EnableViewState = true;
                    number.Attributes.Add("name", containerName);

                    if (setFieldValues)
						number.Text = value == null || string.IsNullOrWhiteSpace(Convert.ToString(value)) ? "" : value.ToString();

                    //TODO clientside without validator using attribute validation
                    RegularExpressionValidator revNumber = new RegularExpressionValidator();
                    revNumber.ControlToValidate = number.ClientID;
					revNumber.ValidationExpression = @"\d+";
					revNumber.Display = ValidatorDisplay.Dynamic;
					revNumber.EnableClientScript = true;
                    revNumber.ErrorMessage = GetLabel(
                        "AutoLayout." + item.ItemTypeName + "_" + propDefName + "-NumberValidationErrorMessage",
                        "Please enter numbers only");

					innerControl = number;
					break;

				case FormFieldTypeEnum.Image:
					Control imageUpload = LoadControl("~/Controls/ImageUpload/ImageUploadModern.ascx");
                    imageUpload.ID = "property_" + propDefName + "-" + propertyName;
					imageUpload.EnableViewState = true;
                    //imageUpload.Enabled = attribute.Enabled;
                    //imageUpload.CssClass = "form-control " + attribute.CssClass;

                    uploadControl = imageUpload as IUploadControl;
					if (uploadControl != null)
					{
                        uploadControl.AllowedFileTypes = field.AllowedFileTypes;
                        uploadControl.MaxFileSize = field.MaxFileSize;
                        //field.Folder --> set in readEditor()
						uploadControl.FilePath = value == null ? "" : value.ToString();
                        uploadControl.Name = containerName;

                        uploadControl.FileDeleted += delegate (object sender, EventArgs e)
                        {
                            changeClientTab("tab-" + group, containerName);
                        };

                        ImageFormField imageAttribute = field as ImageFormField;
						if (imageAttribute != null && !string.IsNullOrWhiteSpace(imageAttribute.AllowedFileTypes))
							uploadControl.AllowedFileTypes = imageAttribute.AllowedFileTypes;
					}

					innerControl = imageUpload;
					break;

				case FormFieldTypeEnum.File:
					if (field.Localized)
					{
						Panel translationContainer = new Panel();
						Translation localizedValue = value as Translation;
						foreach (KeyValuePair<string, string> culture in Config.CultureList)
						{
							Control fileUpload = LoadControl("~/Controls/ImageUpload/FileUploadModern.ascx");
							fileUpload.EnableViewState = true;

							uploadControl = fileUpload as IUploadControl;
							if (uploadControl != null)
							{
                                uploadControl.FileDeleted += delegate (object sender, EventArgs e)
                                {
                                    changeClientTab("tab-" + group, containerName);
                                };

                                uploadControl.AllowedFileTypes = field.AllowedFileTypes;
								uploadControl.MaxFileSize = field.MaxFileSize;
                                uploadControl.Name = containerName;

                                //field.Folder --> set in readEditor()
                                FileFormField fileAttribute = field as FileFormField;
								if (fileAttribute != null && !string.IsNullOrWhiteSpace(fileAttribute.AllowedFileTypes))
									uploadControl.AllowedFileTypes = fileAttribute.AllowedFileTypes;
							}

							AddTransControl(fileUpload, "property_" + propDefName + "-" + propertyName, translationContainer, culture, "form-control");

							if (setFieldValues)
							{
								if (localizedValue != null && uploadControl != null)
								{
									string text = "";
									localizedValue.TryGetValue(culture.Key, out text);
									uploadControl.FilePath = text;
								}
							}
						}
						innerControl = translationContainer;
					}
					else
					{
						Control fileUpload = LoadControl("~/Controls/ImageUpload/FileUploadModern.ascx");
						fileUpload.ID = "property_" + propDefName + "-" + propertyName;
						fileUpload.EnableViewState = true;

						uploadControl = fileUpload as IUploadControl;
						if (uploadControl != null)
						{
                            uploadControl.FileDeleted += delegate (object sender, EventArgs e)
                            {
                                changeClientTab("tab-" + group, containerName);
                            };

                            uploadControl.AllowedFileTypes = field.AllowedFileTypes;
                            uploadControl.MaxFileSize = field.MaxFileSize;
                            uploadControl.Name = containerName;

                            FileFormField fileAttribute = field as FileFormField;
							if (fileAttribute != null && !string.IsNullOrWhiteSpace(fileAttribute.AllowedFileTypes))
								uploadControl.AllowedFileTypes = fileAttribute.AllowedFileTypes;

							uploadControl.FilePath = value == null ? "" : value.ToString();
						}

						innerControl = fileUpload;
					}

					break;
				default:
					return null;
			}

			if (innerControl == null)
				return null;

            container.InnerControl = innerControl;

            //add label
            string labelValue = field.LabelValue;
            if (string.IsNullOrEmpty(labelValue))
                labelValue = propertyName;

            container.ID = propDefName + "-" + propertyName + "_container";
            container.Label = GetLabel(
                "AutoLayout." + item.ItemTypeName + "_" + propDefName + "-" + propertyName,
                labelValue);

            container.LabelClass = $"label-{propDefName}_{propertyName}";

            return container;
		}

		// TODO: implement all editor types
        /// <summary>
        /// find the right control in the form, read its value and set to the item property
        /// </summary>
        /// <param name="property"></param>
        /// <param name="item"></param>
		protected void ReadEditor(
            FormField field, 
            PropertyInfo property, 
            ItemPropertiesDefs propDef, 
            IItem item)
		{
            string propDefName = propDef.GetType().Name;
            //string propDefName = propDef.MapAttributeValue;

			if (field == null)
				return;

			switch (field.Type)
			{
				case FormFieldTypeEnum.Html:
					if (field.Localized)
					{
						Translation localizedValue = new Translation();
						foreach (KeyValuePair<string, string> culture in Config.CultureList)
						{
							ITextControl htmlEditor = Utility.Controls.FindControlRecursive<Control>(
                                _FieldsContainer, 
                                "property_" + propDefName + "-" + property.Name + culture.Value) as ITextControl;

                            if (htmlEditor != null)
								localizedValue[culture.Key] = htmlEditor == null ? null : htmlEditor.Text;
						}

						property.SetValue(propDef, localizedValue, null);
					}
					else
					{
						ITextControl htmlEditor = Utility.Controls.FindControlRecursive<Control>(
                            _FieldsContainer, 
                            "property_" + propDefName + "-" + property.Name) as ITextControl;

						if (htmlEditor != null)
							property.SetValue(propDef, htmlEditor == null ? null : htmlEditor.Text, null);
					}
					break;

				case FormFieldTypeEnum.Combo:
					DropDownList dropDownList = Utility.Controls.FindControlRecursive<DropDownList>(
                        _FieldsContainer, 
                        "property_" + propDefName + "-" + property.Name);

					if (dropDownList != null)
						property.SetValue(propDef, dropDownList.SelectedValue, null);
					break;

				case FormFieldTypeEnum.Numeric:
					TextBox number = Utility.Controls.FindControlRecursive<TextBox>(
                        _FieldsContainer, 
                        "property_" + propDefName + "-" + property.Name);
					if (number != null)
					{
						if (string.IsNullOrWhiteSpace(number.Text))
						{
							property.SetValue(propDef, null, null);
						}
						else
						{
							int value = 0;
							if (int.TryParse(number.Text, out value))
							{
								property.SetValue(propDef, value, null);
							}
						}
					}
					break;

				case FormFieldTypeEnum.Check:
					CheckBox checkbox = Utility.Controls.FindControlRecursive<CheckBox>(
                        _FieldsContainer, 
                        "property_" + propDefName + "-" + property.Name);

					if (checkbox != null)
						property.SetValue(propDef, checkbox.Checked, null);
					break;

				case FormFieldTypeEnum.Text:
					if (field.Localized)
					{
						Translation localizedValue = new Translation();
						foreach (KeyValuePair<string, string> culture in Config.CultureList)
						{
							TextBox textbox = Utility.Controls.FindControlRecursive<TextBox>(
                                _FieldsContainer, 
                                "property_" + propDefName + "-" + property.Name + culture.Value);

							localizedValue[culture.Key] = textbox == null ? null : textbox.Text;
						}

						property.SetValue(propDef, localizedValue, null);
					}
					else
					{
						TextBox textbox = Utility.Controls.FindControlRecursive<TextBox>(
                            _FieldsContainer, 
                            "property_" + propDefName + "-" + property.Name);

						if (textbox != null)
							property.SetValue(propDef, textbox.Text, null);
					}
					break;

				case FormFieldTypeEnum.File:
					// TODO: refactor
					if (field.Localized)
					{
						Translation localizedValue = new Translation();
						foreach (KeyValuePair<string, string> culture in Config.CultureList)
						{
							IUploadControl fileUpload = Utility.Controls.FindControlRecursive<Control>(
                                _FieldsContainer, 
                                "property_" + propDefName + "-" + property.Name + culture.Value) as IUploadControl;

							if (fileUpload == null)
								break;

							if (fileUpload.Deleted)
							{
								localizedValue[culture.Key] = null;
								fileUpload.PerformDelete();
							}
							else if (fileUpload.HasChanged)
							{
								string fileName = Regex.Replace(item.Alias + " " + property.Name, "[^a-zA-Z0-9]", "-") + "." + fileUpload.GetExtension();
								string newFileName;
                                string basePath = Path.Combine(item.StaticFilesPath, field.Folder);
                                string filePath = FilesHelper.GetUniqueFilename(
                                    basePath, fileName.ToLower(), out newFileName);

								fileUpload.SaveTo(filePath);
								localizedValue[culture.Key] = Path.Combine(basePath, newFileName).Replace("\\", "/");
							}
						}

						property.SetValue(propDef, localizedValue, null);
					}
					else
					{
						IUploadControl fileUpload = Utility.Controls.FindControlRecursive<Control>(
                            _FieldsContainer, 
                            "property_" + propDefName + "-" + property.Name) as IUploadControl;
						if (fileUpload == null)
							break;

						if (fileUpload.Deleted)
						{
							property.SetValue(propDef, null, null);
							fileUpload.PerformDelete();
						}
						else if (fileUpload.HasChanged)
						{
							string fileName = Regex.Replace(item.Alias + " " + property.Name, "[^a-zA-Z0-9]", "-") + "." + fileUpload.GetExtension();
							string newFileName;
                            string basePath = Path.Combine(item.StaticFilesPath, field.Folder);
                            string filePath = FilesHelper.GetUniqueFilename(
                                basePath, fileName.ToLower(), out newFileName);

							fileUpload.SaveTo(filePath);
							property.SetValue(propDef, 
                                Path.Combine(basePath, newFileName).Replace("\\", "/"), 
                                null);
						}
					}
					break;

				case FormFieldTypeEnum.Image:
					IUploadControl imageUpload = Utility.Controls.FindControlRecursive<Control>(
                        _FieldsContainer, 
                        "property_" + propDefName + "-" + property.Name) as IUploadControl;

					if (imageUpload == null)
						break;

					if (imageUpload.Deleted)
					{
						property.SetValue(propDef, null, null);
						imageUpload.PerformDelete();
                    }
                    else if (imageUpload.HasChanged)
					{
                        string fileName = Regex.Replace(item.Alias + " " + property.Name, "[^a-zA-Z0-9]", "-") + "." + imageUpload.GetExtension();
						string newFileName;
                        string basePath = Path.Combine(item.StaticImagesPath, field.Folder);
                        string filePath = FilesHelper.GetUniqueFilename(
                                basePath, fileName.ToLower(), out newFileName);

                        imageUpload.SaveTo(filePath);
						property.SetValue(propDef, 
                            Path.Combine(basePath, newFileName).Replace("\\", "/"), 
                            null);
					}
					break;
			}
		}

        private ItemTemplateType getItemTemplate(IItem item)
        {
            var template = new ItemTemplateType();

            if (this.AllowedItems.ContainsKey(item.ItemTypeName))
            {
                //template set in itemsAdmin instance params
                template = this.AllowedItems[item.ItemTypeName];
            }
            else
            {
                //default template for current itemtype
                template = new ItemTemplateTypeManager().GetByFullName(item.ItemTypeName + "/templates", "default.xml");
            }

            return template;
        }

		#endregion

		#region utility

		protected DateTime GetItemDate(ITextControl control)
		{
			CultureInfo culture = CultureInfo.CreateSpecificCulture(Config.CultureDefault);
			DateTime res;
			DateTime.TryParse(control.Text, culture, DateTimeStyles.None, out res);
			return res.Date;
		}

		protected void SetItemDate(ITextControl control, DateTime value)
		{
			control.Text = "";
			if (value != DateTime.MinValue)
				control.Text = value.ToShortDateString();
		}

		protected void setError(string content)
		{
			_LblErrInsert.Text = _LblErrSee.Text = RenderError(content);
		}

		protected void setSuccess(string content)
		{
			_LblOkInsert.Text = _LblOkSee.Text = RenderSuccess(content);
		}

        protected void changeClientTab(string tabId, string scrollAnchor = "")
        {
            Utility.Script.RegisterStartupScript(_Upd1, "changeTab", $@"changeTab('{tabId}', '{scrollAnchor}');");
        }

        /// <summary>
        /// remove all items from cache
        /// </summary>
        protected static void removeFromCache()
		{
			//new CacheManager<PigeonCms.Item>("PigeonCms.Item").Clear();
		}

		[PigeonCms.UserControlScriptMethod]
		public static void RemoveFromCache()
		{
			removeFromCache();
		}

		[PigeonCms.UserControlScriptMethod]
		public static void Refresh()
		{
			//return ClientScript.GetPostBackEventReference(this, null);
			//__doPostBack('__Page', 'MyCustomArgument')
		}

        //TOCHECK
        public static string CheckAlias(string alias)
        {
            if (string.IsNullOrWhiteSpace(alias))
                return string.Empty;

            string result = Regex.Replace(alias, "<[^>]*(>|$)", string.Empty);
            result = Regex.Replace(result, @"[^a-zA-Z0-9\-_]", "-");
            result = Regex.Replace(result, @"[\-]+", "-");
            return result.ToLower().Trim();
        }

        #endregion

        #region state

        protected DateTime ItemDate
		{
			get { return GetItemDate(_TxtItemDate); }
			set { SetItemDate(_TxtItemDate, value); }
		}

		protected DateTime ValidFrom
		{
			get { return GetItemDate(_TxtValidFrom); }
			set { SetItemDate(_TxtValidFrom, value); }
		}

		protected DateTime ValidTo
		{
			get { return GetItemDate(_TxtValidTo); }
			set { SetItemDate(_TxtValidTo, value); }
		}

        /// <summary>
        /// selected item type
        /// PigeonCms.Item as default value
        /// </summary>
		protected string CurrentItemType
		{
			get
			{
                string res = "";

                if (_HidCurrentItemType != null)
                    res = Request[_HidCurrentItemType.UniqueID];

                if (string.IsNullOrEmpty(res))
                    res = new PigeonCms.Item().ItemTypeName;

                return res;
			}
			set
			{
				if (_HidCurrentItemType == null)
					return;

				_HidCurrentItemType.Value = value;
			}
		}

		protected int CurrentSectionId
		{
			get
			{
				int res = 0;
				if (this.SectionId > 0)
					res = this.SectionId;
				else
					int.TryParse(_DropSectionsFilter.SelectedValue, out res);
				return res;
			}
		}

        #endregion

        //TODO move in ItemsAdminControl
        #region Translated user control

        public void GetTransControl(string panelPrefix, Panel panel, Dictionary<string, string> translations, KeyValuePair<string, string> cultureItem)
		{
			TextBox t1 = new TextBox();
			t1 = (TextBox)panel.FindControl(panelPrefix + cultureItem.Value);
			translations.Add(cultureItem.Key, t1.Text);
		}

		public void SetTransControl(string panelPrefix, Panel panel, Dictionary<string, string> translations, KeyValuePair<string, string> cultureItem)
		{
			string res = "";
			TextBox t1 = new TextBox();
			t1 = (TextBox)panel.FindControl(panelPrefix + cultureItem.Value);
			if (translations != null)
				translations.TryGetValue(cultureItem.Key, out res);
			t1.Text = res;
		}

		public void AddTransControl(Control obj, string panelPrefix, Panel panel, KeyValuePair<string, string> cultureItem, string cssClass)
		{
			obj.ID = panelPrefix + cultureItem.Value;
			if (obj is WebControl)
			{
				WebControl wObj = (WebControl)obj;
				wObj.CssClass = cssClass;
				wObj.ToolTip = cultureItem.Key;
			}

			LabelsProvider.SetLocalizedControlVisibility(this.ShowOnlyDefaultCulture, cultureItem.Key, obj);
			var group = new Panel();
			group.CssClass = "form-group input-group";
			group.Controls.Add(obj);

			Literal lit = new Literal();
			if (!this.ShowOnlyDefaultCulture)
				lit.Text = "<div class=\"input-group-addon\"><span>" + cultureItem.Value.Substring(0, 3) + "</span></div>";
			group.Controls.Add(lit);
			panel.Controls.Add(group);
		}

		#endregion
	}
}

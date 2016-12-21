using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PigeonCms.Core.Helpers;
using System.Web.UI.WebControls;
using System.Web.UI;
using PigeonCms.Controls;
using System.Web;

namespace PigeonCms.Modules
{
	public abstract class AutoLayoutItemsAdminControl : ItemsAdminControl
    {
		const int COL_ALIAS_INDEX = 1;
		const int COL_CATEGORY_INDEX = 3;
		const int COL_ACCESSTYPE_INDEX = 4;
		const int COL_ORDER_ARROWS_INDEX = 6;
		const int COL_UPLOADFILES_INDEX = 7;
		const int COL_UPLOADIMAGES_INDEX = 8;
		const int COL_DELETE_INDEX = 9;


		public string TitleItem = "";


		#region Abstract Usercontrol (implemented in derived class)

		protected abstract DropDownList _DropSectionsFilter { get; }
		protected abstract DropDownList _DropCategoriesFilter { get; }
		protected abstract DropDownList _DropNew { get; }
		protected abstract DropDownList _DropEnabledFilter { get; }

		protected abstract DropDownList _DropCategories { get; }
		protected abstract DropDownList _DropItemTypesFilter { get; }

		protected abstract Panel _PanelInsert { get; }
		protected abstract Panel _PanelDescription { get; }
		protected abstract PlaceHolder _FieldsContainer { get; }

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

		//protected abstract HiddenField _HidCurrentId { get; }
		protected abstract HiddenField _HidCurrentItemType { get; }

		#endregion

		#region Initialize

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			ContentEditorProvider.InitEditor(this, _Upd1, base.ContentEditorConfig);

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

			//restrictions TODO
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


			// Recreate form to handle control events and viewstate
			// TODO: avoid on "list mode" postbacks
			var genericItem = new ItemsProxy().GetByKey(CurrentId, this.CurrentItemType, true, true);
			LoadDynamicFields(genericItem, false);

			ItemsAdminHelper.RegisterCss("FieldContainer/FieldContainer", Page);
			ItemsAdminHelper.RegisterCss("ImageUpload/ImageUpload", Page);

			ItemsAdminHelper.InsertJsIntoPageScriptManager("ImageUpload/ImageUploadModern", Page);
			ItemsAdminHelper.InsertJsIntoPageScriptManager("ImageUpload/FileUploadModern", Page);

			//to allow file upload
			Page.Form.Attributes.Add("enctype", "multipart/form-data");
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			setSuccess("");
			setError("");

			if (this.BaseModule.DirectEditMode)
			{
				if (base.CurrItem.Id == 0)
					throw new ArgumentException();
				if (new ItemsManager<Item, ItemsFilter>(true, true).GetByKey(base.CurrItem.Id).Id == 0)
					throw new ArgumentException();
			}

			if (!Page.IsPostBack)
			{
				loadDropEnabledFilter();
				loadDropSectionsFilter(base.SectionId);
				{
					int secId = -1;
					int.TryParse(DropSectionsFilter.SelectedValue, out secId);
					loadDropCategoriesFilter(secId);
				}
				loadDropsItemTypes();
				loadList();
			}
			else
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


				//reload params on every postback, because cannot manage dinamically fields
				var currentItem = new PigeonCms.Item();
				if (CurrentId > 0)
				{
					currentItem = new ItemsManager<Item, ItemsFilter>(true, true).GetByKey(CurrentId);
					ItemParams1.LoadParams(currentItem);
					ItemFields1.LoadFields(currentItem);
				}
				else
				{
					//manually set ItemType
					try
					{
						currentItem.ItemTypeName = LitItemType.Text;
						ItemParams1.LoadParams(currentItem);
						ItemFields1.LoadFields(currentItem);
					}
					catch { }
				}
			}
			if (this.BaseModule.DirectEditMode)
			{
				DivDropNewContainer.Visible = false;
				BtnNew.Visible = false;
				//BtnCancel.OnClientClick = "closePopup();";

				editRow(base.CurrItem.Id);
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
			int.TryParse(DropSectionsFilter.SelectedValue, out secID);

			loadDropCategoriesFilter(secID);
			loadDropCategories(secID);
			loadList();

			base.LastSelectedSectionId = secID;
		}

		protected void DropCategoriesFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			int catID = 0;
			int.TryParse(DropCategoriesFilter.SelectedValue, out catID);

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
				Utility.SetDropByValue(DropNew, "");
			else
			{
				try { editRow(0); }
				catch (Exception e1)
				{
					setError(e1.Message);
				}
			}
		}

		protected void BtnNew_Click(object sender, EventArgs e)
		{
			try
			{
				if (checkAddNewFilters())
				{
					Utility.SetDropByValue(DropNew, this.ItemType);
					editRow(0);
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
			setError("");
			setSuccess("");
			showInsertPanel(false);
		}

		protected void RepPaging_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Header)
			{
				return;
			}

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
			{
				return;
			}

			var item = (PigeonCms.Item)e.Item.DataItem;


			var LitEnabled = (Literal)e.Item.FindControl("LitEnabled");
			string enabledClass = "";
			if (item.Enabled)
				enabledClass = "checked";
			LitEnabled.Text = "<span class='table-modern--checkbox--square " + enabledClass + "'></span>";


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
				LitCategoryTitle.Text = cat.Title;
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
			if (e.CommandName == "Select")
			{
				try
				{
					editRow(int.Parse(e.CommandArgument.ToString()));
				}
				catch (Exception e1)
				{
					setError(e1.Message);
				}
			}
			if (e.CommandName == "DeleteRow")
			{
				deleteRow(int.Parse(e.CommandArgument.ToString()));
			}
			//Enabled
			if (e.CommandName == "Enabled0")
			{
				setFlag(Convert.ToInt32(e.CommandArgument), false, "enabled");
				loadList();
			}
			if (e.CommandName == "Enabled1")
			{
				setFlag(Convert.ToInt32(e.CommandArgument), true, "enabled");
				loadList();
			}
		}

		#region private methods

		private bool checkForm()
		{
			setError("");
			setSuccess("");
			bool res = true;
			string err = "";

			int catId = 0;
			int.TryParse(DropCategories.SelectedValue, out catId);
			if (catId <= 0)
			{
				res = false;
				err += base.GetLabel("ChooseCategory", "alias in use") + "<br>";
			}

			if (!string.IsNullOrEmpty(TxtAlias.Text))
			{
				var filter = new ItemsFilter();
				var list = new List<PigeonCms.Item>();

				filter.Alias = TxtAlias.Text;
				list = new ItemsManager<Item, ItemsFilter>().GetByFilter(filter, "");
				if (list.Count > 0)
				{
					if (this.CurrentId == 0)
					{
						res = false;
						err += "alias in use<br />";
					}
					else
					{
						if (list[0].Id != this.CurrentId)
						{
							res = false;
							err += "alias in use<br />";
						}
					}
				}
			}
			setError(err);
			return res;
		}

		private bool saveForm()
		{
			bool res = false;
			setError("");
			setSuccess("");

			try
			{
				var o1 = new Item();

				if (CurrentId == 0)
				{
					form2obj(o1);
					o1 = new ItemsManager<Item, ItemsFilter>().Insert(o1);
				}
				else
				{
					o1 = new ItemsManager<Item, ItemsFilter>().GetByKey(CurrentId);  //precarico i campi esistenti e nn gestiti dal form
					form2obj(o1);
					new ItemsManager<Item, ItemsFilter>().Update(o1);
				}
				removeFromCache();

				loadList();
				setSuccess(Utility.GetLabel("RECORD_SAVED_MSG"));
				res = true;
			}
			catch (CustomException e1)
			{
				if (e1.CustomMessage == ItemsManager<Item, ItemsFilter>.MaxItemsException)
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
			LblId.Text = "";
			LblOrderId.Text = "";
			LitSection.Text = "";
			LitItemType.Text = "";
			LblCreated.Text = "";
			LblUpdated.Text = "";
			ChkEnabled.Checked = true;
			TxtAlias.Text = "";
			TxtCssClass.Text = "";
			TxtExtId.Text = "";
			this.ItemDate = DateTime.MinValue;
			this.ValidFrom = DateTime.MinValue;
			this.ValidTo = DateTime.MinValue;
			foreach (KeyValuePair<string, string> item in Config.CultureList)
			{
				TextBox t1 = new TextBox();
				t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
				t1.Text = "";

				var txt2 = new Controls_ContentEditorControl();
				txt2 = Utility.FindControlRecursive<Controls_ContentEditorControl>(this, "TxtDescription" + item.Value);
				txt2.Text = "";
			}
			PermissionsControl1.ClearForm();
			SeoControl1.ClearForm();
		}

		private void form2obj(Item obj)
		{
			obj.Id = CurrentId;
			obj.Enabled = ChkEnabled.Checked;
			obj.TitleTranslations.Clear();
			obj.DescriptionTranslations.Clear();
			obj.CategoryId = int.Parse(DropCategories.SelectedValue);
			obj.Alias = TxtAlias.Text;
			obj.CssClass = TxtCssClass.Text;
			obj.ExtId = TxtExtId.Text;
			obj.ItemDate = this.ItemDate;
			obj.ValidFrom = this.ValidFrom;
			obj.ValidTo = this.ValidTo;

			if (CurrentId == 0)
				obj.ItemTypeName = LitItemType.Text;

			foreach (KeyValuePair<string, string> item in Config.CultureList)
			{
				TextBox t1 = new TextBox();
				t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
				obj.TitleTranslations.Add(item.Key, t1.Text);

				var txt2 = new Controls_ContentEditorControl();
				txt2 = Utility.FindControlRecursive<Controls_ContentEditorControl>(this, "TxtDescription" + item.Value);
				obj.DescriptionTranslations.Add(item.Key, txt2.Text);
			}
			obj.ItemParams = FormBuilder.GetParamsString(obj.ItemType.Params, ItemParams1);
			string fieldsString = FormBuilder.GetParamsString(obj.ItemType.Fields, ItemFields1);
			obj.LoadCustomFieldsFromString(fieldsString);
			PermissionsControl1.Form2obj(obj);
			SeoControl1.Form2obj(obj);
		}

		private void obj2form(Item obj)
		{
			LblId.Text = obj.Id.ToString();
			LblOrderId.Text = obj.Ordering.ToString();
			LblUpdated.Text = obj.DateUpdated.ToString() + " by " + obj.UserUpdated;
			LblCreated.Text = obj.DateInserted.ToString() + " by " + obj.UserInserted;
			ChkEnabled.Checked = obj.Enabled;
			TxtAlias.Text = obj.Alias;
			TxtCssClass.Text = obj.CssClass;
			TxtExtId.Text = obj.ExtId;
			Utility.SetDropByValue(DropCategories, obj.CategoryId.ToString());

			foreach (KeyValuePair<string, string> item in Config.CultureList)
			{
				string sTitleTranslation = "";
				TextBox t1 = new TextBox();
				t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
				obj.TitleTranslations.TryGetValue(item.Key, out sTitleTranslation);
				t1.Text = sTitleTranslation;

				//Set title edit item
				if (string.IsNullOrEmpty(TitleItem))
					TitleItem = sTitleTranslation;

				string sDescriptionTraslation = "";
				var txt2 = new Controls_ContentEditorControl();
				txt2 = Utility.FindControlRecursive<Controls_ContentEditorControl>(this, "TxtDescription" + item.Value);
				obj.DescriptionTranslations.TryGetValue(item.Key, out sDescriptionTraslation);
				txt2.Text = sDescriptionTraslation;
				//string sDescriptionTraslation = "";
				//FCKeditor t2 = new FCKeditor();
				//t2 = (FCKeditor)PanelDescription.FindControl("TxtDescription" + item.Value);
				//obj.DescriptionTranslations.TryGetValue(item.Key, out sDescriptionTraslation);
				//t2.Value = sDescriptionTraslation;
			}
			ItemParams1.ClearParams();
			ItemFields1.ClearParams();

			ItemParams1.LoadParams(obj);
			ItemFields1.LoadFields(obj);
			PermissionsControl1.Obj2form(obj);
			SeoControl1.Obj2form(obj);
			LitSection.Text = obj.Category.Section.Title;
			LitItemType.Text = obj.ItemTypeName;

			this.ItemDate = obj.ItemDate;
			this.ValidFrom = obj.ValidFrom;
			this.ValidTo = obj.ValidTo;
		}

		private void editRow(int recordId)
		{
			var obj = new PigeonCms.Item();
			setSuccess("");
			setError("");

			if (!PgnUserCurrent.IsAuthenticated)
				throw new Exception("user not authenticated");

			clearForm();
			CurrentId = recordId;
			if (CurrentId == 0)
			{
				int sectionId = int.Parse(DropSectionsFilter.SelectedValue);
				loadDropCategories(sectionId);

				obj.ItemTypeName = DropNew.SelectedValue;
				obj.ItemDate = DateTime.Now;
				obj.ValidFrom = DateTime.Now;
				obj.ValidTo = DateTime.MinValue;
				int defaultCategoryId = 0;
				int.TryParse(DropCategoriesFilter.SelectedValue, out defaultCategoryId);
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
				LitItemType.Text = DropNew.SelectedValue;
			}
			else
			{
				obj = new ItemsManager<Item, ItemsFilter>(true, true).GetByKey(CurrentId);
				loadDropCategories(obj.SectionId);
				obj2form(obj);
			}
			Utility.SetDropByValue(DropNew, "");
			showInsertPanel(true);
		}

		private void deleteRow(int recordId)
		{
			setSuccess("");
			setError("");

			try
			{
				if (!PgnUserCurrent.IsAuthenticated)
					throw new Exception("user not authenticated");

				new ItemsManager<Item, ItemsFilter>(true, true).DeleteById(recordId);
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
			var man = new ItemsManager<Item, ItemsFilter>(true, true);
			var filter = new ItemsFilter();

			filter.Enabled = Utility.TristateBool.NotSet;
			switch (DropEnabledFilter.SelectedValue)
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

			if (DropItemTypesFilter.SelectedValue != "")
				filter.ItemType = DropItemTypesFilter.SelectedValue;
			if (this.ItemId > 0)
				filter.Id = this.ItemId;

			int secId = -1;
			int.TryParse(DropSectionsFilter.SelectedValue, out secId);

			int catId = -1;
			int.TryParse(DropCategoriesFilter.SelectedValue, out catId);

			filter.SectionId = secId;
			if (base.SectionId > 0)
				filter.SectionId = base.SectionId;

			filter.CategoryId = catId;
			if (base.CategoryId > 0)
				filter.CategoryId = base.CategoryId;

			var list = man.GetByFilter(filter, "");
			var ds = new PagedDataSource();
			ds.DataSource = list;
			ds.AllowPaging = true;
			ds.PageSize = base.ListPageSize;
			ds.CurrentPageIndex = base.ListCurrentPage;

			RepPaging.Visible = false;
			if (ds.PageCount > 1)
			{
				RepPaging.Visible = true;
				ArrayList pages = new ArrayList();
				for (int i = 0; i <= ds.PageCount - 1; i++)
				{
					pages.Add((i + 1).ToString());
				}
				RepPaging.DataSource = pages;
				RepPaging.DataBind();
			}

			Rep1.DataSource = ds;
			Rep1.DataBind();
		}

		private void loadDropEnabledFilter()
		{
			DropEnabledFilter.Items.Clear();
			DropEnabledFilter.Items.Add(new ListItem(Utility.GetLabel("LblSelectState", "Select state"), ""));
			DropEnabledFilter.Items.Add(new ListItem("On-line", "1"));
			DropEnabledFilter.Items.Add(new ListItem("Off-line", "0"));
		}

		private void loadDropSectionsFilter(int sectionId)
		{
			DropSectionsFilter.Items.Clear();

			var secFilter = new SectionsFilter();
			secFilter.Id = sectionId;
			var secList = new SectionsManager(true, true).GetByFilter(secFilter, "");

			foreach (var sec in secList)
			{
				DropSectionsFilter.Items.Add(new ListItem(sec.Title, sec.Id.ToString()));
			}
			if (base.LastSelectedSectionId > 0)
				Utility.SetDropByValue(DropSectionsFilter, base.LastSelectedSectionId.ToString());
		}

		private void loadDropCategoriesFilter(int sectionId)
		{
			DropCategoriesFilter.Items.Clear();
			DropCategoriesFilter.Items.Add(new ListItem(Utility.GetLabel("LblSelectCategory", "Select category"), "0"));

			var catFilter = new CategoriesFilter();
			catFilter.SectionId = sectionId;
			catFilter.Id = base.CategoryId;
			if (catFilter.SectionId == 0)
				catFilter.Id = -1;

			var list = new CategoriesManager(true, true).GetByFilter(catFilter, "");
			loadListCategories(DropCategoriesFilter, list, 0, 0, base.ShowItemsCount);

			if (base.LastSelectedCategoryId > 0)
				Utility.SetDropByValue(DropCategoriesFilter, base.LastSelectedCategoryId.ToString());

			if (base.CategoryId > 0)
				Utility.SetDropByValue(DropCategoriesFilter, base.CategoryId.ToString());
		}

		private void loadDropCategories(int sectionId)
		{
			DropCategories.Items.Clear();

			var catFilter = new CategoriesFilter();
			catFilter.SectionId = sectionId;
			catFilter.Id = base.CategoryId;
			if (catFilter.SectionId == 0)
				catFilter.Id = -1;
			var list = new CategoriesManager().GetByFilter(catFilter, "");
			loadListCategories(DropCategories, list, 0, 0);
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

			DropNew.Items.Clear();
			DropNew.Items.Add(new ListItem(Utility.GetLabel("LblCreateNew", "Create new"), ""));

			DropItemTypesFilter.Items.Clear();
			DropItemTypesFilter.Items.Add(new ListItem(Utility.GetLabel("LblSelectItem", "Select item"), ""));

			var filter = new ItemTypeFilter();
			if (!string.IsNullOrEmpty(this.ItemType))
				filter.FullName = this.ItemType;
			List<ItemType> recordList = new ItemTypeManager().GetByFilter(filter, "FullName");
			foreach (ItemType record1 in recordList)
			{
				DropNew.Items.Add(
					new ListItem(record1.FullName, record1.FullName));

				DropItemTypesFilter.Items.Add(
					new ListItem(record1.FullName, record1.FullName));
			}

			if (!string.IsNullOrEmpty(this.ItemType))
			{
				BtnNew.Visible = true;
				DivDropNewContainer.Visible = false;
				DropItemTypesFilter.Visible = false;
			}
			else
			{
				BtnNew.Visible = false;
				DivDropNewContainer.Visible = true;
				DropItemTypesFilter.Visible = true;
			}
		}

		private void setFlag(int recordId, bool value, string flagName)
		{
			try
			{
				if (!PgnUserCurrent.IsAuthenticated)
					throw new Exception("user not authenticated");

				var o1 = new ItemsManager<Item, ItemsFilter>().GetByKey(recordId);
				switch (flagName.ToLower())
				{
					case "enabled":
						o1.Enabled = value;
						break;
					default:
						break;
				}
				new ItemsManager<Item, ItemsFilter>().Update(o1);
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
			//<input type="hidden" name="RowId" value='<%# Eval("Id") %>' />
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
			var man = new ItemsManager<Item, ItemsFilter>(true, false);
			var item = man.GetByKey(id);
			item.Ordering = ordering;
			man.Update(item);
		}

		private bool checkAddNewFilters()
		{
			bool res = true;
			if (DropSectionsFilter.SelectedValue == "0" || DropSectionsFilter.SelectedValue == "")
			{
				setError(base.GetLabel("ChooseSection", "Choose a section before"));
				res = false;
			}

			return res;
		}

		/// function for display insert panel
		/// <summary>
		/// </summary>
		private void showInsertPanel(bool toShow)
		{

			PigeonCms.Utility.Script.RegisterStartupScript(Upd1, "bodyBlocked", "bodyBlocked(" + toShow.ToString().ToLower() + ");");

			if (toShow)
				PanelInsert.Visible = true;
			else
				PanelInsert.Visible = false;
		}
		#endregion

		#region Dynamic properties management

		protected PropertyInfo[] GetItemPropertiesInfo(Item obj)
		{
			BaseItem item = obj as BaseItem;
			if (item == null)
				return null;

			AbstractPropertiesDefs props = item.Properties;
			Type type = props.GetType();

			return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
		}

		protected void LoadDynamicFields(Item obj, bool setFieldValues = true)
		{
			BaseItem item = obj as BaseItem;
			if (item == null)
				return;

			PropertyInfo[] properties = GetItemPropertiesInfo(obj);

			_FieldsContainer.Controls.Clear();

			foreach (PropertyInfo property in properties)
			{
				object value = property.GetValue(item.Properties, null);

				AbstractFieldContainer control = CreateEditorAndContainer(property, item, value, setFieldValues);
				if (control == null)
					continue;

				control.ID = property.Name + "_container";
				control.Label = GetLabel("AQItemsAdmin." + item.ItemType.Name + "_" + property.Name, property.Name);

				_FieldsContainer.Controls.Add(control);
			}
		}

		protected virtual void StoreDynamicFields(Item obj)
		{
			BaseItem item = obj as BaseItem;
			if (item == null)
				return;

			PropertyInfo[] properties = GetItemPropertiesInfo(obj);

			foreach (PropertyInfo property in properties)
			{
				ReadEditor(property, item);
			}

			item.UpdatePropertiesStore();
		}

		// TODO: implement all editor types
		protected AbstractFieldContainer CreateEditorAndContainer(PropertyInfo property, BaseItem item, object value, bool setFieldValues)
		{
			ItemFieldAttribute attribute = (ItemFieldAttribute)property.GetCustomAttribute(typeof(ItemFieldAttribute));
			if (attribute == null)
				return null;

			AbstractFieldContainer container = (AbstractFieldContainer)LoadControl("~/Controls/FieldContainer/FieldContainer.ascx");
			Control innerControl = null;
			IUploadControl uploadControl = null;

			switch (attribute.EditorType)
			{
				case ItemFieldEditorType.Select:
					container.CSSClass = "form-select-wrapper form-select-detail-item";

					DropDownList dropDownList = new DropDownList();
					dropDownList.ID = "property_" + property.Name;
					dropDownList.EnableViewState = true;
					dropDownList.Enabled = true;
					dropDownList.CssClass = "form-control";
					foreach (string dropDownItemValue in attribute.Values)
					{
						dropDownList.Items.Add(new ListItem(GetLabel("AQItemsAdmin." + item.ItemType.Name + "_" + property.Name + ".Option_" + dropDownItemValue, dropDownItemValue), dropDownItemValue));
						if (setFieldValues && value != null && Convert.ToString(value) == dropDownItemValue)
							dropDownList.SelectedValue = dropDownItemValue;
					}

					innerControl = dropDownList;
					break;

				case ItemFieldEditorType.Html:
					container = (AbstractFieldContainer)LoadControl("~/Controls/FieldContainer/TextAreaFieldContainer.ascx");
					container.CSSClass = "spacing-detail";

					Panel textareaPanel = new Panel();
					IEditorControl editorControl = null;

					if (attribute.Localized)
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
							htmlEditor.ID = "property_" + property.Name + culture.Value;

							editorControl = htmlEditor as IEditorControl;
							if (editorControl != null)
							{
								editorControl.Configuration = new ContentEditorProvider.Configuration()
								{
									EditorType = ContentEditorProvider.Configuration.EditorTypeEnum.BasicHtml,
									FileButton = false,
									PageBreakButton = false,
									ReadMoreButton = false
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
						htmlEditor.ID = "property_" + property.Name;

						editorControl = htmlEditor as IEditorControl;
						if (editorControl != null)
						{
							editorControl.Configuration = new ContentEditorProvider.Configuration()
							{
								EditorType = ContentEditorProvider.Configuration.EditorTypeEnum.BasicHtml,
								FileButton = false,
								PageBreakButton = false,
								ReadMoreButton = false
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
				case ItemFieldEditorType.Flag:
					container = (AbstractFieldContainer)LoadControl("~/Controls/FieldContainer/CheckboxFieldContainer.ascx");
					container.ControlLargeSize = 3;

					CheckBox checkbox = new CheckBox();
					checkbox.ID = "property_" + property.Name;
					checkbox.EnableViewState = true;
					checkbox.Enabled = true;
					checkbox.Text = "";
					if (setFieldValues)
						checkbox.Checked = value == null ? false : Convert.ToBoolean(value);

					innerControl = checkbox;
					break;
				case ItemFieldEditorType.TextBox:
					if (attribute.Localized)
					{
						Panel translationContainer = new Panel();
						Translation localizedValue = value as Translation;
						foreach (KeyValuePair<string, string> culture in Config.CultureList)
						{
							AddTransText("property_" + property.Name, translationContainer, ContentEditorConfig, culture, 200, "form-control");
							if (setFieldValues)
							{
								TextBox textbox = Utility.FindControlRecursive<TextBox>(translationContainer, "property_" + property.Name + culture.Value);
								if (textbox != null && localizedValue != null)
								{
									string text = "";
									localizedValue.TryGetValue(culture.Key, out text);
									textbox.Text = text;
								}
							}
						}

						innerControl = translationContainer;
					}
					else
					{
						TextBox textbox = new TextBox();
						textbox.ID = "property_" + property.Name;
						textbox.CssClass = "form-control";
						textbox.EnableViewState = true;
						if (setFieldValues)
							textbox.Text = value == null ? "" : value.ToString();

						innerControl = textbox;
					}
					break;
				case ItemFieldEditorType.Number:
					container.ControlLargeSize = 3;
					TextBox number = new TextBox();
					number.ID = "property_" + property.Name;
					number.CssClass = "form-control";
					number.EnableViewState = true;
					if (setFieldValues)
						number.Text = value == null || string.IsNullOrWhiteSpace(Convert.ToString(value)) ? "" : value.ToString();

					RegularExpressionValidator revNumber = new RegularExpressionValidator();
					revNumber.ControlToValidate = number.ClientID;
					revNumber.ValidationExpression = @"\d+";
					revNumber.Display = ValidatorDisplay.Dynamic;
					revNumber.EnableClientScript = true;
					revNumber.ErrorMessage = GetLabel("AQItemsAdmin.NumberValidationErrorMessage", "Please enter numbers only");

					innerControl = number;
					break;
				case ItemFieldEditorType.Image:
					Control imageUpload = LoadControl("~/Controls/ImageUpload/ImageUploadModern.ascx");
					imageUpload.ID = "property_" + property.Name;
					imageUpload.EnableViewState = true;

					uploadControl = imageUpload as IUploadControl;
					if (uploadControl != null)
					{
						uploadControl.AllowedFileTypes = GetStringParam("ItemImagesTypes", "jpg,png");
						uploadControl.MaxFileSize = GetIntParam("ItemImagesMaxFileSize", 1024);
						uploadControl.FilePath = value == null ? "" : value.ToString();

						ImageFieldAttribute imageAttribute = attribute as ImageFieldAttribute;
						if (imageAttribute != null && !string.IsNullOrWhiteSpace(imageAttribute.AllowedFileTypes))
							uploadControl.AllowedFileTypes = imageAttribute.AllowedFileTypes;
					}

					innerControl = imageUpload;
					break;
				case ItemFieldEditorType.File:
					if (attribute.Localized)
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
								uploadControl.AllowedFileTypes = GetStringParam("ItemFilesTypes", "*");
								uploadControl.MaxFileSize = GetIntParam("ItemFilesMaxFileSize", 10240);

								FileFieldAttribute fileAttribute = attribute as FileFieldAttribute;
								if (fileAttribute != null && !string.IsNullOrWhiteSpace(fileAttribute.AllowedFileTypes))
									uploadControl.AllowedFileTypes = fileAttribute.AllowedFileTypes;
							}

							AddTransControl(fileUpload, "property_" + property.Name, translationContainer, culture, "form-control");

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
						fileUpload.ID = "property_" + property.Name;
						fileUpload.EnableViewState = true;

						uploadControl = fileUpload as IUploadControl;
						if (uploadControl != null)
						{
							uploadControl.AllowedFileTypes = GetStringParam("ItemFilesTypes", "*");
							uploadControl.MaxFileSize = GetIntParam("ItemFilesMaxFileSize", 10240);

							FileFieldAttribute fileAttribute = (FileFieldAttribute)attribute;
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
			return container;
		}

		// TODO: implement all editor types
		protected void ReadEditor(PropertyInfo property, BaseItem item)
		{
			ItemFieldAttribute attribute = (ItemFieldAttribute)property.GetCustomAttribute(typeof(ItemFieldAttribute));
			if (attribute == null)
				return;

			switch (attribute.EditorType)
			{
				case ItemFieldEditorType.Html:
					if (attribute.Localized)
					{
						Translation localizedValue = new Translation();
						foreach (KeyValuePair<string, string> culture in Config.CultureList)
						{
							ITextControl htmlEditor = Utility.FindControlRecursive<Control>(_FieldsContainer, "property_" + property.Name + culture.Value) as ITextControl;
							if (htmlEditor != null)
								localizedValue[culture.Key] = htmlEditor == null ? null : htmlEditor.Text;
						}

						property.SetValue(item.Properties, localizedValue, null);
					}
					else
					{
						ITextControl htmlEditor = Utility.FindControlRecursive<Control>(_FieldsContainer, "property_" + property.Name) as ITextControl;
						if (htmlEditor != null)
							property.SetValue(item.Properties, htmlEditor == null ? null : htmlEditor.Text, null);
					}
					break;
				case ItemFieldEditorType.Select:
					DropDownList dropDownList = Utility.FindControlRecursive<DropDownList>(_FieldsContainer, "property_" + property.Name);
					if (dropDownList != null)
						property.SetValue(item.Properties, dropDownList.SelectedValue, null);
					break;
				case ItemFieldEditorType.Number:
					TextBox number = Utility.FindControlRecursive<TextBox>(_FieldsContainer, "property_" + property.Name);
					if (number != null)
					{
						if (string.IsNullOrWhiteSpace(number.Text))
						{
							property.SetValue(item.Properties, null, null);
						}
						else
						{
							int value = 0;
							if (int.TryParse(number.Text, out value))
							{
								property.SetValue(item.Properties, value, null);
							}
						}
					}
					break;
				case ItemFieldEditorType.Flag:
					CheckBox checkbox = Utility.FindControlRecursive<CheckBox>(_FieldsContainer, "property_" + property.Name);
					if (checkbox != null)
						property.SetValue(item.Properties, checkbox.Checked, null);
					break;
				case ItemFieldEditorType.TextBox:
					if (attribute.Localized)
					{
						Translation localizedValue = new Translation();
						foreach (KeyValuePair<string, string> culture in Config.CultureList)
						{
							TextBox textbox = Utility.FindControlRecursive<TextBox>(_FieldsContainer, "property_" + property.Name + culture.Value);
							localizedValue[culture.Key] = textbox == null ? null : textbox.Text;
						}

						property.SetValue(item.Properties, localizedValue, null);
					}
					else
					{
						TextBox textbox = Utility.FindControlRecursive<TextBox>(_FieldsContainer, "property_" + property.Name);
						if (textbox != null)
							property.SetValue(item.Properties, textbox.Text, null);
					}
					break;
				case ItemFieldEditorType.File:
					// TODO: refactor
					if (attribute.Localized)
					{
						Translation localizedValue = new Translation();
						foreach (KeyValuePair<string, string> culture in Config.CultureList)
						{
							IUploadControl fileUpload = Utility.FindControlRecursive<Control>(_FieldsContainer, "property_" + property.Name + culture.Value) as IUploadControl;
							if (fileUpload == null)
								break;

							if (fileUpload.Deleted)
							{
								localizedValue[culture.Key] = null;
								fileUpload.PerformDelete();
							}
							else if (fileUpload.HasChanged)
							{
								string fileName = Regex.Replace(GetAlias(item) + " " + property.Name, "[^a-zA-Z0-9]", "-") + "." + fileUpload.GetExtension();
								string newFileName;
								string filePath = FilesManipulationUtils.GetUniqueFilename(item.StaticFilesPath, fileName.ToLower(), out newFileName);
								fileUpload.SaveTo(filePath);
								localizedValue[culture.Key] = item.StaticFilesPath + newFileName;
							}
						}

						property.SetValue(item.Properties, localizedValue, null);
					}
					else
					{
						IUploadControl fileUpload = Utility.FindControlRecursive<Control>(_FieldsContainer, "property_" + property.Name) as IUploadControl;
						if (fileUpload == null)
							break;

						if (fileUpload.Deleted)
						{
							property.SetValue(item.Properties, null, null);
							fileUpload.PerformDelete();
						}
						else if (fileUpload.HasChanged)
						{
							string fileName = Regex.Replace(GetAlias(item) + " " + property.Name, "[^a-zA-Z0-9]", "-") + "." + fileUpload.GetExtension();
							string newFileName;
							string filePath = FilesManipulationUtils.GetUniqueFilename(item.StaticFilesPath, fileName.ToLower(), out newFileName);
							fileUpload.SaveTo(filePath);
							property.SetValue(item.Properties, item.StaticFilesPath + newFileName, null);
						}
					}
					break;
				case ItemFieldEditorType.Image:
					IUploadControl imageUpload = Utility.FindControlRecursive<Control>(_FieldsContainer, "property_" + property.Name) as IUploadControl;
					if (imageUpload == null)
						break;

					if (imageUpload.Deleted)
					{
						property.SetValue(item.Properties, null, null);
						imageUpload.PerformDelete();
					}
					else if (imageUpload.HasChanged)
					{
						string fileName = Regex.Replace(GetAlias(item) + " " + property.Name, "[^a-zA-Z0-9]", "-") + "." + imageUpload.GetExtension();
						string newFileName;
						string filePath = FilesManipulationUtils.GetUniqueFilename(item.StaticImagesPath, fileName.ToLower(), out newFileName);
						imageUpload.SaveTo(filePath);
						property.SetValue(item.Properties, item.StaticImagesPath + newFileName, null);
					}
					break;
			}
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

		/// <summary>
		/// remove all items from cache
		/// </summary>
		protected static void removeFromCache()
		{
			new CacheManager<PigeonCms.Item>("PigeonCms.Item").Clear();
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

		#endregion

		#region state

		protected string itemTypes;
		public string ItemTypes
		{
			get { return GetStringParam("ItemTypes", itemTypes); }
			set { itemTypes = value; }
		}

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

		protected string CurrentItemType
		{
			get
			{
				if (_HidCurrentItemType == null)
					return string.Empty;

				return Request[_HidCurrentItemType.UniqueID];
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
					int.TryParse(DropSectionsFilter.SelectedValue, out res);
				return res;
			}
		}

		#endregion	

	}
}

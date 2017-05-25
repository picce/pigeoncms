using System;
using System.Data;
using System.Linq;
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
using System.Globalization;
using PigeonCms;
using PigeonCms.Core.Helpers;
using PigeonCms.Controls;


public partial class Controls_AutoLayout : PigeonCms.Modules.AutoLayoutItemsAdminControl
{
	protected override DropDownList _DropSectionsFilter { get { return DropSectionsFilter; } }
	protected override DropDownList _DropCategoriesFilter { get { return DropCategoriesFilter; } }
	protected override DropDownList _DropNew { get { return DropNew; } }
	protected override DropDownList _DropEnabledFilter { get { return DropEnabledFilter; } }

	protected override DropDownList _DropCategories { get { return DropCategories; } }
	protected override DropDownList _DropItemTypesFilter { get { return DropItemTypesFilter; } }

	protected override Panel _PanelInsert { get { return PanelInsert; } }
	protected override Panel _PanelDescription { get { return PanelDescription; } }
    protected override Literal _LitFieldsTabs { get { return LitFieldsTabs; } }
    protected override PlaceHolder _FieldsContainer { get { return PlhItemFieldsContainer; } }

    protected override Literal _LitTemplateResources { get { return LitTemplateResources; } }


    protected override Repeater _Rep1 { get { return Rep1; } }
	protected override Repeater _RepPaging { get { return RepPaging; } }

	protected override UpdatePanel _Upd1 { get { return Upd1; } }

	protected override ITextControl _LblErrInsert { get { return LblErrInsert; } }
	protected override ITextControl _LblErrSee { get { return LblErrSee; } }
	protected override ITextControl _LblOkInsert { get { return LblOkInsert; } }
	protected override ITextControl _LblOkSee { get { return LblOkSee; } }
	protected override ITextControl _LitItemType { get { return LitItemType; } }

	protected override Button _BtnNew { get { return BtnNew; } }
	protected override Button _BtnSave { get { return BtnSave; } }
	protected override Button _BtnCancel { get { return BtnCancel; } }

	protected override ITextControl _LblId { get { return LblId; } }
	protected override ITextControl _LblOrderId { get { return LblOrderId; } }
	protected override ITextControl _LblUpdated { get { return LblUpdated; } }
	protected override ITextControl _LblCreated { get { return LblCreated; } }
	protected override ITextControl _LitSection { get { return LitSection; } }
	protected override CheckBox _ChkEnabled { get { return ChkEnabled; } }
	protected override TextBox _TxtAlias { get { return TxtAlias; } }
	protected override ITextControl _TxtCssClass { get { return TxtCssClass; } }
	protected override ITextControl _TxtExtId { get { return TxtExtId; } }
	protected override Panel _PanelTitle { get { return PanelTitle; } }

	protected override TextBox _TxtItemDate { get { return TxtItemDate; } }
	protected override TextBox _TxtValidFrom { get { return TxtValidFrom; } }
	protected override TextBox _TxtValidTo { get { return TxtValidTo; } }

	protected override PigeonCms.Controls.PermissionsControl _PermissionsControl { get { return PermissionsControl1; } }
	protected override PigeonCms.Controls.SeoControl _SeoControl { get { return SeoControl1; } }

	protected override PigeonCms.Controls.ItemParamsControl _ItemParams { get { return ItemParams1; } }
	protected override PigeonCms.Controls.ItemParamsControl _ItemFields { get { return ItemFields1; } }

	//protected override HiddenField _HidCurrentId { get { return HidCurrentId; } }
	protected override HiddenField _HidCurrentItemType { get { return HidCurrentItemType; } }
    protected override IPageComposer _PageComposer { get { return PageComposer; } }
}

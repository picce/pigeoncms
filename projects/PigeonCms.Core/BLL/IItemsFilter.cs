using System;
namespace PigeonCms
{
	public interface IItemsFilter : ITableExternalId
	{
		string AccessCode { get; set; }
		int AccessLevel { get; set; }
		string Alias { get; set; }
		int CategoryId { get; set; }
		PigeonCms.Utility.TristateBool CustomBool1 { get; set; }
		PigeonCms.Utility.TristateBool CustomBool2 { get; set; }
		PigeonCms.Utility.TristateBool CustomBool3 { get; set; }
		PigeonCms.Utility.TristateBool CustomBool4 { get; set; }
		int CustomInt1 { get; set; }
		int CustomInt2 { get; set; }
		int CustomInt3 { get; set; }
		int CustomInt4 { get; set; }
		string CustomString1 { get; set; }
		string CustomString2 { get; set; }
		string CustomString3 { get; set; }
		string CustomString4 { get; set; }
		string DescriptionSearch { get; set; }
		PigeonCms.Utility.TristateBool Enabled { get; set; }
		string FullSearch { get; set; }
		int Id { get; set; }
		PigeonCms.Utility.TristateBool IsValidItem { get; set; }
		PigeonCms.DatesRange ItemDateRange { get; set; }
		string ItemType { get; set; }
		int NumOfRecords { get; set; }
		int SectionId { get; set; }
		bool ShowOnlyRootItems { get; set; }
		System.Collections.Generic.List<int> TagsId { get; set; }
		System.Collections.Generic.List<string> TagsTitle { get; set; }
		int ThreadId { get; set; }
		string TitleSearch { get; set; }
		string UserInserted { get; set; }
		string WriteAccessCode { get; set; }
		int WriteAccessLevel { get; set; }
	}
}

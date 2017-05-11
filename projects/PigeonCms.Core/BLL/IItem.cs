/***************************************************
PigeonCms - Open source Content Management System 
https://github.com/picce/pigeoncms
Copyright © 2017 Nicola Ridolfi - picce@yahoo.it
version: 2.0.0
Licensed under the terms of "GNU General Public License v3"
For the full license text see license.txt or
visit "http://www.gnu.org/licenses/gpl.html"
***************************************************/

using System;
using System.Collections.Generic;

namespace PigeonCms
{
	public interface IItem: ITableWithPermissions,
		ITableWithOrdering, ITableWithComments,
		ITableExternalId, ITableWithSeo
	{
		List<ItemPropertiesDefs> PropertiesList { get; set; }
        ItemsManager MyManager(bool checkUserContext, bool writeMode);
		string Alias { get; set; }
		int CategoryId { get; set; }
		int CompareTo(global::PigeonCms.Item obj, string field);
		string CssClass { get; set; }
		bool CustomBool1 { get; set; }
		bool CustomBool2 { get; set; }
		bool CustomBool3 { get; set; }
		bool CustomBool4 { get; set; }
		DateTime CustomDate1 { get; set; }
		DateTime CustomDate2 { get; set; }
		DateTime CustomDate3 { get; set; }
		DateTime CustomDate4 { get; set; }
		decimal CustomDecimal1 { get; set; }
		decimal CustomDecimal2 { get; set; }
		decimal CustomDecimal3 { get; set; }
		decimal CustomDecimal4 { get; set; }
		int CustomInt1 { get; set; }
		int CustomInt2 { get; set; }
		int CustomInt3 { get; set; }
		int CustomInt4 { get; set; }
		string CustomString1 { get; set; }
		string CustomString2 { get; set; }
		string CustomString3 { get; set; }
		string CustomString4 { get; set; }
		DateTime DateInserted { get; set; }
		DateTime DateUpdated { get; set; }
		global::PigeonCms.FileMetaInfo DefaultImage { get; set; }
		string DefaultImageName { get; set; }
		void DeleteFiles();
		void DeleteImages();
		string Description { get; }
		global::System.Collections.Generic.List<string> DescriptionPages { get; }
		string DescriptionParsed { get; }
		global::System.Collections.Generic.Dictionary<string, string> DescriptionTranslations { get; set; }
		bool Enabled { get; set; }
		global::System.Collections.Generic.List<global::PigeonCms.FileMetaInfo> Files { get; }
		string FilesPath { get; }
		long FilesSize { get; }
		global::System.Collections.Generic.Dictionary<int, string> FormFields { get; set; }
		int Id { get; set; }
		global::System.Collections.Generic.List<global::PigeonCms.FileMetaInfo> Images { get; }
		global::System.Collections.Generic.List<global::PigeonCms.FileMetaInfo> ImagesNotDefault { get; }
		string ImagesPath { get; }
		long ImagesSize { get; }
		bool IsDescriptionTranslated { get; }
		bool IsThreadRoot { get; }
		bool IsTitleTranslated { get; }
		DateTime ItemDate { get; set; }
		string ItemParams { get; set; }
		global::PigeonCms.ItemType ItemType { get; }
		string ItemTypeName { get; set; }
        string FilterTypeName { get; }
        string ManagerTypeName { get; }

		void LoadCustomFieldsFromString(string fieldsString);
		int NumOfImagesLoaded { get; }
		global::System.Collections.Generic.Dictionary<string, string> Params { get; }
		int SectionId { get; set; }
		int ThreadId { get; set; }
		global::System.Collections.Generic.List<global::PigeonCms.Item> ThreadList { get; }
		global::PigeonCms.Item ThreadRoot { get; }
		string Title { get; }
		global::System.Collections.Generic.Dictionary<string, string> TitleTranslations { get; set; }
		string UserInserted { get; set; }
		string UserUpdated { get; set; }
		DateTime ValidFrom { get; set; }
		DateTime ValidTo { get; set; }
        string StaticImagesPath { get; }
        string StaticFilesPath { get; }
        List<FileMetaInfo> StaticImages { get; }
        void UpdatePropertiesStore();

    }
}

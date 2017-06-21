using System;
namespace PigeonCms.Core.Helpers
{
	interface ITableManager<T, F, Kkey>
	 where T : PigeonCms.ITable
	{
		int DeleteById(Kkey recordId);
		System.Collections.Generic.List<T> GetByFilter(F filter, string sort);
		T GetByKey(Kkey id);
		System.Collections.Generic.Dictionary<string, string> GetList();
		T Insert(T newObj);
		string KeyFieldName { get; set; }
		string TableName { get; set; }
		int Update(T theObj);
	}
}

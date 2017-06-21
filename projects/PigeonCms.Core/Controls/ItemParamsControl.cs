using PigeonCms;


namespace PigeonCms.Controls
{
	/// <summary>
	/// abstract class pigeon\controls\ItemParams.ascx
	/// </summary>
	public abstract class ItemParamsControl : BaseModuleControl
	{
		public abstract string Title { get; set; }
		public abstract void ClearParams();
		public abstract void LoadParams(IItem obj);
		public abstract void LoadFields(IItem obj);
	}
}

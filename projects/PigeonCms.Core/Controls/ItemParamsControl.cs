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
		public abstract void LoadParams(Item obj);
		public abstract void LoadFields(Item obj);
	}
}

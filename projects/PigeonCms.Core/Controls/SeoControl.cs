using PigeonCms;


namespace PigeonCms.Controls
{
	/// <summary>
	/// abstract class pigeon\controls\SeoControl.ascx
	/// </summary>
	public abstract class SeoControl : BaseModuleControl
	{
		public abstract void Obj2form(ITableWithSeo obj);
		public abstract void Form2obj(ITableWithSeo obj);
		public abstract void ClearForm();
	}
}

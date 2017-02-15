using PigeonCms;


namespace PigeonCms.Controls
{
	/// <summary>
	/// abstract class pigeon\controls\PermissionControl.ascx
	/// </summary>
	public abstract class PermissionsControl : BaseModuleControl
	{
		public abstract void Obj2form(ITableWithPermissions obj);
		public abstract void Form2obj(ITableWithPermissions obj);
		public abstract void ClearForm();
	}
}

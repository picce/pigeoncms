using PigeonCms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace PigeonCms.Core.Helpers
{
	public class ItemsProxy
    {
        private string itemType = "";
        private bool checkUserContext = false;
        private bool writeMode = false;
        private PigeonCms.Module fakeModule;
        string itemContainerAssemblyString = "";

        private Assembly itemAssembly = null;
        protected Assembly ItemAssembly
        {
            get
            {
                if (itemAssembly == null)
                {
                    //item xml descriptor and avoid xml loading for items
                    if (!itemType.StartsWith("PigeonCms."))
                    {
                        var xmlItemType = new ItemTypeManager().GetByFullName(itemType);
                        itemContainerAssemblyString = xmlItemType.AssemblyString;
                    }
                    try
                    {
                        if (string.IsNullOrEmpty(itemContainerAssemblyString))
                        {
                            //default PigeonCms.Core
                            itemAssembly = Assembly.GetExecutingAssembly();
                        }
                        else
                        {
                            //custom external assembly IoC for the current Item
                            itemAssembly = Assembly.Load(itemContainerAssemblyString);
                        }
                    }
                    catch (Exception ex)
                    {
                        string err = "ItemsProxy.ItemAssembly: {itemType} err: {errDesc}"
                            .Replace("{itemType}", itemType)
                            .Replace("{errDesc}", ex.ToString());
                        LogProvider.Write(fakeModule, err, TracerItemType.Error);

                        if (this.ThrowExceptions)
                            throw ex;
                    }
                }
                return itemAssembly;
            }
        }

        /// <summary>
        /// propagate exceptions
        /// </summary>
        public bool ThrowExceptions { get; set; } = true;

        private bool logException = true;
        /// <summary>
        /// log exceptions 
        /// </summary>
        public bool LogExceptions
        {
            get { return logException; }
            set
            {
                this.logException = value;
                this.fakeModule.UseLog = (this.logException ? Utility.TristateBool.True : Utility.TristateBool.False);
            }
        }


        public ItemsProxy(string itemType = "PigeonCms.Item", bool checkUserContext = false, bool writeMode = false)
		{
            if (string.IsNullOrEmpty(itemType))
                itemType = "PigeonCms.Item";

            //manager instance params
            this.itemType = itemType;
            this.checkUserContext = checkUserContext;
            this.writeMode = writeMode;

            //log settings
            this.fakeModule = new PigeonCms.Module();
            this.fakeModule.ModuleNamespace = "PigeonCms";
            this.fakeModule.ModuleName = "ItemsProxy";
            this.fakeModule.UseLog = (this.LogExceptions ? Utility.TristateBool.True : Utility.TristateBool.False);
        }

		public IItem GetByKey(int id)
		{
            IItem res = GetNewItem();

            if (string.IsNullOrEmpty(itemType))
                return res;

            try
            {
                Type type = getManagerType(res);
                MethodInfo method = type.GetMethod("GetByKey");
                object[] methodArgs = new object[] { id };

                object[] constructorArgs = new object[] { this.checkUserContext, this.writeMode };
                var classInstance = Activator.CreateInstance(type, constructorArgs);

                res = (IItem)method.Invoke(classInstance, methodArgs);
            }
            catch (Exception ex)
            {
                string err = "ItemsProxy.GetByKey({id}) err: {errDesc}"
                    .Replace("{id}", id.ToString())
                    .Replace("{errDesc}", ex.ToString());
                LogProvider.Write(fakeModule, err, TracerItemType.Error);

                if (this.ThrowExceptions)
                    throw ex;
            }
            return res;
        }

        public List<IItem> GetByFilter(IItemsFilter filter, string sort)
        {
            var res = new List<IItem>();

            if (string.IsNullOrEmpty(itemType))
                return res;

            try
            {
                Type type = getManagerType(GetNewItem());
                MethodInfo method = type.GetMethod("GetByFilter");
                object[] methodArgs = new object[] { filter, sort };

                object[] constructorArgs = new object[] { this.checkUserContext, this.writeMode };
                var classInstance = Activator.CreateInstance(type, constructorArgs);

                //http://stackoverflow.com/questions/1829756/casting-a-list-of-objects-to-their-interface-type-in-c-sharp
                res = ((IEnumerable<IItem>)method.Invoke(classInstance, methodArgs)).ToList();
                //res = (List<IItem>)method.Invoke(classInstance, methodArgs);
            }
            catch (Exception ex)
            {
                string err = "ItemsProxy.GetByFilter() err: {errDesc}"
                    .Replace("{errDesc}", ex.ToString());
                LogProvider.Write(fakeModule, err, TracerItemType.Error);

                if (this.ThrowExceptions)
                    throw ex;
            }
            return res;

        }

        public int Update(IItem theObj)
        {
            int res = 0;

            if (string.IsNullOrEmpty(itemType))
                return res;

            try
            {
                Type type = getManagerType(GetNewItem());
                MethodInfo method = type.GetMethod("Update");
                object[] methodArgs = new object[] { theObj };

                object[] constructorArgs = new object[] { this.checkUserContext, this.writeMode };
                var classInstance = Activator.CreateInstance(type, constructorArgs);

                res = (int)method.Invoke(classInstance, methodArgs);
            }
            catch (Exception ex)
            {
                string err = "ItemsProxy.Update(theObj.Id={id}) err: {errDesc}"
                    .Replace("{id}", theObj.Id.ToString())
                    .Replace("{errDesc}", ex.ToString());
                LogProvider.Write(fakeModule, err, TracerItemType.Error);

                if (this.ThrowExceptions)
                    throw ex;
            }
            return res;
        }

        public IItem Insert(IItem newObj)
        {
            var res = GetNewItem();

            if (string.IsNullOrEmpty(itemType))
                return res;

            try
            {
                Type type = getManagerType(GetNewItem());
                MethodInfo method = type.GetMethod(
                    "Insert", new[] { GetNewItem().GetType(), typeof(Boolean) });

                object[] methodArgs = new object[] { newObj, true };

                object[] constructorArgs = new object[] { this.checkUserContext, this.writeMode };
                var classInstance = Activator.CreateInstance(type, constructorArgs);

                res = (IItem)method.Invoke(classInstance, methodArgs);
            }
            catch (Exception ex)
            {
                string err = "ItemsProxy.Insert(newObj.ExtId={extId}) err: {errDesc}"
                    .Replace("{extId}", newObj.ExtId)
                    .Replace("{errDesc}", ex.ToString());
                LogProvider.Write(fakeModule, err, TracerItemType.Error);

                if (this.ThrowExceptions)
                    throw ex;
            }
            return res;
        }

        public int DeleteById(int id)
        {
            int res = 0;

            if (string.IsNullOrEmpty(itemType))
                return res;

            try
            {
                Type type = getManagerType(GetNewItem());
                MethodInfo method = type.GetMethod("DeleteById");
                object[] methodArgs = new object[] { id };

                object[] constructorArgs = new object[] { this.checkUserContext, this.writeMode };
                var classInstance = Activator.CreateInstance(type, constructorArgs);

                res = (int)method.Invoke(classInstance, methodArgs);
            }
            catch (Exception ex)
            {
                string err = "ItemsProxy.DeleteById({id}) err: {errDesc}"
                    .Replace("{id}", id.ToString())
                    .Replace("{errDesc}", ex.ToString());
                LogProvider.Write(fakeModule, err, TracerItemType.Error);

                if (this.ThrowExceptions)
                    throw ex;
            }
            return res;
        }

        public IItem GetNewItem()
		{
			try
			{
				Type itemType = this.ItemAssembly.GetTypes().Where(type => matchItem(type, this.itemType)).FirstOrDefault();
				return (IItem)Activator.CreateInstance(itemType);
			}
			catch (Exception e)
			{
				return null;
			}
		}

        public IItemsFilter GetNewItemFilter()
        {
            try
            {
                var item = GetNewItem();
                //Type type = this.ItemAssembly.GetTypes().Where(x => x.FullName.Equals(item.FilterTypeName)).FirstOrDefault();
                Type type = this.ItemAssembly.GetTypes().Where(x => matchItem(x, item.FilterTypeName)).FirstOrDefault();
                return (IItemsFilter)Activator.CreateInstance(type);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //TODO 
        //Assembly.GetExecutingAssembly().GetReferencedAssemblies
        //http://stackoverflow.com/questions/383686/how-do-you-loop-through-currently-loaded-assemblies

        private Type getManagerType(IItem item)
        {
            try
            {
                //Type type = this.ItemAssembly.GetTypes().Where(x => x.FullName.Equals(item.ManagerTypeName)).FirstOrDefault();
                Type type = this.ItemAssembly.GetTypes().Where(x => matchItem(x, item.ManagerTypeName)).FirstOrDefault();
                return type;
            }
            catch (Exception e)
            {
                return null;
            }
        }

		private bool matchItem(Type type, string itemTypeName)
		{
			string[] itemTypeNameToken = itemTypeName.Split('.');
			return type.Name == itemTypeNameToken.Last();
		}
    }

}

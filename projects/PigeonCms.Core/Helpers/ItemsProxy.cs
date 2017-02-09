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
        private string filterType = "";
        private string managerType = "";
        private bool checkUserContext = false;
        private bool writeMode = false;


        public ItemsProxy(string itemType = "PigeonCms.Item", bool checkUserContext = false, bool writeMode = false)
		{
            this.itemType = itemType;
            this.filterType = itemType + "sFilter";
            this.managerType = itemType + "sManager";
            this.checkUserContext = checkUserContext;
            this.writeMode = writeMode;
        }

		// TODO: avoid switch and use reflection to instatiate manager
		// Ask to use interfaces for ItemsManager and Item
		// https://blogs.msdn.microsoft.com/csharpfaq/2010/02/16/covariance-and-contravariance-faq/
		public IItem GetByKey(int id)
		{
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Type type = getManagerType();
                MethodInfo method = type.GetMethod("GetByKey");
                object[] methodArgs = new object[] { id };

                object[] constructorArgs = new object[] { this.checkUserContext, this.writeMode };
                var classInstance = Activator.CreateInstance(type, constructorArgs);

                var res = (IItem)method.Invoke(classInstance, methodArgs);
                return res;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<IItem> GetByFilter(IItemsFilter filter, string sort)
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Type type = getManagerType();
                MethodInfo method = type.GetMethod("GetByFilter");
                object[] methodArgs = new object[] { filter, sort };

                object[] constructorArgs = new object[] { this.checkUserContext, this.writeMode };
                var classInstance = Activator.CreateInstance(type, constructorArgs);

                var res = (List<IItem>)method.Invoke(classInstance, methodArgs);
                return res;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //TODO Delete Update Insert

        public IItem CreateItem()
		{
			try
			{
				Assembly assembly = Assembly.GetExecutingAssembly();

				Type itemType = assembly.GetTypes().Where(type => matchItem(type, this.itemType)).FirstOrDefault();

				return (IItem)Activator.CreateInstance(itemType);
			}
			catch (Exception e)
			{
				return null;
			}
		}

        private Type getManagerType()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Type type = assembly.GetTypes().Where(x => x.FullName.Equals(this.managerType)).FirstOrDefault();

                return type;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private IItemsFilter getNewItemFilter()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Type type = assembly.GetTypes().Where(x => x.FullName.Equals(this.filterType)).FirstOrDefault();

                return (IItemsFilter)Activator.CreateInstance(type);
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

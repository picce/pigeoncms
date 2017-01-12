using PigeonCms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace PigeonCms.Core.Helpers
{
	public class ItemsProxy
    {
		public ItemsProxy()
		{

		}

		// TODO: avoid switch and use reflection to instatiate manager
		// Ask to use interfaces for ItemsManager and Item
		// https://blogs.msdn.microsoft.com/csharpfaq/2010/02/16/covariance-and-contravariance-faq/
		public IItem GetByKey(int itemId, string itemType, bool checkUserContext = false, bool writeMode = false)
		{
            var item = this.CreateItem(itemType);
            if (item != null)
                return item.MyManager(checkUserContext, writeMode).GetByKey(itemId);
            else
                return null;
		}

		public IItem CreateItem(string itemTypeName)
		{
			try
			{
				Assembly assembly = Assembly.GetExecutingAssembly();

				Type itemType = assembly.GetTypes().Where(type => MatchItem(type, itemTypeName)).FirstOrDefault();

				return (IItem)Activator.CreateInstance(itemType);
			}
			catch (Exception e)
			{
				return null;
			}
		}

		public bool MatchItem(Type type, string itemTypeName)
		{
			string[] itemTypeNameToken = itemTypeName.Split('.');
			return type.Name == itemTypeNameToken.Last();
		}

    }

}

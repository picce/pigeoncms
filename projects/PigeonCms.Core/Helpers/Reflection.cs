using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Compilation;
using System.Reflection;
using System.Text;
using System.Collections.Generic;

namespace PigeonCms
{
    public static class Reflection
    {
        /// <summary>
        /// Call the asssembly dynamically and execute a method
        /// see http://www.c-sharpcorner.com/UploadFile/sridhar_subra/DynamicAssemblyMethod10132008214835PM/DynamicAssemblyMethod.aspx
        /// </summary>
        /// <param name="AssemblyName">Name of the Assembly to be loaded</param>
        /// <param name="className">Name of the class to be intantiated </param>
        /// <param name="methodName">Name of the method to be called</param>
        /// <param name="parameterForTheMethod">Parameters should be passed as object array</param>
        /// <returns>Returns as Generic object..</returns>
        /*
        //Input parameters to be passed to the method
        object[] Parameter =  new object[1];
        Parameter[0]= textBox1.Text;
        //Calling the method
        //Parameters Assembly Name, Class Name, Method Name, Parmateres as Array    
        object obj = Process("BusinessLogic", "BankAccount", "GetBalance", Parameter);
        //Assign the result
        label2.Text = "Balance in your account is:" + Convert.ToString(obj);          
        */
        public static object Process(/*string AssemblyName, */string className, string methodName, 
            object[] parameterForTheMethod)
        {
            object returnObject = null;
            MethodInfo mi = null;
            ConstructorInfo ci = null;
            //PropertyInfo pi = null; //type.GetProperty();
            object responder = null;
            Type type = null;
            System.Type[] objectTypes;
            int count = 0;

            try
            {
                //Load the assembly and get it's information
                type = BuildManager.GetType(className, false);

                //TODO - manage classes with generics
                //example: className = "MyClass`2"
                //http://stackoverflow.com/questions/1151464/how-to-dynamically-create-generic-c-sharp-object-using-reflection
                //http://www.codeproject.com/Articles/22088/Reflecting-on-Generics

                //type = System.Reflection.Assembly.LoadFrom(AssemblyName + ".dll").GetType(AssemblyName + "." + className);
                //Get the Passed parameter types to find the method type
                if (parameterForTheMethod != null)
                {
                    objectTypes = new System.Type[parameterForTheMethod.GetUpperBound(0) + 1];
                    foreach (object objectParameter in parameterForTheMethod)
                    {
                        if (objectParameter != null)
                            objectTypes[count] = objectParameter.GetType();
                        count++;
                    }
                    mi = type.GetMethod(methodName, objectTypes);
                }
                else
                {
                    mi = type.GetMethod(methodName);
                }
                if (!mi.IsStatic)
                {
                    ci = type.GetConstructor(Type.EmptyTypes);
                    responder = ci.Invoke(null);
                }
                //Invoke the method
                returnObject = mi.Invoke(responder, parameterForTheMethod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                mi = null;
                ci = null;
                responder = null;
                type = null;
                objectTypes = null;
            }

            //Return the value as a generic object
            return returnObject;

        }


		public static int GetNonNullStringPropertiesCount(object obj)
		{
			int count = 0;

			if (obj != null)
			{
				foreach (PropertyInfo pi in obj.GetType().GetProperties())
				{
					if (pi.PropertyType == typeof(string))
					{
						string value = (string)pi.GetValue(obj);
						if (!string.IsNullOrEmpty(value))
						{
							count++;
						}
					}
				}
			}

			return count;
		}

		public static string BuildQueryStringByObject(object obj)
		{
			string queryString = "", key = "", value = "";

			if (obj != null)
			{
				foreach (PropertyInfo pi in obj.GetType().GetProperties())
				{
					key = pi.Name;
					value = (string)pi.GetValue(obj);

					if (value != "")
					{
						queryString += key + "=" + value + "&";
					}
				}
			}

			return queryString == "" ? "" : queryString.Substring(0, queryString.Length - 1);
		}

		public static List<string> GetListOfPropertiesByObject(object obj)
		{
			List<string> retValue = new List<string>();
			string key = "", value = "";

			if (obj != null)
			{
				foreach (PropertyInfo pi in obj.GetType().GetProperties())
				{
					key = pi.Name;
					value = (string)pi.GetValue(obj);

					if (!String.IsNullOrEmpty(value))
					{
						retValue.Add(key + "|" + value);
					}
				}
			}

			return retValue;
		}

        public static List<T> CreateInstancesOfNestedType<T>(object obj)
        {
            List<T> res = new List<T>();

            if (obj == null)
                return res;

            return CreateInstancesOfNestedType<T>(obj.GetType());
        }

        public static List<T> CreateInstancesOfNestedType<T>(Type objType)
        {
            List<T> res = new List<T>();

            if (objType == null)
                return res;

            foreach (Type nestedType in objType.GetNestedTypes())
            {
                if (typeof(T).IsAssignableFrom(nestedType))
                {
                    res.Add((T)Activator.CreateInstance(nestedType));
                }
            }
            return res;
        }

        //public static T CreateInstanceOfNestedType<T>(object obj)
		//{
		//	if (obj == null)
		//		return default(T);

		//	return CreateInstanceOfNestedType<T>(obj.GetType());
		//}

		//public static T CreateInstanceOfNestedType<T>(Type objType)
		//{
		//	if (objType == null)
		//		return default(T);

		//	foreach (Type nestedType in objType.GetNestedTypes())
		//	{
		//		if (typeof(T).IsAssignableFrom(nestedType))
		//			return (T)Activator.CreateInstance(nestedType);
		//	}

		//	return default(T);
		//}

		public static Type GetNestedType<T>(Type objType)
		{
			if (objType == null)
				return null;

			foreach (Type nestedType in objType.GetNestedTypes())
			{
				if (typeof(T).IsAssignableFrom(nestedType))
					return nestedType;
			}

			return null;
		}

		public static string PropertiesToString(object obj)
		{
			if (obj == null)
				return "";

			StringBuilder result = new StringBuilder();
			PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

			foreach (PropertyInfo property in properties)
			{
				object propValue = property.GetValue(obj);
				if (propValue == null)
					continue;

				result.Append(string.Format("{0}={1};", property.Name, propValue.ToString()));
			}

			return result.ToString();
		}

    }
}
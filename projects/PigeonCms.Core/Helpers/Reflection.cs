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

    }
}
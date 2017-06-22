using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Data.Common;
using PigeonCms;
using System.Diagnostics;

namespace PigeonCms
{
    public class SeoManager: TableManager<Seo, SeoFilter, int>
    {

		string resourceSet = "";

        [DebuggerStepThrough()]
		public SeoManager(string resourceSet = "")
        {
            this.TableName = "#__seo";
            this.KeyFieldName = "Id";
			this.resourceSet = resourceSet;
        }


        public override Seo GetByKey(int id)
        {
			DbProviderFactory myProv = Database.ProviderFactory;
			DbConnection myConn = myProv.CreateConnection();
			DbDataReader myRd = null;
			DbCommand myCmd = myConn.CreateCommand();
			string sSql;
			var result = new Seo();

			if (id <= 0)
				return result;

			try
			{
				myConn.ConnectionString = Database.ConnString;
				myConn.Open();
				myCmd.Connection = myConn;

				sSql = "SELECT t.Id, t.ResourceSet, t.DateUpdated, t.UserUpdated, "
					+ " t.NoIndex, t.NoFollow, c.CultureName, "
                    + " c.Title, c.Description, c.Slug "
					+ " FROM [" + this.TableName + "] t "
					+ " LEFT JOIN [" + this.TableName + "_Culture] c ON t.Id = c.SeoId "
					+ " WHERE t.Id = @Id ";
				
				myCmd.Parameters.Add(Database.Parameter(myProv, "Id", id));

				if (!string.IsNullOrEmpty(this.resourceSet))
				{
					sSql += " AND t.ResourceSet=@ResourceSet ";
					myCmd.Parameters.Add(Database.Parameter(myProv, "ResourceSet", resourceSet));
				}

				myCmd.CommandText = Database.ParseSql(sSql);
				myRd = myCmd.ExecuteReader();
				while (myRd.Read())
				{
					FillObject(result, myRd);

					if (!Convert.IsDBNull(myRd["Title"]))
						result.TitleTranslations.Add((string)myRd["cultureName"], (string)myRd["Title"]);
					if (!Convert.IsDBNull(myRd["Description"]))
						result.DescriptionTranslations.Add((string)myRd["cultureName"], (string)myRd["Description"]);
                    if (!Convert.IsDBNull(myRd["Slug"]))
                        result.SlugTranslations.Add((string)myRd["cultureName"], (string)myRd["Slug"]);

                }
				myRd.Close();

			}
			finally
			{
				myConn.Dispose();
			}

            return result;
        }


        public override int Update(Seo theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            int result = 0;


            theObj.DateUpdated = DateTime.Now;
            theObj.UserUpdated = PgnUserCurrent.UserName;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "UPDATE [" + this.TableName + "] "
                + " SET DateUpdated = @DateUpdated, UserUpdated = @UserUpdated, "
				+ " NoIndex = @NoIndex, NoFollow = @NoFollow "
                + " WHERE Id = @Id";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateUpdated", theObj.DateUpdated));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserUpdated", theObj.UserUpdated));
				myCmd.Parameters.Add(Database.Parameter(myProv, "NoIndex", theObj.NoIndex));
				myCmd.Parameters.Add(Database.Parameter(myProv, "NoFollow", theObj.NoFollow));

                result = myCmd.ExecuteNonQuery();
                updateCultureText(theObj, myCmd, myProv);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override Seo Insert(Seo newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new Seo();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                result = newObj;

                result.DateUpdated = DateTime.Now;
                if (string.IsNullOrEmpty(result.UserUpdated))
                    result.UserUpdated = PgnUserCurrent.UserName;

				sSql = "INSERT INTO [" + this.TableName + "](ResourceSet, DateUpdated, UserUpdated, NoIndex, NoFollow) "
				+ " VALUES(@ResourceSet, @DateUpdated, @UserUpdated, @NoIndex, @NoFollow) "
                + " SELECT SCOPE_IDENTITY()";
                myCmd.CommandText = Database.ParseSql(sSql);

                //myCmd.Parameters.Add(Database.Parameter(myProv, "Id", result.Id));//IDENTITY
				myCmd.Parameters.Add(Database.Parameter(myProv, "ResourceSet", this.resourceSet));
				myCmd.Parameters.Add(Database.Parameter(myProv, "DateUpdated", result.DateUpdated));
				myCmd.Parameters.Add(Database.Parameter(myProv, "UserUpdated", result.UserUpdated));
				myCmd.Parameters.Add(Database.Parameter(myProv, "NoIndex", result.NoIndex));
				myCmd.Parameters.Add(Database.Parameter(myProv, "NoFollow", result.NoFollow));

                result.Id = (int)(decimal)myCmd.ExecuteScalar();

                updateCultureText(result, myCmd, myProv);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

		public override int DeleteById(int recordId)
		{
			DbProviderFactory myProv = Database.ProviderFactory;
			DbConnection myConn = myProv.CreateConnection();
			DbCommand myCmd = myProv.CreateCommand();
			int res = 0;
			string sSql = "";

			try
			{
				myConn.ConnectionString = Database.ConnString;
				myConn.Open();
				myCmd.Connection = myConn;

				//item
				sSql = "DELETE FROM [" + this.TableName + "] WHERE Id = @Id ";
				myCmd.CommandText = Database.ParseSql(sSql);
				myCmd.Parameters.Clear();
				myCmd.Parameters.Add(Database.Parameter(myProv, "Id", recordId));
				myCmd.ExecuteNonQuery();

				//culture
				sSql = "DELETE FROM [" + this.TableName + "_Culture] WHERE SeoId = @SeoId ";
				myCmd.CommandText = Database.ParseSql(sSql);
				myCmd.Parameters.Clear();
				myCmd.Parameters.Add(Database.Parameter(myProv, "SeoId", recordId));
				myCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				myConn.Dispose();
			}
			return res;
		}

        protected override void FillObject(Seo result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
			if (!Convert.IsDBNull(myRd["ResourceSet"]))
				result.ResourceSet = (string)myRd["ResourceSet"];
            if (!Convert.IsDBNull(myRd["DateUpdated"]))
                result.DateUpdated = (DateTime)myRd["DateUpdated"];
            if (!Convert.IsDBNull(myRd["UserUpdated"]))
                result.UserUpdated = (string)myRd["UserUpdated"];
			if (!Convert.IsDBNull(myRd["NoIndex"]))
				result.NoIndex = (bool)myRd["NoIndex"];
			if (!Convert.IsDBNull(myRd["NoFollow"]))
				result.NoFollow = (bool)myRd["NoFollow"];
        }


        private void updateCultureText(Seo theObj, DbCommand myCmd, DbProviderFactory myProv)
        {
            foreach (KeyValuePair<string, string> item in theObj.TitleTranslations)
            {
                string sSql = "";
                string descriptionValue = "";
                string slugValue = "";


                theObj.DescriptionTranslations.TryGetValue(item.Key, out descriptionValue);
                if (string.IsNullOrEmpty(descriptionValue))
                    descriptionValue = "";

                theObj.SlugTranslations.TryGetValue(item.Key, out slugValue);
                if (string.IsNullOrEmpty(slugValue))
                    slugValue = "";

                //delete previous entry (if exists)
                sSql = "DELETE FROM [" + this.TableName + "_culture] WHERE CultureName=@CultureName AND SeoId=@SeoId ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "CultureName", item.Key));
				myCmd.Parameters.Add(Database.Parameter(myProv, "SeoId", theObj.Id));
                myCmd.ExecuteNonQuery();

                //insert current culture entry
				sSql = "INSERT INTO [" + this.TableName + "_culture](CultureName, SeoId, Title, Description, Slug) "
				+ " VALUES(@CultureName, @SeoId, @Title, @Description, @Slug) ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "CultureName", item.Key));
				myCmd.Parameters.Add(Database.Parameter(myProv, "SeoId", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Title", item.Value));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Description", descriptionValue));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Slug", slugValue));

                myCmd.ExecuteNonQuery();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading;

namespace PigeonCms
{
    public class DbVersion : ITable
    {
        private string componentFullName = "";
        private int versionId = 0;
        private DateTime versionDate = DateTime.MinValue;
        private string versionDev = "";
        private string versionNotes = "";
        private DateTime dateUpdated = DateTime.MinValue;
        private string userUpdated = "";
        private string sqlContent = "";

        /// <summary>
        /// example: PigeonCms.Core
        /// </summary>
        [DataObjectField(false)]
        public string ComponentFullName
        {
            get { return componentFullName; }
            set { componentFullName = value; }
        }

        /// <summary>
        /// database version id
        /// </summary>
        [DataObjectField(false)]
        public int VersionId
        {
            get { return versionId; }
            set { versionId = value; }
        }

        /// <summary>
        /// database patch date
        /// </summary>
        [DataObjectField(false)]
        public DateTime VersionDate
        {
            get { return versionDate; }
            set { versionDate = value; }
        }

        /// <summary>
        /// developer reference
        /// </summary>
        [DataObjectField(false)]
        public string VersionDev
        {
            get { return versionDev; }
            set { versionDev = value; }
        }

        /// <summary>
        /// notes ord summary about version update
        /// </summary>
        [DataObjectField(false)]
        public string VersionNotes
        {
            get { return versionNotes; }
            set { versionNotes = value; }
        }

        [DataObjectField(false)]
        public DateTime DateUpdated
        {
            get { return dateUpdated; }
            set { dateUpdated = value; }
        }

        [DataObjectField(false)]
        public string UserUpdated
        {
            get { return userUpdated; }
            set { userUpdated = value; }
        }

        public string SqlContent
        {
            get { return sqlContent; }
            set { sqlContent = value; }
        }

        public DbVersion() { }

        public override string ToString()
        {
            return "componentFullName: " + componentFullName + "; "
            + "versionId: " + versionId.ToString() + "; "
            + "versionDate: " + versionDate.ToShortDateString() + "; "
            + "versionDev: " + versionDev + "; ";
        }

    }

    [Serializable]
    public class DbVersionsFilter
    {
        private string componentFullName = "";
        private int versionId = 0;

        public string ComponentFullName
        {
            get { return componentFullName; }
            set { componentFullName = value; }
        }

        public int VersionId
        {
            get { return versionId; }
            set { versionId = value; }
        }

    }

}

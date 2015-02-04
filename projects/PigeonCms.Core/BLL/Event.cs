using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using PigeonCms;
using System.Collections.Generic;
using System.Threading;
using System.Web.Compilation;
using System.Reflection;



namespace PigeonCms
{
    public class Event: ITable
    {
        public enum EventStatusEnum
        {
            ToConfirm = 0,
            Confirmed = 1
        }

        //private int id = 0;
        //private int resourceId = 0;
        //private int groupId = 0;
        //private int orderId = 0;
        private EventStatusEnum status = EventStatusEnum.ToConfirm;
        private string name = "";
        private string description = "";
        private DateTime eventStart;
        private DateTime eventEnd;


        [DataObjectField(true)]
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public int GroupId { get; set; }
        /// <summary>
        /// ref to PayPalIntegration TmpOrder
        /// </summary>
        public int OrderId { get; set; }

        public EventStatusEnum Status 
        {
            [DebuggerStepThrough()]
            get { return status; }
            [DebuggerStepThrough()]
            set { status = value; }
        }

        public string Name
        {
            [DebuggerStepThrough()]
            get { return name; }
            [DebuggerStepThrough()]
            set { name = value; }
        }

        public string Description
        {
            [DebuggerStepThrough()]
            get { return description; }
            [DebuggerStepThrough()]
            set { description = value; }
        }

        public DateTime EventStart
        {
            [DebuggerStepThrough()]
            get { return eventStart; }
            [DebuggerStepThrough()]
            set { eventStart = value; }
        }

        public DateTime EventEnd
        {
            [DebuggerStepThrough()]
            get { return eventEnd; }
            [DebuggerStepThrough()]
            set { eventEnd = value; }
        }

        public Event() { }
    }


    [Serializable]
    public class EventsFilter
    {
        //private int id = 0;
        //private int resourceId = 0;
        //private int groupId = 0;
        //private int orderId = 0;
        private bool filterStatus = false;
        private Event.EventStatusEnum status = Event.EventStatusEnum.ToConfirm;
        private string nameSearch = "";
        private DateTime eventStart;
        private DateTime eventEnd;
        

        [DataObjectField(true)]
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public int GroupId { get; set; }
        public int OrderId { get; set; }

        public bool FilterStatus
        {
            [DebuggerStepThrough()]
            get { return filterStatus; }
            [DebuggerStepThrough()]
            set { filterStatus = value; }
        }

        public Event.EventStatusEnum Status
        {
            [DebuggerStepThrough()]
            get { return status; }
            [DebuggerStepThrough()]
            set { status = value; }
        }

        public string NameSearch
        {
            [DebuggerStepThrough()]
            get { return nameSearch; }
            [DebuggerStepThrough()]
            set { nameSearch = value; }
        }

        public DateTime EventStart
        {
            [DebuggerStepThrough()]
            get { return eventStart; }
            [DebuggerStepThrough()]
            set { eventStart = value; }
        }

        public DateTime EventEnd
        {
            [DebuggerStepThrough()]
            get { return eventEnd; }
            [DebuggerStepThrough()]
            set { eventEnd = value; }
        }
    }
}
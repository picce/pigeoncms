using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PigeonCms
{
    public class DatesRange
    {
        public enum RangeType
        {
            Custom = 0,
            None,
            Always,
            Today,
            LastWeek,
            LastMonth
        }

        DateTime initDate = DateTime.MinValue;
        DateTime endDate = DateTime.MinValue;
        RangeType dateRangeType = RangeType.Custom;

        public DateTime InitDate
        {
            get 
            {
                DateTime res = initDate;
                switch (dateRangeType)
                {
                    case RangeType.Custom:
                        res = initDate;
                        break;
                    case RangeType.None:
                        res = DateTime.MaxValue;
                        break;
                    case RangeType.Always:
                        res = DateTime.MinValue;
                        break;
                    case RangeType.Today:
                        res = DateTime.Today;
                        break;
                    case RangeType.LastWeek:
                        res = DateTime.Today.Subtract(TimeSpan.FromDays(7.0));
                        break;
                    case RangeType.LastMonth:
                        res = DateTime.Today.Subtract(TimeSpan.FromDays(30.0));
                        break;
                }
                return res;
            }
        }

        public DateTime EndDate
        {
            get
            {
                DateTime res = endDate;
                switch (dateRangeType)
                {
                    case RangeType.Custom:
                        res = endDate;
                        break;
                    case RangeType.None:
                        res = DateTime.MaxValue;
                        break;
                    case RangeType.Always:
                        res = DateTime.MaxValue;
                        break;
                    case RangeType.Today:
                        res = DateTime.Today;
                        break;
                    case RangeType.LastWeek:
                        res = DateTime.Today;
                        break;
                    case RangeType.LastMonth:
                        res = DateTime.Today;
                        break;
                }
                return res;
            }
        }
        
        public RangeType DateRangeType 
        {
            get { return dateRangeType; }
        }

        /// <summary>
        /// default RangeType.Always
        /// </summary>
        public DatesRange()
        {
            this.dateRangeType = RangeType.Always;
        }

        /// <param name="rangeType"></param>
        public DatesRange(RangeType rangeType) 
        {
            this.dateRangeType = rangeType;
        }

        /// <summary>
        /// constructor with custom dates range
        /// </summary>
        /// <param name="initDate">set to DateTime.MinValue to skip</param>
        /// <param name="endDate">set to DateTime.MaxValue to skip</param>
        public DatesRange(DateTime initDate, DateTime endDate)
        {
            this.initDate = initDate;
            this.endDate = endDate;
            this.dateRangeType = RangeType.Custom;
        }
    }
}

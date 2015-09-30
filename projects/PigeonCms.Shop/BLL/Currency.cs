using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PigeonCms.Shop
{
    public class Currency: ITable
    {

        public Currency()
            : this("")
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="currencyString">Coma separated Code, Symbol and name. Example: EUR, €, Euro</param>
        public Currency(string currencyString)
        {
            string[] arr = currencyString.Split(',');
            if (arr == null || arr.Length < 3)
                throw new ArgumentException("Invalid currency string");

            this.code = arr[0].Trim();
            this.symbol = arr[1].Trim();
            this.name = arr[2].Trim();
        }

        private string code = "";
        /// <summary>
        /// ex: EUR
        /// </summary>
        [DataObjectField(true)]
        public string Code
        {
            [DebuggerStepThrough()]
            get { return code; }
            [DebuggerStepThrough()]
            set { code = value; }
        }

        private string symbol = "";
        /// <summary>
        /// ex: €
        /// </summary>
        [DataObjectField(true)]
        public string Symbol
        {
            [DebuggerStepThrough()]
            get { return symbol; }
            [DebuggerStepThrough()]
            set { symbol = value; }
        }

        private string name = "";
        /// <summary>
        /// ex: Euro
        /// </summary>
        [DataObjectField(true)]
        public string Name
        {
            [DebuggerStepThrough()]
            get { return name; }
            [DebuggerStepThrough()]
            set { name = value; }
        }

    }
}

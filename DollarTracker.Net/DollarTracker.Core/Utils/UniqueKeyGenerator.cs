using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Utils
{
	public class UniqueKeyGenerator
	{
		private static readonly int shortKeyLen = 20;
		public static string DatePrefixShortKey()
		{
			//todo: need to double check about possible key collision
			return DatePrefixKey(shortKeyLen);
		}
		public static string DatePrefixKey(int len = 20)
		{
			string months = "123456789ABC";
			var n = DateTime.Now;
			var m = months[n.Month - 1].ToString();
			return (n.Year.ToString() + m + Guid.NewGuid().ToString("N")).Substring(0, len);
		}
	}
}

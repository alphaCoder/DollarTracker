using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Utils
{
	public class UniqueKeyGenerator
	{
		private static readonly int longKeyLen = 40;
		private static readonly int shortKeyLen = 40;
		public static string DatePrefixShortKey(int len = 20)
		{
			//todo: need to double check about possible key collision
			return DatePrefixKey(shortKeyLen);
		}
		public static string DatePrefixLongKey()
		{
			return DatePrefixKey(longKeyLen);
		}
		public static string DatePrefixKey(int len = 20)
		{
			string months = "123456789ABC";
			var n = DateTime.Now;
			return (n.Year.ToString() + months[n.Month - 1].ToString() + Guid.NewGuid().ToString("N")).Substring(0, len);
		}
	}
}

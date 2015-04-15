using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DollarTracker.Web.Utils
{
	public class SimpleJwt
	{
		public string sub { get; set; }
		public string exp { get; set; }
	}
	public class JwtHelper
	{
		public static SimpleJwt ExtractJwtFromBearerLine(string bearerTokenLine)
		{
			SimpleJwt jwt = default(SimpleJwt);
			try
			{
				if (bearerTokenLine.StartsWith("Bearer "))
				{
					bearerTokenLine = bearerTokenLine.Substring(7).Trim();
				}
				if (!string.IsNullOrEmpty(bearerTokenLine))
				{
					jwt = JsonConvert.DeserializeObject<SimpleJwt>(bearerTokenLine);	
				}
				
			}
			catch (Exception e)
			{
			}
			return jwt;
		}
		public static bool IsValid(SimpleJwt simpleJwt, out string errorMsg)
		{
			errorMsg = "";
			try
			{
				System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
				dtDateTime = dtDateTime.AddSeconds(int.Parse(simpleJwt.exp)).ToUniversalTime();
				if (dtDateTime <= DateTime.UtcNow)
				{
					errorMsg = "Token Expired";
				}

			}
			catch (Exception e)
			{
				errorMsg = "Unknown Error";
			}

			return string.IsNullOrEmpty(errorMsg);
		}
	}
}
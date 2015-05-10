using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;

namespace DollarTracker.Web.Utils
{
	public class UserInfo
	{
		public string Email { get; set; }
		public string DisplayName { get; set; }
	}
	public class SimpleJwt
	{
		public string Sub { get; set; }
		public string Exp { get; set; }
		public UserInfo UserInfo { get; set; }
	}

	public interface IJwtHelper
	{
		SimpleJwt ExtractJwtFromBearerLine(string bearerTokenLine);
		bool IsValid(SimpleJwt simpleJtw);
	}
	public class JwtHelper : IJwtHelper
	{
		public SimpleJwt ExtractJwtFromBearerLine(string bearerTokenLine)
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
					var kernel = DollarTracker.Web.App_Start.NinjectWebCommon.Kernel;
					var appSettingMgr = kernel.Get<DollarTracker.Core.Managers.IAppSettingManager>();
					var jwtSecret = appSettingMgr.GetByName("JwtSecret");
					var decodedJwt = JWT.JsonWebToken.Decode(bearerTokenLine, jwtSecret); //currently hard coding.
					if (decodedJwt != null)
					{
						jwt = JsonConvert.DeserializeObject<SimpleJwt>(decodedJwt);
					}
				}
				
			}
			catch (Exception e)
			{
			}
			return jwt;
		}
		public bool IsValid(SimpleJwt simpleJwt)
		{
			bool isValidJwt = false;
			try
			{
				System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
				dtDateTime = dtDateTime.AddSeconds(int.Parse(simpleJwt.Exp)).ToUniversalTime();
				if (dtDateTime >= DateTime.UtcNow)
				{
					isValidJwt = true;
				}

			}
			catch (Exception e)
			{
				isValidJwt = false;
			}

			return isValidJwt;
		}
	}
}
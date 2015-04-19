using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using JWT;
using DollarTracker.Core.Managers;
using DollarTracker.Common;
using Ninject;
using DollarTracker.Web.Utils;
namespace DollarTracker.Web.ApiControllers
{
    public class DollarTrackerBaseController : ApiController
    {
		private UserClaim claim = null;
		protected UserClaim UserClaim
		{
			get
			{
				if (claim == null)
				{
					string AuthHeaderName = "authorization";
					IEnumerable<string> authHeaders = null;
					Request.Headers.TryGetValues(AuthHeaderName, out authHeaders);
					if (authHeaders != null)
					{
						string bearerTokenLine = authHeaders.First();
						var jwtHelper = DollarTracker.Web.App_Start.NinjectWebCommon.Kernel.Get<IJwtHelper>();

						var simpleJwt = jwtHelper.ExtractJwtFromBearerLine(bearerTokenLine);
						claim = new UserClaim
						{
							Email = simpleJwt.UserInfo.Email,
							UserId = simpleJwt.Sub,
							DisplayName = simpleJwt.UserInfo.DisplayName
						};
					}
				}
				return claim;
			}
		}
	
    }
}

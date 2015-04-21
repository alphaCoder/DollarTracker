using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Net.Http;
using Ninject;
using DollarTracker.Core.Managers;
namespace DollarTracker.Web.Utils
{
	[AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public class DTJwtApiAuthorization : System.Web.Http.AuthorizeAttribute
	{
		static readonly string AuthHeaderName = "Authorization";

		public List<string> AcceptedRoles { get; set; }

		public DTJwtApiAuthorization(params string[] acceptedRoles)
			: base()
		{
			AcceptedRoles = acceptedRoles.Select(a => a.ToUpper()).ToList();
		}

		protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
		{
			HttpContext.Current.Response.AddHeader("AuthenticationStatus", "NotAuthorized");
			actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
		}

		protected override bool IsAuthorized(HttpActionContext actionContext)
		{
			IEnumerable<string> authHeaders = null;
			actionContext.Request.Headers.TryGetValues(AuthHeaderName, out authHeaders); 

			if (authHeaders == null || !authHeaders.Any())
			{
				return false;
			}

			string authValue = authHeaders.First();

			bool authenticated = false;
			try
			{
				var kernel = DollarTracker.Web.App_Start.NinjectWebCommon.Kernel;
				var jwtHelper = kernel.Get<IJwtHelper>();
				var simpleJwt = jwtHelper.ExtractJwtFromBearerLine(authValue);

				if (simpleJwt != null)
				{
					if (jwtHelper.IsValid(simpleJwt))
					{
						authenticated = true;

						var userManager = kernel.Get<IUserManager>();
						if (userManager != null)
						{
							if (userManager.GetUserViaEmail(simpleJwt.UserInfo.Email) == null)
							{
								var user = new DollarTracker.EF.User
								{
									Email = simpleJwt.UserInfo.Email,
									UserId = simpleJwt.Sub,
									DisplayName = simpleJwt.UserInfo.DisplayName,
									CreatedDtUtc = DateTime.UtcNow,
									Status = true
								};
								userManager.AddUser(user);
							} 
						}
					}
				}

			}
			catch (Exception e)
			{
				return false;
			}

			return authenticated;
		}
	}
}
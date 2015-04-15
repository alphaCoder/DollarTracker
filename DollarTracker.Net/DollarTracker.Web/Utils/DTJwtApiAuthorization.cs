using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Net.Http;
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
			actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Forbidden);
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

			string errorMsg = "";
			bool authenticated = false;
			var simpleJwt = JwtHelper.ExtractJwtFromBearerLine(authValue);

			if (simpleJwt != null)
			{
				if (JwtHelper.IsValid(simpleJwt, out errorMsg))
				{
					authenticated = true;
				}
			}

			if (!authenticated) return false;
			//todo: how do we check teh authrization of this access? we need to know the role and the closing diclosedId
		//	var jwt = TinyJwtHelper.ExtractFromBearerToken(authValue);

			//todo: we can drill into the jwt to see if somethogn wrong
	//		var user = jwt.FindUser();

			// base on the user, I can determine the authorized

			bool authorized = true; //AcceptedRoles.Count == 0 || (user.Roles !=null && user.Roles.Intersect(AcceptedRoles).Count() > 0);

			//todo: coming back to clean this up.
			//authorized = true;

			return authenticated && authorized;
		}
	}
}
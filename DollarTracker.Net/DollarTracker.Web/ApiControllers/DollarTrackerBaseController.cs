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
namespace DollarTracker.Web.ApiControllers
{
    public class DollarTrackerBaseController : ApiController
    {
		private readonly IAppSettingManager appSettingManager;
		protected UserClaim User { get; set; }
		public DollarTrackerBaseController(IAppSettingManager appSettingManager)
		{
			this.appSettingManager = appSettingManager;
		}
		protected static Task<System.Net.Http.HttpResponseMessage> UnauthorizedAccessResponseTask(System.Threading.CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(() =>
			{
				System.Net.Http.HttpResponseMessage unauthenticatedResponse = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
				return unauthenticatedResponse;
			}, cancellationToken);
		}



		public override Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Web.Http.Controllers.HttpControllerContext controllerContext, System.Threading.CancellationToken cancellationToken)
		{
			if (controllerContext.Request.Headers.Authorization == null || string.IsNullOrEmpty(controllerContext.Request.Headers.Authorization.Scheme)
				|| controllerContext.Request.Headers.Authorization.Scheme != "Bearer" ||
				string.IsNullOrEmpty(controllerContext.Request.Headers.Authorization.Parameter))
			{
				return UnauthorizedAccessResponseTask(cancellationToken);
			}


			string authHeaderName = "authorization";
					IEnumerable<string> authHeaders = null;
					controllerContext.Request.Headers.TryGetValues(authHeaderName, out authHeaders);
					if (authHeaders != null)
					{
						var bearerTokenLine = authHeaders.First();
						string jwtSecret = appSettingManager.GetByName("JwtSecret");
						string payload = JWT.JsonWebToken.Decode(bearerTokenLine.Split(' ')[1], jwtSecret, false);

					}

			return base.ExecuteAsync(controllerContext, cancellationToken);
		}
    }
}

using DollarTracker.Core.Infrastructure;
using DollarTracker.Core.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Services
{
	public class GoogleAuthProvider : IAuthProvider
	{
		private readonly IAppSettingManager appSettingManager;
		public GoogleAuthProvider(IAppSettingManager appSettingManager)
		{
			this.appSettingManager = appSettingManager;
		}
		public void AuthenticateAsync(Common.OAuthRequest oAuthRequest)
		{
			var clientId = appSettingManager.GetByName("Google-ClientId");
			var clientSecret = appSettingManager.GetByName("Google-ClientSecret");
			var url = @"https://accounts.google.com/o/oauth2/token?
						client_id={0}&
						redirect_uri={1}&
						code={2}&
						grant_type={3}&
						client_secret={4}";
			var tokenUrl = string.Format(url, clientId, oAuthRequest.RedirectUri, oAuthRequest.Code, "authorization_code", clientSecret);
			Dictionary<string, string> tokens = new Dictionary<string,string>();

			HttpWebRequest request = WebRequest.Create(tokenUrl) as HttpWebRequest;
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = 0;
			//request.
			try
			{
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					StreamReader reader = new StreamReader(response.GetResponseStream());
					string vals = reader.ReadToEnd();
					foreach (string token in vals.Split('&'))
					{
						tokens.Add(token.Substring(0, token.IndexOf("=")),
							token.Substring(token.IndexOf("=") + 1, token.Length - token.IndexOf("=") - 1));
					}

					string access_token = tokens["access_token"]; //will never expire
				}
			}
			catch (Exception e)
			{
				var abc = e.Message;
			}
		}
	}
}

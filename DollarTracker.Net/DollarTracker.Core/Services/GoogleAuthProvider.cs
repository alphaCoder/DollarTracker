using DollarTracker.Common;
using DollarTracker.Core.Infrastructure;
using DollarTracker.Core.Managers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security.Provider;
using System.Net.Http;
using Newtonsoft.Json.Linq;
//using Microsoft.Owin.Security.;
namespace DollarTracker.Core.Services
{
	public class GoogleAuthProvider : IAuthProvider
	{
		private readonly IAppSettingManager appSettingManager;
		public GoogleAuthProvider(IAppSettingManager appSettingManager)
		{
			this.appSettingManager = appSettingManager;
		}

		public void Junck()
		{
			//var google = new Microsoft.Owin.Security.Google.GoogleAuthenticatedContext
		}
		public async Task<OAuthTokenValidationResponse> AuthenticateAsync(Common.OAuthRequest oAuthRequest)
		{
			var clientId = appSettingManager.GetByName("Google-ClientId");
			var clientSecret = appSettingManager.GetByName("Google-ClientSecret");
			var url = @"https://accounts.google.com/o/oauth2/token";

			StringBuilder data = new StringBuilder();
			data.Append("grant_type=" + Uri.EscapeDataString("authorization_code"));
			data.Append("&client_id=" + Uri.EscapeDataString(clientId));
			data.Append("&redirect_uri=" + Uri.EscapeDataString(oAuthRequest.RedirectUri));
			data.Append("&code=" + Uri.EscapeDataString(oAuthRequest.Code));
			data.Append("&client_secret=" + Uri.EscapeDataString(clientSecret));

			var post = new Dictionary<string, string>();
			post.Add("grant_type", "authorization_code");
			post.Add("client_id", clientId);
			post.Add("redirect_uri", oAuthRequest.RedirectUri);
			post.Add("code", oAuthRequest.Code);
			post.Add("client_secret", clientSecret);

				var client = new HttpClient();
				var response = await client.PostAsync(url, new FormUrlEncodedContent(post));
				var content = await response.Content.ReadAsStringAsync();
				content = content.Replace("\n", "");
				OAuthTokenValidationResponse valResponse = JsonConvert.DeserializeObject<OAuthTokenValidationResponse>(content);

				return valResponse;
				//content = content.Replace("{", "");
				//content = content.Replace("}", "");
				//content = content.Replace("\"", "'");
				//var tokens = content.Split(',');
				//var abc = json;
			//}
			//catch (JsonReaderException e)
			//{
			//	var ab = e.Message;
			//	throw;
			//}
			/*
			//	var tokenUrl = string.Format(url, HttpUtility.UrlEncode(clientId), HttpUtility.UrlEncode(oAuthRequest.RedirectUri), oAuthRequest.Code, "authorization_code", clientSecret);
			Dictionary<string, string> tokens = new Dictionary<string, string>();
			byte[] byteArray = Encoding.UTF8.GetBytes(data.ToString());
			HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = byteArray.Length;

			OAuthTokenValidationResponse valResponse = null;


			//request.
			try
			{
				// Write data
				Stream postStream = request.GetRequestStream();
				postStream.Write(byteArray, 0, byteArray.Length);
				postStream.Close();

				// Send Request & Get Response
				//	var response = (HttpWebResponse)request.GetResponse();

				//using (StreamReader reader = new StreamReader(response.GetResponseStream()))
				//{
				//	// Get the Response Stream
				//	string json = reader.ReadLine();
				//	Console.WriteLine(json);

				//	// Retrieve and Return the Access Token
				//	Dictionary<string, object> x = (Dictionary<string, object>)JsonConvert.DeserializeObject(json);
				//	string accessToken = x["access_token"].ToString();
				//}
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					StreamReader reader = new StreamReader(response.GetResponseStream());
					string vals = reader.ReadToEnd();

					valResponse = JsonConvert.DeserializeObject<OAuthTokenValidationResponse>(vals);
					//foreach (string token in vals.Split('&'))
					//{
					//	tokens.Add(token.Substring(0, token.IndexOf("=")),
					//		token.Substring(token.IndexOf("=") + 1, token.Length - token.IndexOf("=") - 1));
					//}

					//string access_token = tokens["access_token"].ToString(); //will never expire
				}

				var assert = false;

			}
			catch (Exception e)
			{
				var abc = e.Message;
			}
			*/
		}
	}
}

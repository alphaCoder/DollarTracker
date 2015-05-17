using DollarTracker.Common;
using DollarTracker.Core.Managers;
using DollarTracker.Core.Services;
using DollarTracker.EF;
using DollarTracker.Web.Utils;
using Microsoft.Owin;
using Microsoft.Owin.Builder;
using Microsoft.Owin.Security.Google;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace DollarTracker.Web.ApiControllers
{
	public class ProfilePicUploadRequest
	{
		public string ImageBase64 { get; set; }
		public string UserId { get; set; }
	}

	public class UserController : DollarTrackerBaseController
	{
		private readonly IUserManager userManager;
		private readonly IAppSettingManager appSettingManager;
		public UserController(IUserManager userManager, IAppSettingManager appSettingManager)
		{
			this.userManager = userManager;
			this.appSettingManager = appSettingManager;
		}

		[Route("api/profilePic/{userId}")]
		public HttpResponseMessage Get(string userId)
		{
			HttpResponseMessage response = new HttpResponseMessage();
			var user = userManager.GetUserViaUserId(userId);
			if (user != null)
			{
				if (!string.IsNullOrEmpty(user.ProfilePic))
				{
					response.Content = new StreamContent(new MemoryStream(Convert.FromBase64String(user.ProfilePic)));
				}
				else
				{
					var defaultImg = @"~/Images/default_user_icon.jpg";
					response.Content = new StreamContent(new FileStream(System.Web.Hosting.HostingEnvironment.MapPath(defaultImg), FileMode.Open, FileAccess.Read));
				}
				response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
			}
			return response;
		}

		[Route("api/profilePic")]
		public bool Post(ProfilePicUploadRequest request)
		{
			if (request != null && !string.IsNullOrEmpty(request.UserId) && !string.IsNullOrEmpty(request.ImageBase64))
			{
				var user = userManager.GetUserViaUserId(request.UserId);
				if (user != null)
				{
					string imageBase64 = request.ImageBase64;
					string image = Regex.Replace(imageBase64, "data.*base64,", "", RegexOptions.IgnoreCase);
					user.ProfilePic = image;
					userManager.UpdateUser(user);
				}
			}
			return false;
		}

		//[Route("api/login")]
		//public void Post()

		[Route("api/authorize/{provider}")]
		public void Post(string provider,OAuthRequest request)
		{

			var options = new GoogleOAuth2AuthenticationOptions()
			{

			};
		//	var a = new GoogleOAuth2AuthenticationMiddleware(new OwinMiddleware(), new AppBuilder(), options);

			if (!string.IsNullOrEmpty(provider) && provider == "google")
			{
				var googleProvider = new GoogleAuthProvider(appSettingManager);
				var response = googleProvider.AuthenticateAsync(request);
			}
		}
	}
	


}

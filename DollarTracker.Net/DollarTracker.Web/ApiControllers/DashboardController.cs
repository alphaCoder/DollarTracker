using DollarTracker.Common;
using DollarTracker.Core.Models;
using DollarTracker.Core.Services;
using DollarTracker.Web.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DollarTracker.Web.ApiControllers
{
	[DTJwtApiAuthorization]
	public class DashboardController : DollarTrackerBaseController
    {
		private readonly IDashboardSummaryBuilder dashboardSummaryBuilder;
		public DashboardController(IDashboardSummaryBuilder dashboardSummaryBuilder)
		{
			this.dashboardSummaryBuilder = dashboardSummaryBuilder;
		}

		public DollarTrackerResponse<DashboardSummary> Get()
		{
			var response = new DollarTrackerResponse<DashboardSummary>();
			try
			{
				var summary = dashboardSummaryBuilder.Build(UserClaim.UserId);
				response.Data = summary;
				response.Success = true;
			}
			catch (Exception e)
			{
				response.Success = false;
				response.Message = "An Unknown error occured";
			}
			return response;
		}
    }
}

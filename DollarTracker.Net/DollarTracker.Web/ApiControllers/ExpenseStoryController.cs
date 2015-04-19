using DollarTracker.Common;
using DollarTracker.Core.Managers;
using DollarTracker.Core.Utils;
using DollarTracker.EF;
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
	public class ExpenseStoryController : DollarTrackerBaseController
    {
		private readonly IExpenseStoryManager expenseStoryManager;

		public ExpenseStoryController(IExpenseStoryManager esm, IAppSettingManager appSettingManager): base(appSettingManager)
		{
			this.expenseStoryManager = esm;
		}
		public DollarTrackerResponse<IEnumerable<ExpenseStory>> Get()
		{
			var response = new DollarTrackerResponse<IEnumerable<ExpenseStory>>();
			try
			{
				Guid userId = Guid.Parse("DB6B3AA4-8981-45D1-8E67-11B94FF0DF85");  //todo: will get from the user session
				var stories = expenseStoryManager.GetAllExpenseStories(userId.ToString());
				response.Data = stories;
				response.Success = true;
			}
			catch (Exception e)
			{
				//todo: log the server error
				response.Success = false;
				response.Message = "Unknown Server error";
			}
			return response;
		}

		[Route("api/addExpenseStory")]
		public DollarTrackerResponse<ExpenseStory> Post(ExpenseStory story)
		{
			var response = new DollarTrackerResponse<ExpenseStory>();
			try
			{
				Guid userId = Guid.Parse("DB6B3AA4-8981-45D1-8E67-11B94FF0DF85");  //todo: will get from the user session
				story.ExpenseStoryId = UniqueKeyGenerator.DatePrefixShortKey();
				story.CreatedBy = userId.ToString();
				story.CreatedUtcDt = DateTime.UtcNow;
				expenseStoryManager.AddExpenseStory(story); //todo: need to do validations--some design pattern
				response.Data = story;
			}
			catch (Exception e)
			{
				//todo: log the server error
				response.Success = false;
				response.Message = "Unknown Server error";
			}
			return response;
		}
    }
}

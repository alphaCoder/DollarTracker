using DollarTracker.Common;
using DollarTracker.Core.Managers;
using DollarTracker.Core.Services;
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
		private readonly ICollaboratorManager collaboratorManager;
		private readonly IExpenseStorySummaryBuilder expenseStorySummaryBuilder;
		public ExpenseStoryController(IExpenseStoryManager esm, ICollaboratorManager collaboratorManager, IExpenseStorySummaryBuilder expenseStorySummaryBuilder)
		{
			this.expenseStoryManager = esm;
			this.collaboratorManager = collaboratorManager;
			this.expenseStorySummaryBuilder = expenseStorySummaryBuilder;
		}
		public DollarTrackerResponse<IEnumerable<ExpenseStory>> Get()
		{
			var response = new DollarTrackerResponse<IEnumerable<ExpenseStory>>();
			try
			{
				var stories = expenseStoryManager.GetAllExpenseStories(UserClaim.UserId);
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
		public DollarTrackerResponse<ExpenseStorySummary> Post(ExpenseStory story)
		{
			var response = new DollarTrackerResponse<ExpenseStorySummary>();
			try
			{
				story.ExpenseStoryId = UniqueKeyGenerator.DatePrefixShortKey();
				story.CreatedBy = UserClaim.UserId;
				story.CreatedUtcDt = DateTime.UtcNow;
				expenseStoryManager.AddExpenseStory(story); //todo: need to do validations--some design pattern

				var collaborator = new Collaborator
				{
					CollaboratorId = Guid.NewGuid(),
					ExpenseStoryId = story.ExpenseStoryId,
					UserId = UserClaim.UserId,
					Status = true,
					CreatedUtcDt = DateTime.UtcNow
				};
				collaboratorManager.AddCollaborator(collaborator);
				response.Data = expenseStorySummaryBuilder.Build(story.ExpenseStoryId);
				response.Data.ExpenseStory = story;
			}
			catch (Exception e)
			{
				//todo: log the server error
				response.Success = false;
				response.Message = "Unknown Server error";
			}
			return response;
		}

		[Route("api/deleteExpenseStory/{storyId}")]
		[HttpDelete]
		public DollarTrackerResponse<IEnumerable<ExpenseStory>> Delete(string storyId)
		{
			var response = new DollarTrackerResponse<IEnumerable<ExpenseStory>>();
			try
			{
				expenseStoryManager.DeleteExpenseStory(storyId);
				response.Success = true;
				response.Data = expenseStoryManager.GetAllExpenseStories(UserClaim.UserId);
			}
			catch (Exception e)
			{
				response.Success = false;
				response.Message = "Unknown Server error";
			}
			return response;
		}
    }
}

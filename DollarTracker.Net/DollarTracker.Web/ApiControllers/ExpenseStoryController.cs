using DollarTracker.Core.Managers;
using DollarTracker.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DollarTracker.Web.ApiControllers
{
    public class ExpenseStoryController : ApiController
    {
		private readonly IExpenseStoryManager expenseStoryManager;

		public ExpenseStoryController(IExpenseStoryManager esm)
		{
			this.expenseStoryManager = esm;
		}
		public IEnumerable<ExpenseStory> Get()
		{
			Guid userId = Guid.Parse("DB6B3AA4-8981-45D1-8E67-11B94FF0DF85");  //todo: will get from the user session
			var stories = expenseStoryManager.GetAllExpenseStories(userId);
			return stories;
		}
    }
}

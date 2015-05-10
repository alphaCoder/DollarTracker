using DollarTracker.Core.Infrastructure;
using DollarTracker.Core.Repository;
using DollarTracker.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Managers
{
	public interface IExpenseStoryManager
	{
		void AddExpenseStory(ExpenseStory story);
		void UpdateExpenseStory(ExpenseStory story);
		IEnumerable<ExpenseStory> GetAllExpenseStories(string userId);
		IEnumerable<ExpenseStory> GetTopNExpenseStories(string userId, int n);
		IEnumerable<ExpenseStory> GetAllExpenseStoryWithInDtRange(string userId, DateTime startDt, DateTime endDt);
		ExpenseStory GetExpenseStory(string storyId);
		
		void DeleteExpenseStory(string storyId);
		void SaveExpenseStory();
	}
	public class ExpenseStoryManager : IExpenseStoryManager
	{
		private readonly IExpenseStoryRepository expenseStoryRepository;
        private readonly IUnitOfWork unitOfWork;
		private readonly IExpenseManager expenseManager;
		private readonly ICollaboratorManager collaboratorManager;
		public ExpenseStoryManager(IExpenseStoryRepository expenseStoryRepository, IUnitOfWork unitOfWork, 
			IExpenseManager expenseManager, ICollaboratorManager collaboratorManager)
        {
			this.expenseStoryRepository = expenseStoryRepository;
            this.unitOfWork = unitOfWork;
			this.expenseManager = expenseManager;
			this.collaboratorManager = collaboratorManager;
        }
		public void AddExpenseStory(ExpenseStory story)
		{
			if (!expenseStoryRepository.Any(x=>x.ExpenseStoryId == story.ExpenseStoryId))
			{
				expenseStoryRepository.Add(story);
				SaveExpenseStory();
			}
		}

		public void UpdateExpenseStory(ExpenseStory story)
		{
			var existingExpenseStory = expenseStoryRepository.Get(x => x.ExpenseStoryId == story.ExpenseStoryId);
			if (existingExpenseStory != null)
			{
				if (story.Income.HasValue)
				{
					existingExpenseStory.Income = story.Income;
				}
				if (story.Budget.HasValue)
				{
					existingExpenseStory.Budget = story.Budget;
				}
				expenseStoryRepository.Update(story);
				SaveExpenseStory();
			}
		}

		public IEnumerable<ExpenseStory> GetAllExpenseStories(string userId)
		{
			return expenseStoryRepository.GetMany(x => x.CreatedBy == userId);
		}

		public IEnumerable<ExpenseStory> GetTopNExpenseStories(string userId, int n)
		{
			return expenseStoryRepository.Get(x => x.CreatedBy == userId, take: n);
		}

		public IEnumerable<ExpenseStory> GetAllExpenseStoryWithInDtRange(string userId, DateTime startDt, DateTime endDt)
		{
			return expenseStoryRepository.Get(x => x.CreatedBy == userId && (x.StartDt >= startDt && x.EndDt <= endDt), orderBy: (z => z.OrderByDescending(y => y.StartDt)));
		}

		public ExpenseStory GetExpenseStory(string storyId)
		{
			return expenseStoryRepository.Get(e => e.ExpenseStoryId == storyId);
		}

		public void DeleteExpenseStory(string storyId)
		{
			expenseManager.DeleteAllExpenses(storyId);
			collaboratorManager.DeleteAllCollaborators(storyId);
			expenseStoryRepository.Delete(x => x.ExpenseStoryId == storyId);
			SaveExpenseStory();
		}

		public void SaveExpenseStory()
		{
			unitOfWork.Save();
		}
	}
}

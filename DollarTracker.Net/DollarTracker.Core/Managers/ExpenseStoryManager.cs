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
		IEnumerable<ExpenseStory> GetAllExpenseStories(Guid userId);
		IEnumerable<ExpenseStory> GetTopNExpenseStories(Guid userId, int n);
		IEnumerable<ExpenseStory> GetAllExpenseStoryWithInDtRange(Guid userId, DateTime startDt, DateTime endDt);
		void DeleteExpenseStory(string storyId);
		void SaveExpenseStory();
	}
	public class ExpenseStoryManager : IExpenseStoryManager
	{
		private readonly IExpenseStoryRepository expenseStoryRepository;
        private readonly IUnitOfWork unitOfWork;
		public ExpenseStoryManager(IExpenseStoryRepository expenseStoryRepository, IUnitOfWork unitOfWork)
        {
			this.expenseStoryRepository = expenseStoryRepository;
            this.unitOfWork = unitOfWork;
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
			//? todo need to determine if I need to update other fields as well.
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

		public IEnumerable<ExpenseStory> GetAllExpenseStories(Guid userId)
		{
			return expenseStoryRepository.GetMany(x => x.CreatedBy == userId);
		}

		public IEnumerable<ExpenseStory> GetTopNExpenseStories(Guid userId, int n)
		{
			return expenseStoryRepository.Get(x => x.CreatedBy == userId, take: n);
		}

		public IEnumerable<ExpenseStory> GetAllExpenseStoryWithInDtRange(Guid userId, DateTime startDt, DateTime endDt)
		{
			return expenseStoryRepository.Get(x => x.CreatedBy == userId && (x.StartDt >= startDt && x.EndDt <= endDt), orderBy: (z => z.OrderByDescending(y => y.StartDt)));
		}
		public void DeleteExpenseStory(string storyId)
		{
			expenseStoryRepository.Delete(x => x.ExpenseStoryId == storyId);
			SaveExpenseStory();
		}

		public void SaveExpenseStory()
		{
			unitOfWork.Save();
		}
	}
}

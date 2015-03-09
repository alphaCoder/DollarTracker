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
			expenseStoryRepository.Add(story);
			SaveExpenseStory();
		}

		public void UpdateExpenseStory(ExpenseStory story)
		{
			expenseStoryRepository.Update(story);
			SaveExpenseStory();
		}

		public IEnumerable<ExpenseStory> GetAllExpenseStories(Guid userId)
		{
			return expenseStoryRepository.GetMany(x => x.CreatedBy == userId);
		}

		public IEnumerable<ExpenseStory> GetTopNExpenseStories(Guid userId, int n)
		{
			return expenseStoryRepository.Get(x => x.CreatedBy == userId, take: n);
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

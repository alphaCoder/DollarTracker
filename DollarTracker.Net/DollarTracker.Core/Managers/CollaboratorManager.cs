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
	public interface ICollaboratorManager
	{
		void AddCollaborator(Collaborator collaborator);
		void UpdateCollaborator(Collaborator collaborator);
		IEnumerable<Collaborator> GetAllCollaborators(string expenseStoryId);
		Collaborator GetCollaborator(string userId, string expenseStoryId);
		void DeleteCollaborator(Guid collaboratorId);
		void DeleteAllCollaborators(string expenseStoryId);
		void SaveCollaborator();
	}
	public class CollaboratorManager : ICollaboratorManager
	{
		private readonly ICollaboratorRepository collaboratorRepository;
		private readonly IUnitOfWork unitOfWork;
		public CollaboratorManager(ICollaboratorRepository collaboratorRepository, IUnitOfWork unitOfWork)
		{
			this.collaboratorRepository = collaboratorRepository;
			this.unitOfWork = unitOfWork;
		}

		public void AddCollaborator(Collaborator collaborator)
		{
			if (!collaboratorRepository.Any(c => c.CollaboratorId == collaborator.CollaboratorId))
			{
				collaboratorRepository.Add(collaborator);
				SaveCollaborator();
			}
		}

		public void UpdateCollaborator(Collaborator collaborator)
		{
			var existingCollaborator = collaboratorRepository.GetById(collaborator.CollaboratorId);
			if (existingCollaborator != null)
			{
				existingCollaborator.Status = collaborator.Status;
				SaveCollaborator();
			}
		}

		public IEnumerable<Collaborator> GetAllCollaborators(string expenseStoryId)
		{
			return collaboratorRepository.GetMany(c => c.ExpenseStoryId == expenseStoryId);
		}

		public void DeleteCollaborator(Guid collaboratorId)
		{
			collaboratorRepository.Delete(x => x.CollaboratorId == collaboratorId);
			SaveCollaborator();
		}

		public Collaborator GetCollaborator(string userId, string expenseStoryId)
		{
			return collaboratorRepository.Get(c => c.UserId == userId && c.ExpenseStoryId == expenseStoryId);
		}

		public void DeleteAllCollaborators(string expenseStoryId)
		{
			collaboratorRepository.Delete(x => x.ExpenseStoryId == expenseStoryId);
			SaveCollaborator();
		}

		public void SaveCollaborator()
		{
			unitOfWork.Save();
		}

	}
}

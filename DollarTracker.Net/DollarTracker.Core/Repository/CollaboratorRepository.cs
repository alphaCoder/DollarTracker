using DollarTracker.Core.Infrastructure;
using DollarTracker.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Repository
{
	public interface ICollaboratorRepository : IRepository<Collaborator>
	{
	}

	public class CollaboratorRepository : RepositoryBase<Collaborator>, ICollaboratorRepository
	{
		public CollaboratorRepository(IDbFactory dbFactory) : base(dbFactory) { }
	}
}

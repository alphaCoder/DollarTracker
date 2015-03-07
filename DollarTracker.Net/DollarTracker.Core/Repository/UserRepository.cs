using DollarTracker.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DollarTracker.Core.Infrastructure;

namespace DollarTracker.Core.Repository
{
	public interface IUserRepository: IRepository<Users>
	{

	}
	public class UserRepository: RepositoryBase<Users>, IUserRepository
	{
		public UserRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}
	}
}

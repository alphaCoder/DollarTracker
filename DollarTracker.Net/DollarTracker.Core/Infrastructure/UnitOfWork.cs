using DollarTracker.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Infrastructure
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly IDbFactory dbFactory;
		private DollarTrackerEntities dataContext;

		public UnitOfWork(IDbFactory dbFactory)
		{
			this.dbFactory = dbFactory;
		}
		protected DollarTrackerEntities DataContext
		{
			get { return dataContext ?? (dataContext = dbFactory.Get()); }
		}
		public void Save()
		{
			DataContext.SaveChanges();
		}
	}
}

using DollarTracker.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Infrastructure
{
	public abstract class RepositoryBase<T> where T : class
	{
		private DollarTrackerEntities dataContext;
		private readonly IDbSet<T> dbset;
		protected RepositoryBase(IDbFactory databaseFactory)
		{
			DatabaseFactory = databaseFactory;
			dbset = DataContext.Set<T>();
		}

		protected IDbFactory DatabaseFactory
		{
			get;
			private set;
		}

		protected DollarTrackerEntities DataContext
		{
			get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
		}
		public virtual void Add(T entity)
		{
			dbset.Add(entity);
		}
		public virtual void Update(T entity)
		{
			dbset.Attach(entity);
			dataContext.Entry(entity).State = EntityState.Modified;
		}
		public virtual void Delete(T entity)
		{
			dbset.Remove(entity);
		}
		public virtual void Delete(Expression<Func<T, bool>> where)
		{
			IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
			foreach (T obj in objects)
				dbset.Remove(obj);
		}
		public virtual T GetById(Guid id)
		{
			return dbset.Find(id);
		}
		public virtual T GetById(string id)
		{
			return dbset.Find(id);
		}
		public virtual IEnumerable<T> GetAll()
		{
			return dbset.ToList();
		}

		public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> predicate)
		{
			return dbset.Where(predicate).ToList();
		}
	
		public T Get(Expression<Func<T, bool>> predicate)
		{
			return dbset.Where(predicate).FirstOrDefault<T>();
		}

		public virtual IEnumerable<T> Get(
			Expression<Func<T, bool>> filter = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			string includeProperties = "",
			int? take = null
			)
		{
			IQueryable<T> query = dbset;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			foreach (var includeProperty in includeProperties.Split
				(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			if (orderBy != null)
			{
				query = orderBy(query);
			}
			if (take.HasValue)
			{
				query = query.Take(take.Value);
			}
			return query.ToList();
		}
	}
}

using DollarTracker.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Infrastructure
{
	public class DbFactory : Disposable, IDbFactory
	{
		private DollarTrackerEntities context;
		private string connectionString;
		private bool isMockMode = false;
		public DbFactory()
		{
			try
			{
				connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DollarTrackDbConnection"].ConnectionString;
			}
			catch
			{
				isMockMode = true;
			}
		}
		public DollarTrackerEntities Get()
		{
			if (isMockMode)
			{
				return context ?? (context = new DollarTrackerEntities(Effort.EntityConnectionFactory.CreatePersistent("metadata=res://*/DollarTrackerModel.csdl|res://*/DollarTrackerModel.ssdl|res://*/DollarTrackerModel.msl")));
			}
			return context ?? (context = new DollarTrackerEntities(connectionString));
		}

		protected override void DisposeCore()
		{
			if (context != null)
			{
				context.Dispose();
			}
		}
	}
}

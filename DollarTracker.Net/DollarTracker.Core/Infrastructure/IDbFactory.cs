using DollarTracker.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Infrastructure
{
	public interface IDbFactory : IDisposable
	{
		DollarTrackerEntities Get();
	}
}

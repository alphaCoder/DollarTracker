using DollarTracker.Core.Infrastructure;
using DollarTracker.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Managers
{
	public interface IAppSettingManager
	{
		string Get(string key);
		IEnumerable<AppSetting> GetAll();
	}
	public class AppSettingManager : IAppSettingManager
	{
		private readonly IDbFactory dbFactory;
		public AppSettingManager(IDbFactory dbFactory)
		{
			this.dbFactory = dbFactory;
		}

		public string Get(string key)
		{
			var setting = dbFactory.Get().AppSetting.FirstOrDefault(app => app.Name == key);
			var settingValue = (setting != null) ? setting.Value : null;
			return settingValue;
		}


		public IEnumerable<AppSetting> GetAll()
		{
			return dbFactory.Get().AppSetting.Select(ap => ap).ToArray();
		}
	}
}

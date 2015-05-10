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
		string GetByName(string key);
		IEnumerable<AppSetting> GetAll();
	}
	public class AppSettingManager : IAppSettingManager
	{
		private readonly IDbFactory dbFactory;
		private static List<AppSetting> appSettings; 
		public AppSettingManager(IDbFactory dbFactory)
		{
			this.dbFactory = dbFactory;
			appSettings = dbFactory.Get().AppSetting.ToList();
		}

		public string GetByName(string key)
		{
			var setting = appSettings.FirstOrDefault(app => app.Name == key);
			var settingValue = (setting != null) ? setting.Value : null;
			return settingValue;
		}


		public IEnumerable<AppSetting> GetAll()
		{
			return appSettings;
		}
	}
}

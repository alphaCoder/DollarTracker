using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Common
{
	public class OAuthRequest
	{
		public string Code { get; set; }
		public string ClientId { get; set; }
		public string RedirectUri { get; set; }
	}
}

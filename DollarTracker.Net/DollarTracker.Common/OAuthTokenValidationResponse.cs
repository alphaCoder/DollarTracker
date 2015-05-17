using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Common
{
	public class OAuthTokenValidationResponse
	{
		public string Access_token { get; set; }
		public string Token_type { get; set; }
		public int Expires_in { get; set; }
		public string Id_token { get; set; }
	}
}

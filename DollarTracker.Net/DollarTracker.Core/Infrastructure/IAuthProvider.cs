using DollarTracker.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Infrastructure
{
	public interface IAuthProvider
	{
		Task<OAuthTokenValidationResponse> AuthenticateAsync(OAuthRequest request);
	}
}

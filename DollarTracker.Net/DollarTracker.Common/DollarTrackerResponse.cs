using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Common
{
    public class DollarTrackerResponse<T> : BasicResponse
    {
		public T Data { get; set; }
		public Dictionary<string, object> AdditionalData { get; set; }
    }
}

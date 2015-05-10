using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Common
{
    public class DollarTrackerResponse<T>
    {
		public bool Success { get; set; }
		public string Message { get; set; }
		public T Data { get; set; }
		public Dictionary<string, object> AdditionalData { get; set; }
    }
}

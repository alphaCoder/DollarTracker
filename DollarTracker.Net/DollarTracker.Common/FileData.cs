using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Common
{
	public class FileData<T>
	{
		public DollarTrackerFileType FileType { get; set; }
		public T Data { get; set; }
		public Guid FileId { get; set; }
		public DateTime CreateDtUtc { get; set; }
		public string Filename { get; set; }
	}
}

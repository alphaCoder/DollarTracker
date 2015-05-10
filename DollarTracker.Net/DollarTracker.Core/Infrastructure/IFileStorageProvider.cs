using DollarTracker.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Infrastructure
{
	public interface IFileStorageProvider
	{
		BasicResponse SaveString(string userId, FileData<string> file);
		DollarTrackerResponse<FileData<string>> GetFileDataAsString(string userId, Guid fileId);
	}
}

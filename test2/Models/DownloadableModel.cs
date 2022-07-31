using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test2.Models
{
	public class DownloadableModel
	{
		public string FullName { get; set; }
		public string Name { get; set; }
		public DateTime DateModified { get; set; }
		public long Size { get; set; }
		
		public enum InternalTypeEnum
		{
			FILE,
			FOLDER,
		};

		public InternalTypeEnum InternalType { get; set; }
	}
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test2.Models;

namespace test2.Controllers
{
	public class BrowseController : Controller
	{
		public IActionResult Index(string path = "D:\\", string search = "*")
		{
			string full = path;

			DownloadableModel[] DownloadableDirectories = new DownloadableModel[0];
			DownloadableModel[] DownloadableFiles = new DownloadableModel[0];

			// TODO : Throw error.

			if (Directory.Exists(full))
			{
				DirectoryInfo dir = new DirectoryInfo(full);
				DirectoryInfo[] directories = dir.GetDirectories(search);
				FileInfo[] files = dir.GetFiles(search);

				DownloadableDirectories = new DownloadableModel[directories.Length];
				DownloadableFiles = new DownloadableModel[files.Length];

				for (int i = 0; i < directories.Length; i++)
				{
					DownloadableDirectories[i] = new DownloadableModel();
					DownloadableDirectories[i].FullName = directories[i].FullName;
					DownloadableDirectories[i].Name = directories[i].Name;
					DownloadableDirectories[i].Size = 0;
					DownloadableDirectories[i].InternalType = DownloadableModel.InternalTypeEnum.FOLDER;
				}

				for (int i = 0; i < files.Length; i++)
				{
					DownloadableFiles[i] = new DownloadableModel();
					DownloadableFiles[i].FullName = files[i].FullName;
					DownloadableFiles[i].Name = files[i].Name;
					DownloadableFiles[i].Size = files[i].Length;
					DownloadableFiles[i].DateModified = files[i].LastWriteTime;
					DownloadableFiles[i].InternalType = DownloadableModel.InternalTypeEnum.FILE;
				}
			}

			ViewData["Directories"] = DownloadableDirectories;
			ViewData["Files"] = DownloadableFiles;
			return View();
		}
	}
}

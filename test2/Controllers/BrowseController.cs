using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test2.Models;
using System.Web;
using System.Net;

namespace test2.Controllers
{
	public class BrowseController : Controller
	{
		public IActionResult Index(string path = "D:\\shared", string search = "*")
		{
			string full = path;

			DownloadableModel[] DownloadableDirectories = new DownloadableModel[0];
			DownloadableModel[] DownloadableFiles = new DownloadableModel[0];

			// TODO : Throw error.

			if (full.StartsWith("D:\\shared") && Directory.Exists(full))
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

		public IActionResult Preview(string path)
		{
			if (path.StartsWith("D:\\shared") && System.IO.File.Exists(path))
			{
				PrepareFile(path);
				FileInfo fileinfo = new FileInfo(path);
				
				DownloadableModel file = new DownloadableModel();
				file.FullName = fileinfo.FullName;
				file.Name = fileinfo.Name;
				file.Size = fileinfo.Length;
				file.DateModified = fileinfo.LastWriteTime;
				file.InternalType = DownloadableModel.InternalTypeEnum.FILE;

				ViewData["File"] = file;
				ViewData["FileType"] = MimeTypes.GetMimeType(path);
				return View();
			}
			return NotFound();
		}

		public void PrepareFile(string path)
		{
			// TODO : check if old files in folder wwwroot\media have expired and delete.
			System.IO.File.Copy(path, @"wwwroot\media");
		}
	}
}

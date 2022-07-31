using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test2.Controllers
{
	public class BrowseController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

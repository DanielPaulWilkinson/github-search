using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace github_search.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public ActionResult Error404()
        {
            return View();
        }
    }
}
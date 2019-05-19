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
        protected void Throw404()
        {
            throw new HttpException(404, "not found");
        }
    }
}
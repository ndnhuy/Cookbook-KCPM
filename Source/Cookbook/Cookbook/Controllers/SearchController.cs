using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cookbook.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Index(string id)
        {
            dynamic model = new System.Dynamic.ExpandoObject();
            model.Content = id;
            return View(model);
        }
    }
}

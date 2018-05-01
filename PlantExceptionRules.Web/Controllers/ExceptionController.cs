using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantExceptionRules.Web.Controllers
{
    public class ExceptionController : Controller
    {
        // GET: Exception
        public ActionResult Index()
        {
            //  return View();
            return View("ExceptionList");
        }

        [HttpPost]
        //public ActionResult Upload()
        //{
        //    return View("ExceptionList");
        //}
        #region Exception

        public ActionResult ExceptionList()
        {
            return View();
        }     
        #endregion
    }
}
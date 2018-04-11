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

        //public ActionResult DeviationListUpdate(int RecId, char status = '2')
        //{
        //    ResultViewModel result = new ResultViewModel();
        //    //  bool isSuccess = false;

        //    try
        //    {
        //        //status = '3' : Accept on Deviation
        //        //status = '0' : Reject on Deviation
        //        result = new VistaParser().AcceptOrReject(status, RecId);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Message = ex.Message;
        //    }

        //    return Json(new { isSuccess = result.Success, message = result.Message }, JsonRequestBehavior.AllowGet);
        //}

        #endregion
    }
}
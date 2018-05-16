using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
//using System.IO;
//using System.Web.UI;
//using System.Web.UI.WebControls;
using PlantExceptionRules.Data;
using PlantExceptionRules.Model;
using PlantExceptionRules.Model.ViewModel;

namespace PlantExceptionRules.Web.Controllers
{
    public class GridController : Controller
    {

        public ActionResult GetExceptionList(DataGridoption dataoptions)
        {
          DataSearch<ProdExceptions> ds = new DataSearch<ProdExceptions>();
            try
            {
                 ds = new ExceptionsData().GetList(dataoptions);
               
            }
            catch (Exception ex)
            {
                ds.Message = ex.Message.ToString();
                ds.total = 0;
            }

            return Json(new { items = ds.items, total = ds.total }, JsonRequestBehavior.AllowGet);
        }   

    }
}
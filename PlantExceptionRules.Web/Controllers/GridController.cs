﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

using PlantExceptionRules.Data;
using PlantExceptionRules.Model;
using PlantExceptionRules.Model.ViewModel;

namespace PlantExceptionRules.Web.Controllers
{
    public class GridController : Controller
    {
        // GET: Grid
        //public ActionResult Index()
        //{
        //    return View();
        //}
        
   
        public ActionResult GetExceptionList(DataGridoption option)
        {
          DataSearch<ProdExceptions> ds = new DataSearch<ProdExceptions>();
            try
            {
                 ds = new ExceptionsData().GetList(option);
               
                //return Json(ds, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ds.Message = ex.Message.ToString();
                ds.total = 0;
                //throw ex;
            }

            // return Json(ds, JsonRequestBehavior.AllowGet);
            return Json(new { items = ds.items, total = ds.total }, JsonRequestBehavior.AllowGet);
        }
    }
}
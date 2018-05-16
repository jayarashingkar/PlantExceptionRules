using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlantExceptionRules.Model.ViewModel;
using PlantExceptionRules.Model;
using PlantExceptionRules.Data;

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
        #region Exception

        public ActionResult ExceptionList()
        {
            return View();
        }
        #endregion


        [HttpPost]
        public ActionResult ExportToExcel(string Spec, string Alloy, string Temper ,string Plant)
        {
         
            DataGridoption ExportDataFilter = new DataGridoption();

            string SearchBy = "";

            if (!string.IsNullOrEmpty(Spec))
            {
                SearchBy = SearchBy + ";" + "Spec:" + Spec;
            }
            if (!string.IsNullOrEmpty(Alloy))
            {
                SearchBy = SearchBy + ";" + "Alloy:" + Alloy;
            }
            if (!string.IsNullOrEmpty(Temper))
            {
                SearchBy = SearchBy + ";" + "Temper:" + Temper;
            }
            if (!string.IsNullOrEmpty(Plant))
            {
                SearchBy = SearchBy + ";" + "Plant:" + Plant;
            }

            ExportDataFilter.Screen = "ExceptionList";
            ExportDataFilter.filterBy = "all";
            ExportDataFilter.pageIndex = 0;
            ExportDataFilter.pageSize = 10000;
            ExportDataFilter.searchBy = SearchBy;

            List<ProdExceptions> lstException = new List<ProdExceptions>();
            DataSearch<ProdExceptions> ds = null;

            try
            {
                ds = new ExceptionsData().GetList(ExportDataFilter);

                if (ds != null && ds.items != null && ds.items.Count > 0)
                {
                    lstException = ds.items;
                    string fileName = "ExceptionList" + "_" + DateTime.Now.ToString().Replace(" ", "").Replace("-", "").Replace(":", "");
                    GetExcelFile<ProdExceptions>(lstException, fileName);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("ExceptionList");
        }
        public ActionResult GetExcelFile<T>(IEnumerable<T> dataSource, string fileName)
        {
            string returnMessage = "Export Success!";
            try
            {
                GridView gridview = new GridView();
                gridview.DataSource = dataSource;
                gridview.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/ms-excel";
                Response.AppendHeader("content-disposition", "attachment; filename=" + fileName + ".xls");
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gridview.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                returnMessage = ex.Message.ToString();
            }
           
            return View("ExceptionList");
        }
          

    }
}
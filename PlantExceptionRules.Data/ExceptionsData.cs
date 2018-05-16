using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using PlantExceptionRules.Model;
using PlantExceptionRules.Model.ViewModel;

namespace PlantExceptionRules.Data
{
    public class ExceptionsData : DatabaseOperations
    {
        public DataSearch<ProdExceptions> GetList(DataGridoption option)
        {
            string message = "success";
            List<ProdExceptions> lstExceptions = new List<ProdExceptions>();
            try
            {
                StoredProcedureName = "PlantExceptionRulesGetExceptionsList";

                this.ConnectionString = ConfigurationManager.AppSettings["DBConnection"].ToString();

                SQLParameters = new Dictionary<string, object>();

                SQLParameters["CurrentPage"] = option.pageIndex;
                SQLParameters["NoOfRecords"] = option.pageSize;
                SQLParameters["sortBy"] = option.sortBy;
                SQLParameters["sortDirection"] = option.sortDirection;

                AddSearchFilter(option, SQLParameters);

                //   ProdExceptions exception = null;
                DataTable result = Execute();

                int j;
                foreach (DataRow row in result.Rows)
                {
                    ProdExceptions exception = new ProdExceptions();
                    j = 0;
                    exception.Total = Convert.ToInt32(row[j]); j++;
                    exception.ExceptionID = Convert.ToInt32(row[j]); j++;
                    exception.Spec = Convert.ToString(row[j]); j++;
                    exception.SpecRev = Convert.ToString(row[j]); j++;
                    exception.Alloy = Convert.ToString(row[j]); j++;
                    exception.Temper = Convert.ToString(row[j]); j++;
                    exception.MinSecThick = Convert.ToDecimal(row[j]); j++;
                    exception.MaxSecThick = Convert.ToDecimal(row[j]); j++;
                    exception.CustPart = Convert.ToString(row[j]); j++;
                    exception.UACPart = Convert.ToDecimal(row[j]); j++;
                    exception.Plant = Convert.ToInt32(row[j]); j++;
                    exception.Severity = Convert.ToInt32(row[j]); j++;
                    exception.Note = Convert.ToString(row[j]); j++;
                    exception.Approval = Convert.ToChar(row[j]); j++;
                    exception.Enabled = Convert.ToInt16(row[j]); j++;
                    if (exception.Enabled == 1)
                        exception.RuleTurnedOn = "Yes";
                    else
                        exception.RuleTurnedOn = "No";
                    exception.PlantDescription = Convert.ToString(row[j]); j++;
                    lstExceptions.Add(exception);
                }

               
            }
            
            catch(Exception ex)
            {
                // throw ex;
                message = ex.ToString();
                lstExceptions[0].Total = 0;
            }
           
            DataSearch<ProdExceptions> ds = new DataSearch<ProdExceptions>
            {
                Message = message,
                items = lstExceptions,
                total = (lstExceptions != null && lstExceptions.Count > 0) ? lstExceptions[0].Total : 0
            };

            return ds;

        }

        private static void AddSearchFilter(DataGridoption option, Dictionary<string, object> SQLParameters)
        {
            #region searchby
            if (option != null && !string.IsNullOrEmpty(option.searchBy))
            {
                string[] searchSplit = option.searchBy.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (searchSplit != null && searchSplit.Length > 0)
                {
                    foreach (var item in searchSplit)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            var itemSplit = item.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                            if (itemSplit != null && itemSplit.Length == 2)
                            {
                                if (!string.IsNullOrEmpty(itemSplit[1]) && itemSplit[1] != "-1")
                                {
                                    if (itemSplit[0] == "Spec")
                                        SQLParameters["Spec"] = itemSplit[1];
                                    if (itemSplit[0] == "Alloy")
                                        SQLParameters["Alloy"] = itemSplit[1];
                                    if (itemSplit[0] == "Temper")
                                        SQLParameters["Temper"] = itemSplit[1];
                                    if (itemSplit[0] == "UACPart")
                                        SQLParameters["UACPart"] = itemSplit[1];
                                    if (itemSplit[0] == "CustPart")
                                        SQLParameters["CustPart"] = itemSplit[1];
                                    if (itemSplit[0] == "Plant")
                                        SQLParameters["Plant"] = itemSplit[1];
                                    if (itemSplit[0] == "Severity")
                                        SQLParameters["Severity"] = itemSplit[1];
                                }
                                //  lstSqlParameter.Add(new SqlParameter("@" + itemSplit[0], itemSplit[1]));
                            }
                        }
                    }
                }
            }
            #endregion

        }
   
    }
}

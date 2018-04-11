using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantExceptionRules.Model
{
    public class ProdExceptions
    {
        public int ExceptionID { get; set; }
        public string Spec { get; set; }
        public string SpecRev { get; set; }
        public string Alloy { get; set; }
        public string Temper { get; set; }
        public decimal MinSecThick { get; set; }
        public decimal MaxSecThick { get; set; }
        public string CustPart { get; set; }
        public decimal UACPart { get; set; }
        public int Plant { get; set; }
        public int Severity { get; set; }
        public string Note { get; set; }
        public char Approval { get; set; }
        public int Enabled { get; set; }
        public int Total { get; set; }
    }
}

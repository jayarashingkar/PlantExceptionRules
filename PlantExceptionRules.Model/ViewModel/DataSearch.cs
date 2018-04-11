
using System.Collections.Generic;

namespace PlantExceptionRules.Model.ViewModel
{
    public class DataSearch<T> where T : class
    {
        public List<T> items { get; set; }
        public int total { get; set; }
        public string Message { get; set; }
    }
}

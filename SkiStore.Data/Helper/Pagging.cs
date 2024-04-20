using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiStore.Data.Helper
{
    public  class Pagging<T> where T : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public List<T> Data { get; set; }= new List<T>();
    }
}

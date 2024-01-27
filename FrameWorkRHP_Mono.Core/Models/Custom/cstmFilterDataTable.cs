using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FrameWorkRHP_Mono.Core.Models.Custom
{
    public class cstmFilterDataTable
    {
        public int draw { get; set; }
        public List<columnsData> columns { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public searchData search { get; set; }
        public List<orderData> order { get; set; }  
    }

    public class columnsData
    {
        public String data { get; set; }
        public String name { get; set; }
        public Boolean searchable { get; set; }
        public Boolean orderable { get; set; }
        public searchData search { get; set; }
    }

    public class searchData
    {
        public String value { get; set; }
        public String regex { get; set; }
    }

    public class orderData
    {
        public int column { get; set; }
        public String dir { get; set; }
    }
     
}

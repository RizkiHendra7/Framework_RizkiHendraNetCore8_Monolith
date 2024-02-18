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

        public string paggingQuery
        {
            get
            { 
                //return " where no >= " + (start + 1).ToString() + " and no <= " + (start + length).ToString() + "";                     
                return " \r\n OFFSET " + start.ToString() + " \r\nLIMIT " + (start + length).ToString() + "";                     
            }
            set { }
        }

        public string getPaggingQuery
        {
            get
            {
                return "\r\n ,row_number() OVER () AS serialNumber, COUNT(*) OVER () AS TOTALDATA ";
            }
            set { }
        } 
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
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class orderData
    {
        public int column { get; set; }
        public String dir { get; set; }
    } 

}

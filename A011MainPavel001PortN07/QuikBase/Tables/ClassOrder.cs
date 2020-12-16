using QuikSharp.DataStructures;
using QuikSharp.DataStructures.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A011MainPavel001PortN07
{
    public class ClassOrder
    {
        public long Id { get; set; }
        public double TransID { get; set; }

        public string Account { get; set; }

        public double Balance { get; set; }

        public string ClassCode { get; set; }

        public QuikDateTime Datetime { get; set; }

        public double ExtOrderStatus { get; set; }

        public OrderTradeFlags Flags { get; set; }

        public double Linkedorder { get; set; }
        public Operation Operation { get; set; }
        public double OrderNum { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public string SecCode { get; set; }

        public State State { get; set; }
     
        public decimal Value { get; set; }




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A011MainPavel001PortN07
{
    public class ClassMoneyLimitEx
    {
        public long Id { get; set; }

        public double CurrentBal { get; set; }

        public double CurrentLimit { get; set; }

        public double Locked { get; set; }

        public double OpenBal { get; set; }

        public double OpenLimit { get; set; }

        public double OrdersCollateral { get; set; }

        public double PositionsCollateral { get; set; }

        public string Tag { get; set; }

        public double WaPositionPrice { get; set; }

        public double LimitKind { get; set; }



    }
}

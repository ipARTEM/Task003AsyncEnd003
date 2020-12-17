using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A011MainPavel002PortN07
{
    public class ClassParam
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string TimeSave { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public int NumberOfBuy { get; set; }
        public int NumberOfSell { get; set; }

        public int TotalDemand { get; set; }

        public int TotalOffer { get; set; }

        public int OpenPositions { get; set; }

        public ICollection<ClassAllTrade> ClassAllTrade { get; set; }

        public ClassParam()
        {
            ClassAllTrade = new List<ClassAllTrade>();
        }

    }
}

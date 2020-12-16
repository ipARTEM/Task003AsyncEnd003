
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A011MainPavel001PortN07.QuikBase
{
    public class QuikArtemDB :DbContext
    {
        public QuikArtemDB ()
            : base("name=QuikArtem")
        {

        }

        public virtual DbSet<ClassAllTrade> ClassAllTrades { get; set; }

        public virtual DbSet<ClassParam> ClassParams { get; set; }

        public virtual DbSet<ClassQuote> ClassQuotes { get; set; }

        public virtual DbSet<ClassMoneyLimitEx> ClassMoneyLimitExs { get; set; }

        public virtual DbSet<ClassDepoLimitEx> ClassDepoLimitExs { get; set; }

        public virtual DbSet<ClassOrder> ClassOrders { get; set; }


    }
}

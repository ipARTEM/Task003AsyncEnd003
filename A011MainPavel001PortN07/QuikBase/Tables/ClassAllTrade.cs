using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task003AsyncEnd003
{
    public class ClassAllTrade
    {
        public long Id { get; set; }

        [Required]
           //уникальное имя
        public long TradeNum { get; set; }
        public string Name { get; set; }
    
        public string TimeSave { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }
        public decimal Price { get; set; }
        public int Volume { get; set; }

        public string Flag { get; set; }

        public int OpenInterest { get; set; }

        public double DeltaTime { get; set; }


        public ICollection<ClassParam> ClassParam { get; set; }           // для связи многие к многим
        public ClassAllTrade()
        {
            ClassParam = new List<ClassParam>();
        }

    }
}

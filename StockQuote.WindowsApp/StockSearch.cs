using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockQuote.Model
{
    public class StockSearch
    {
        public String symbol { get;  }
        public String Date { get;  }
        public decimal Open { get;  }
        public decimal Close { get;  }
        public decimal High { get;  }
        public decimal Low { get;  }
        public decimal Volume { get;  }
        public decimal Adj_Close { get; }

        public StockSearch(String symbol, String Date, decimal Open, 
            decimal High, decimal Low, decimal Close, decimal Volume, decimal Adj_Close) {
            this.symbol = symbol;
            this.Date = Date;
            this.Open = Open;
            this.Close = Close;
            this.High = High;
            this.Low = Low;
            this.Volume = Volume;
            this.Adj_Close = Adj_Close;
        }
    }
}

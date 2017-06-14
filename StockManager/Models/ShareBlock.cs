using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Models
{
    public class ShareBlock
    {
        public int Id { get; set; }

        public int StockId { get; set; }

        public int NumberOfShares { get; set; }

        public decimal BuyPrice { get; set; }

        public decimal SellPrice { get; set; }
    }
}

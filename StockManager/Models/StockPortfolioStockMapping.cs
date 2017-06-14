using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Models
{
    public class StockPortfolioStockMapping
    {
        public int StockPortfolioId { get; set; }
        public int StockId { get; set; }

        // Navigation properties:
        public StockPortfolio StockPortfolio { get; set; }
        public Stock Stock { get; set; }
    }
}

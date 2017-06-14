using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Models
{
    public class Stock
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ShareBlock> ShareBlocks { get; set; }

        public virtual ICollection<StockPortfolioStockMapping> SpsMappings { get; set; }

        public Stock()
        {
            ShareBlocks = new List<ShareBlock>();
            SpsMappings = new List<StockPortfolioStockMapping>();
        }
    }
}

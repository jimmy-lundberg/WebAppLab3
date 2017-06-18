using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Models
{
    public class Stock
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [NotMapped]
        public decimal MarketPrice
        {
            get
            {
                marketPrice += rng.Next(-10, 10) + (decimal)(rng.NextDouble() * 2);
                return Math.Round(marketPrice, 2);
            }
        }

        public virtual ICollection<ShareBlock> ShareBlocks { get; set; }

        public virtual ICollection<StockPortfolioStockMapping> SpsMappings { get; set; }

        private Random rng;
        private decimal marketPrice;
        private Stopwatch stopwatch;
        
        public Stock()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            ShareBlocks = new List<ShareBlock>();
            SpsMappings = new List<StockPortfolioStockMapping>();
            marketPrice = 350; //rng.Next(50, 500);
            stopwatch.Stop();
            rng = new Random((int)stopwatch.ElapsedTicks);
        }

        //public decimal GetMarketPrice()
        //{
        //    marketPrice += rng.Next(-10, 10) + (decimal)(rng.NextDouble()*2);
        //    return Math.Round(marketPrice, 2);
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Models
{
    public class StockPortfolio
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        public string Name { get; set; }

        // Navigation property.
        public ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<StockPortfolioStockMapping> SpsMappings { get; set; }
    }
}

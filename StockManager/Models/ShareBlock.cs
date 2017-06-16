using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Models
{
    public class ShareBlock
    {
        // Not needed
        // public int Id { get; set; }

        public int ParentStockId { get; set; }
                                                // Set StockId + PortfolioId as Primary Key instead
        public int OwnerPortfolioId { get; set; }

        public int NumberOfShares { get; set; }

        // Navigation properties:
        public Stock ParentStock { get; set; }

        public StockPortfolio OwnerPortfolio { get; set; }
    }
}

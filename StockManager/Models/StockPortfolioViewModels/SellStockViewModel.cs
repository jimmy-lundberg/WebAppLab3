using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StockManager.Models.StockPortfolioViewModels
{
    public class SellStockViewModel
    {
        public int StockId { get; set; }
        
        public int NumberOfShares { get; set; }
        
        public IEnumerable<SelectListItem> OwnedStockList { get; set; }
    }
}

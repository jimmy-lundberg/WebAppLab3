using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StockManager.Models.StockPortfolioViewModels
{
    public class SellStockViewModel
    {
        [Required]
        public int StockId { get; set; }
        
        [Required]
        public int NumberOfShares { get; set; }
        
        public IEnumerable<SelectListItem> OwnedStockList { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StockManager.Models.StockPortfolioViewModels
{
    public class BuyStockViewModel
    {
        public int StockId { get; set; }

        public int NumberOfShares { get; set; }

        //public int PortfolioId { get; set; }

        public IEnumerable<SelectListItem> StockList { get; set; }
    }
}

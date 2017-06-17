using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockManager.Data;

namespace StockManager.Components
{
    public class StockInfoRow : ViewComponent
    {
        private ApplicationDbContext _context;

        public StockInfoRow(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int portfolioId, int stockId)
        {
            var shareBlock = _context.ShareBlocks.Where(sb => sb.OwnerPortfolioId == portfolioId && sb.ParentStockId == stockId).Single();
            var stock = _context.Stocks.Where(s => s.Id == stockId).Single();
            var marketPrice = stock.MarketPrice;

            ViewBag.StockName = stock.Name;
            ViewBag.SharePrice = marketPrice;
            ViewBag.NumberOfShares = shareBlock.NumberOfShares;
            ViewBag.MarketValue = shareBlock.NumberOfShares * marketPrice;

            return View();
        }
    }
}

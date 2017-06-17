using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using StockManager.Data;
using StockManager.Models;
using StockManager.Models.StockPortfolioViewModels;
using Newtonsoft.Json;

namespace StockManager.Controllers
{
    [Authorize]
    public class StockPortfoliosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StockPortfoliosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StockPortfolios
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StockPortfolios.Include(s => s.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: StockPortfolios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockPortfolio = await _context.StockPortfolios
                .Include(s => s.ApplicationUser)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stockPortfolio == null)
            {
                return NotFound();
            }

            return View(stockPortfolio);
        }

        // GET: StockPortfolios/Create
        public IActionResult Create()
        {
            //ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: StockPortfolios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApplicationUserId,Name")] StockPortfolio stockPortfolio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockPortfolio);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", stockPortfolio.ApplicationUserId);
            return View(stockPortfolio);
        }

        // GET: StockPortfolios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockPortfolio = await _context.StockPortfolios.SingleOrDefaultAsync(m => m.Id == id);
            if (stockPortfolio == null)
            {
                return NotFound();
            }
            //ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", stockPortfolio.ApplicationUserId);
            return View(stockPortfolio);
        }

        // POST: StockPortfolios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ApplicationUserId,Name")] StockPortfolio stockPortfolio)
        {
            if (id != stockPortfolio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockPortfolio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockPortfolioExists(stockPortfolio.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            //ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", stockPortfolio.ApplicationUserId);
            return View(stockPortfolio);
        }

        // GET: StockPortfolios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockPortfolio = await _context.StockPortfolios
                .Include(s => s.ApplicationUser)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (stockPortfolio == null)
            {
                return NotFound();
            }

            return View(stockPortfolio);
        }

        // POST: StockPortfolios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockPortfolio = await _context.StockPortfolios.SingleOrDefaultAsync(m => m.Id == id);
            _context.StockPortfolios.Remove(stockPortfolio);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool StockPortfolioExists(int id)
        {
            return _context.StockPortfolios.Any(e => e.Id == id);
        }

        // GET: StockPortfolios/Content/5
        public async Task<IActionResult> Content(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioStocks = _context.SpsMappings.Where(spsm => spsm.StockPortfolioId == id).Select(spsm => spsm.Stock);

            if (portfolioStocks == null)
            {
                return NotFound();
            }

            ViewBag.PortfolioId = id;
            ViewBag.PortfolioName = _context.StockPortfolios.Where(sp => sp.Id == id).Select(sp => sp.Name).Single();

            return View(await portfolioStocks.ToListAsync());
        }

        // GET: StockPortfolios/BuyStock/5
        public async Task<IActionResult> BuyStock(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockList = await _context.Stocks.Select(s => new SelectListItem() { Text = s.Name, Value = s.Id.ToString() }).ToListAsync();

            var buyStockViewModel = new BuyStockViewModel() { StockList = stockList };

            ViewBag.PortfolioId = id;
            TempData["StockList"] = JsonConvert.SerializeObject(stockList);

            return View(buyStockViewModel);
        }

        // POST: StockPortfolios/BuyStock/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyStock(int id, [Bind("StockId", "NumberOfShares")] BuyStockViewModel buyStockViewModel)
        {
            if (ModelState.IsValid)
            {
                var portfolioId = id;
                var stockId = buyStockViewModel.StockId;

                // Stock/Stock portfolio is loaded into memory from database.
                var stockPortfolio = _context.StockPortfolios.Where(sp => sp.Id == portfolioId).Single();
                var stock = _context.Stocks.Where(s => s.Id == stockId).Single();

                var shareBlock = GetShareBlock(portfolioId, stockId);

                if (shareBlock == null)
                {
                    shareBlock = new ShareBlock
                    {
                        NumberOfShares = buyStockViewModel.NumberOfShares,
                        ParentStockId = buyStockViewModel.StockId,
                        OwnerPortfolioId = id,
                        ParentStock = stock,
                        OwnerPortfolio = stockPortfolio
                    };

                    var SpsMapping = new StockPortfolioStockMapping { StockId = stockId, StockPortfolioId = portfolioId };

                    stock.ShareBlocks.Add(shareBlock);
                    stock.SpsMappings.Add(SpsMapping);
                    stockPortfolio.SpsMappings.Add(SpsMapping);

                    _context.Update(stock);
                    _context.Update(stockPortfolio);
                    _context.Add(shareBlock);
                }
                else
                {
                    shareBlock.NumberOfShares += buyStockViewModel.NumberOfShares;
                    _context.Update(shareBlock);
                }

                try
                {
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    // Returns NotFound() if the stock portfolio has been deleted from the database since it was loaded into memory.
                    if (!StockPortfolioExists(stockPortfolio.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Content", new { id = id });
            }

            buyStockViewModel.StockList = JsonConvert.DeserializeObject<IEnumerable<SelectListItem>>(TempData["StockList"].ToString());
            TempData.Keep("StockList");

            return View(buyStockViewModel);
        }

        private ShareBlock GetShareBlock(int portfolioId, int stockId)
        {
            return _context.ShareBlocks.Where(sb => sb.OwnerPortfolioId == portfolioId && sb.ParentStockId == stockId).SingleOrDefault();
        }

        // GET: StockPortfolios/SellStock
        public async Task<IActionResult> SellStock(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownedStocks =
                from allStocks in _context.Stocks
                join allSpsMappings in _context.SpsMappings on allStocks.Id equals allSpsMappings.StockId
                where allSpsMappings.StockPortfolioId == id
                select allStocks;

            var ownedStockList = await ownedStocks.Select(s => new SelectListItem() { Text = s.Name, Value = s.Id.ToString() }).ToListAsync();

            var sellStockViewModel = new SellStockViewModel() { OwnedStockList = ownedStockList };

            ViewBag.PortfolioId = id;
            TempData["OwnedStockList"] = JsonConvert.SerializeObject(ownedStockList);

            return View(sellStockViewModel);
        }

        // POST: StockPortfolios/BuyStock/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SellStock(int id, [Bind("StockId","NumberOfShares", "OwnedStockList")] SellStockViewModel sellStockViewModel)
        {
            var portfolioId = id;
            var stockId = sellStockViewModel.StockId;

            var shareBlock = GetShareBlock(portfolioId, stockId);
            
            if (shareBlock.NumberOfShares < sellStockViewModel.NumberOfShares)
            {
                ModelState.AddModelError("NumberOfShares", "Transaction failed! You don't have that many shares to sell! Please select a smaller number.");
            }

            if (ModelState.IsValid)
            {              

                var stockPortfolio = _context.StockPortfolios.Where(sp => sp.Id == portfolioId).Include("SpsMappings").Single();
                var stock = _context.Stocks.Where(s => s.Id == stockId).Single();

                if (shareBlock.NumberOfShares == sellStockViewModel.NumberOfShares)
                {
                    stock.ShareBlocks.Remove(shareBlock);

                    var spsMapping = stockPortfolio.SpsMappings.Where(spsm => spsm.StockPortfolioId == portfolioId).Single();

                    // Unnecessary? Maybe these are removed automatically by Entity Framework?
                    stock.SpsMappings.Remove(spsMapping);
                    stockPortfolio.SpsMappings.Remove(spsMapping);

                    _context.Update(stock);
                    _context.Update(stockPortfolio);
                    _context.Remove(spsMapping);
                    _context.Remove(shareBlock);
                }
                else
                {
                    shareBlock.NumberOfShares -= sellStockViewModel.NumberOfShares;

                    _context.Update(shareBlock);
                }

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockPortfolioExists(stockPortfolio.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Content", new { id = id });
            }

            sellStockViewModel.OwnedStockList = JsonConvert.DeserializeObject<IEnumerable<SelectListItem>>(TempData["OwnedStockList"].ToString());
            TempData.Keep("OwnedStockList");

            return View(sellStockViewModel);
        }
    }
}

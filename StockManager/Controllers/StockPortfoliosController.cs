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

        // GET: StockPortfolios/BuyStock
        public async Task<IActionResult> BuyStock(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockList = await _context.Stocks.Select(s => new SelectListItem() { Text = s.Name, Value = s.Id.ToString() }).ToListAsync();

            var buyStockViewModel = new BuyStockViewModel() { PortfolioId = (int)id, StockList = stockList };
            
            // ViewBag.StockSelectList = new SelectList(_context.Stocks, "Id", "Name");

            return View(buyStockViewModel);
        }

        // POST: StockPortfolios/BuyStock
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BuyStock([Bind("StockId","NumberOfShares","PortfolioId")] BuyStockViewModel buyStockViewModel)
        {
            if (ModelState.IsValid)
            {
                // Do logic and save changes here...
                var stockPortfolio = _context.StockPortfolios.Where(sp => sp.Id == buyStockViewModel.PortfolioId).Single();

                stockPortfolio.SpsMappings.Add(new StockPortfolioStockMapping() { StockId = buyStockViewModel.StockId });

                _context.StockPortfolios.Update(stockPortfolio);
                _context.SaveChangesAsync();

                return RedirectToAction("Content", new { id = buyStockViewModel.PortfolioId });
            }
                        
            return RedirectToAction("BuyStock", new { id = buyStockViewModel.PortfolioId } );
        }
    }
}

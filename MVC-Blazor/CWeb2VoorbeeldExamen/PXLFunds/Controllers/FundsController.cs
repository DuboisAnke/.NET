using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PXLFunds.Data;

namespace PXLFunds.Controllers
{
    public class FundsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FundsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Funds
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Fund.Include(f => f.Bank);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Funds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fund = await _context.Fund
                .Include(f => f.Bank)
                .FirstOrDefaultAsync(m => m.FundId == id);
            if (fund == null)
            {
                return NotFound();
            }

            return View(fund);
        }

        // GET: Funds/Create
        public IActionResult Create()
        {
            ViewData["BankId"] = new SelectList(_context.Bank, "BankId", "BankId");
            return View();
        }

        // POST: Funds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FundId,BankId,FundName,FundValue")] Fund fund)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fund);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BankId"] = new SelectList(_context.Bank, "BankId", "BankId", fund.BankId);
            return View(fund);
        }

        // GET: Funds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fund = await _context.Fund.FindAsync(id);
            if (fund == null)
            {
                return NotFound();
            }
            ViewData["BankId"] = new SelectList(_context.Bank, "BankId", "BankId", fund.BankId);
            return View(fund);
        }

        // POST: Funds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FundId,BankId,FundName,FundValue")] Fund fund)
        {
            if (id != fund.FundId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fund);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FundExists(fund.FundId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BankId"] = new SelectList(_context.Bank, "BankId", "BankId", fund.BankId);
            return View(fund);
        }

        // GET: Funds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fund = await _context.Fund
                .Include(f => f.Bank)
                .FirstOrDefaultAsync(m => m.FundId == id);
            if (fund == null)
            {
                return NotFound();
            }

            return View(fund);
        }

        // POST: Funds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fund = await _context.Fund.FindAsync(id);
            _context.Fund.Remove(fund);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FundExists(int id)
        {
            return _context.Fund.Any(e => e.FundId == id);
        }
    }
}

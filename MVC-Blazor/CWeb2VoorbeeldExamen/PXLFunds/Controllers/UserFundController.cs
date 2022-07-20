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
    public class UserFundController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserFundController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserFund
        public async Task<IActionResult> Index()
        {
            //var applicationDbContext = _context.UserFund.Include(u => u.Fund);
            //return View(await applicationDbContext.ToListAsync());

            return View();
        }

        // GET: UserFund/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFund = await _context.UserFund
                .Include(u => u.Fund)
                .FirstOrDefaultAsync(m => m.UserFundId == id);
            if (userFund == null)
            {
                return NotFound();
            }

            return View(userFund);
        }

        // GET: UserFund/Create
        public IActionResult Create()
        {
            ViewData["FundId"] = new SelectList(_context.Fund, "FundId", "FundId");
            return View();
        }

        // POST: UserFund/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserFundId,UserId,FundId,Amount")] UserFund userFund)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userFund);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FundId"] = new SelectList(_context.Fund, "FundId", "FundId", userFund.FundId);
            return View(userFund);
        }

        // GET: UserFund/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFund = await _context.UserFund.FindAsync(id);
            if (userFund == null)
            {
                return NotFound();
            }
            ViewData["FundId"] = new SelectList(_context.Fund, "FundId", "FundId", userFund.FundId);
            return View(userFund);
        }

        // POST: UserFund/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserFundId,UserId,FundId,Amount")] UserFund userFund)
        {
            if (id != userFund.UserFundId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userFund);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserFundExists(userFund.UserFundId))
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
            ViewData["FundId"] = new SelectList(_context.Fund, "FundId", "FundId", userFund.FundId);
            return View(userFund);
        }

        // GET: UserFund/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFund = await _context.UserFund
                .Include(u => u.Fund)
                .FirstOrDefaultAsync(m => m.UserFundId == id);
            if (userFund == null)
            {
                return NotFound();
            }

            return View(userFund);
        }

        // POST: UserFund/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userFund = await _context.UserFund.FindAsync(id);
            _context.UserFund.Remove(userFund);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserFundExists(int id)
        {
            return _context.UserFund.Any(e => e.UserFundId == id);
        }
    }
}

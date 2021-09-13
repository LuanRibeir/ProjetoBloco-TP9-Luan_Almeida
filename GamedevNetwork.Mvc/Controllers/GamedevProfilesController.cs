using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GamedevNetwork.Mvc.Data;
using GamedevNetwork.Mvc.Models;

namespace GamedevNetwork.Mvc.Controllers
{
    public class GamedevProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GamedevProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GamedevProfiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.GamedevProfile.ToListAsync());
        }

        // GET: GamedevProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamedevProfile = await _context.GamedevProfile
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gamedevProfile == null)
            {
                return NotFound();
            }

            return View(gamedevProfile);
        }

        // GET: GamedevProfiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GamedevProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImagemUri,Nome,Nacionalidade,JogoFavorito,QuantidadeDeJogosPublicados,Nascimento,UserId")] GamedevProfile gamedevProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gamedevProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gamedevProfile);
        }

        // GET: GamedevProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamedevProfile = await _context.GamedevProfile.FindAsync(id);
            if (gamedevProfile == null)
            {
                return NotFound();
            }
            return View(gamedevProfile);
        }

        // POST: GamedevProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImagemUri,Nome,Nacionalidade,JogoFavorito,QuantidadeDeJogosPublicados,Nascimento,UserId")] GamedevProfile gamedevProfile)
        {
            if (id != gamedevProfile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gamedevProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamedevProfileExists(gamedevProfile.Id))
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
            return View(gamedevProfile);
        }

        // GET: GamedevProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamedevProfile = await _context.GamedevProfile
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gamedevProfile == null)
            {
                return NotFound();
            }

            return View(gamedevProfile);
        }

        // POST: GamedevProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gamedevProfile = await _context.GamedevProfile.FindAsync(id);
            _context.GamedevProfile.Remove(gamedevProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GamedevProfileExists(int id)
        {
            return _context.GamedevProfile.Any(e => e.Id == id);
        }
    }
}

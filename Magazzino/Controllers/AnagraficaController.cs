using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Magazzino.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using System.Threading;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Magazzino.Controllers
{
    public class AnagraficaController : Controller
    {
        MagazzinoContext entities = new MagazzinoContext();
        private MagazzinoContext magazzinoContext;
        public AnagraficaController(MagazzinoContext magazzinoContext)
        {
            this.magazzinoContext = magazzinoContext;
        }
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            ViewBag.CurrentSort = searchString;
            ViewBag.NoteSortParm = searchString == "note" ? "note_desc" : "note";
            ViewBag.DescrizioneSortParm = searchString == "descrizione" ? "descrizione_desc" : "descrizione";
            ViewBag.CodiceSortParm = String.IsNullOrEmpty(searchString) ? "codice_desc" : "";
            var articolo = from q in magazzinoContext.Anagrafica select q;
            if (!String.IsNullOrEmpty(searchString))
            {
                articolo = articolo.Where(q => q.Codice.Contains(searchString) || q.Descrizione.Contains(searchString)
                || q.Note.Contains(searchString));
            }
            switch (searchString)
            {
                case "note":
                    articolo = articolo.OrderBy(x => x.Note);
                    break;
                case "note_desc":
                    articolo = articolo.OrderByDescending(x => x.Note);
                    break;
                case "descrizione":
                    articolo = articolo.OrderBy(x => x.Descrizione);
                    break;
                case "descrizione_desc":
                    articolo = articolo.OrderByDescending(x => x.Descrizione);
                    break;
                case "codice_desc":
                    articolo = articolo.OrderByDescending(x => x.Codice);
                    break;
                default:
                    articolo = articolo.OrderBy(x => x.Codice);
                    break;
            }
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(articolo.ToPagedList(pageNumber, pageSize));
        }




        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Codice,Descrizione,Note")] Anagrafica articolo)
        {
            if (ModelState.IsValid)
            {
                magazzinoContext.Add(articolo);
                magazzinoContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(articolo);
        }
        public async Task<IActionResult> Details(string code)
        {
            if (code == "")
            {
                return NotFound();
            }

            var articolo = await magazzinoContext.Anagrafica
                .FirstOrDefaultAsync(m => m.Codice == code);
            if (articolo == null)
            {
                return NotFound();
            }

            return View(articolo);
        }
        public async Task<IActionResult> Edit(string code)
        {
            if (code == "")
            {
                return NotFound();
            }

            var articolo = await magazzinoContext.Anagrafica.FindAsync(code);
            if (articolo == null)
            {
                return NotFound();
            }
            return View(articolo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string code, [Bind("Codice,Descrizione,Note")] Anagrafica articolo) 
        {
            if (code != articolo.Codice)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    magazzinoContext.Update(articolo);
                    await magazzinoContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(articolo.Codice))
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
            return View(articolo);
        }
        public async Task<IActionResult> Delete(string code)
        {
            if (code == "")
            {
                return NotFound();
            }

            var articolo = await magazzinoContext.Anagrafica
                .FirstOrDefaultAsync(m => m.Codice == code);
            if (articolo == null)
            {
                return NotFound();
            }

            return View(articolo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string code)
        {
            var articolo = await magazzinoContext.Anagrafica.FindAsync(code);
            magazzinoContext.Anagrafica.Remove(articolo);
            await magazzinoContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool ItemExists(string code)
        {
            return magazzinoContext.Anagrafica.Any(e => e.Codice == code);
        }
    }
}
 
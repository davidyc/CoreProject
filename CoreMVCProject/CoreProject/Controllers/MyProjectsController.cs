using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreProject.Models;
using CoreProject.Models.AppModel;
using Microsoft.AspNetCore.Authorization;

namespace CoreProject.Controllers
{
    [Authorize]
    public class MyProjectsController : Controller
    {
        private readonly AppDBContext _context;

        public MyProjectsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: MyProjects
        public async Task<IActionResult> Index()
        {
            return View(await _context.MyProjects.ToListAsync());
        }

        // GET: MyProjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myProject = await _context.MyProjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myProject == null)
            {
                return NotFound();
            }

            return View(myProject);
        }

        [Authorize(Roles = "davidyc")]
        // GET: MyProjects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MyProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "davidyc")]
        public async Task<IActionResult> Create([Bind("Id,Name,Decription,External,URL")] MyProject myProject)
        {
            if (ModelState.IsValid)
            {
                myProject.CreateDate = DateTime.UtcNow;
                _context.Add(myProject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(myProject);
        }

        [Authorize(Roles = "davidyc")]
        // GET: MyProjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myProject = await _context.MyProjects.FindAsync(id);
            if (myProject == null)
            {
                return NotFound();
            }
            return View(myProject);
        }

        // POST: MyProjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "davidyc")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Decription,CreateDate,External,URL")] MyProject myProject)
        {
            if (id != myProject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(myProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyProjectExists(myProject.Id))
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
            return View(myProject);
        }

        // GET: MyProjects/Delete/5
        [Authorize(Roles = "davidyc")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myProject = await _context.MyProjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myProject == null)
            {
                return NotFound();
            }

            return View(myProject);
        }

        // POST: MyProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "davidyc")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var myProject = await _context.MyProjects.FindAsync(id);
            _context.MyProjects.Remove(myProject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MyProjectExists(int id)
        {
            return _context.MyProjects.Any(e => e.Id == id);
        }
    }
}

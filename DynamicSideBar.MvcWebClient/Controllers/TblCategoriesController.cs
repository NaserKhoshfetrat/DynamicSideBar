#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Context;
using DataModel.Models;
using DAL.Services;
using Microsoft.Extensions.Caching.Memory;

namespace DynamicSideBar.MvcWebClient.Controllers
{
    public class TblCategoriesController : Controller
    {
        
        private CategoryRepository _categoryRepository;
            
        public TblCategoriesController(IMemoryCache memoryCache)
        {
            _categoryRepository = new UnitOfWork(memoryCache).TblCategory;
        }

        // GET: TblCategories
        public IActionResult Index(int? id)
        {
            var data = _categoryRepository.GetAll().Where(c => c.ParentCategoryId == (id ?? 0));
            return View(data);
        }

        // GET: TblCategories/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCategory = _categoryRepository.GetAll().Where(c => c.ParentCategoryId == id);
            if (tblCategory == null)
            {
                return NotFound();
            }

            return View(tblCategory);
        }

        // GET: TblCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ParentCategoryId,Title,Priority,Deleted")] TblCategory tblCategory)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Insert(tblCategory);
                _categoryRepository.Save();
                _categoryRepository.RemoveCache();
                return RedirectToAction(nameof(Index));
            }
            return View(tblCategory);
        }

        //// GET: TblCategories/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var tblCategory = await _context.TblCategories.FindAsync(id);
        //    if (tblCategory == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(tblCategory);
        //}

        //// POST: TblCategories/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,ParentCategoryId,Title,Priority,Deleted")] TblCategory tblCategory)
        //{
        //    if (id != tblCategory.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(tblCategory);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TblCategoryExists(tblCategory.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(tblCategory);
        //}

        // GET: TblCategories/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCategory = _categoryRepository.GetById(id);
            if (tblCategory == null)
            {
                return NotFound();
            }

            return View(tblCategory);
        }

        // POST: TblCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _categoryRepository.Delete(id);
            _categoryRepository.Save();

            _categoryRepository.RemoveCache();

            return RedirectToAction(nameof(Index));
        }

        //private bool TblCategoryExists(int id)
        //{
        //    return _context.TblCategories.Any(e => e.Id == id);
        //}
    }
}

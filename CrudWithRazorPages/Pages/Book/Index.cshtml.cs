using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudWithRazorPages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CrudWithRazorPages.Pages.Book
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Model.Book> Books { get; set; }

        public async Task OnGet()
        {
            Books = await _db.Book.ToListAsync();
        }


        //custom OnPost method with given name Deletem.(Matches to -> asp-page-handler="Delete" on the front end)
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await _db.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _db.Book.Remove(book);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
using BulkyWebRazor.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BulkyWebRazor.Models;

namespace BulkyWebRazor.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<BulkyWebRazor.Models.Category> CategoryList { get; set; }

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            CategoryList = _db.Categories.ToList();
        }
    }
}

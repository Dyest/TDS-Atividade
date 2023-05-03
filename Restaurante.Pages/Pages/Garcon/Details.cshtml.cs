using Restaurante.API.Data;
using Restaurante.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Restaurante.Pages.Pages.Garcon
{
    public class Details : PageModel
    {
        public GarconModel GarconModel { get; set; } = new();

        public Details(AppDbContext context){
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id){
            if(id == null || _context.Garcon == null){
                return NotFound();
            }

            var garconModel = await _context.Garcon.FirstOrDefaultAsync(e => e.GarconId == id);
            if(garconModel == null){
                return NotFound();
            }
            GarconModel = garconModel;
            return Page();
        }
    }
}
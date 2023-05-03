using Restaurante.API.Data;
using Restaurante.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Restaurante.RazorPages.Pages.Mesa
{
    public class Details : PageModel
    {
        public MesaModel MesaModel { get; set; } = new();

        public Details(AppDbContext context){
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id){
            if(id == null || _context.Mesa == null){
                return NotFound();
            }

            var mesaModel = await _context.Mesa.FirstOrDefaultAsync(e => e.MesaId == id);
            if(mesaModel == null){
                return NotFound();
            }
            MesaModel = mesaModel;
            return Page();
        }
    }
}
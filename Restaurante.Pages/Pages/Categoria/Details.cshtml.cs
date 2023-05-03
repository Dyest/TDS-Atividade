using Restaurante.API.Data;
using Restaurante.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
namespace Restaurante.Pages.Pages.Categoria
{
    public class Details : PageModel
    {
        public CategoriaModel CategoriaModel { get; set; } = new();

        public Details(AppDbContext context){
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id){
            if(id == null || _context.Categoria == null){
                return NotFound();
            }

            var categoriaModel = await _context.Categoria.FirstOrDefaultAsync(e => e.CategoriaId == id);
            if(categoriaModel == null){
                return NotFound();
            }
            CategoriaModel = categoriaModel;
            return Page();
        }
    }
}
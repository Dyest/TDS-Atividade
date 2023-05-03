using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Restaurante.API.Data;
using Restaurante.Pages.Models;

namespace Restaurante.Pages.Pages.Categoria
{
    public class Index : PageModel
    {
        public List<CategoriaModel> CategoriaList { get; set; } = new();
        public Index()
        {
            //_context = context;//
        }

        public async Task<IActionResult> OnGetAsync(){
            CategoriaList = await _context.Categoria!.ToListAsync();
            return Page();
        }
    }
}
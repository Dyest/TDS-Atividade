using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetoGerenciamentoRestaurante.RazorPages.Data;
using ProjetoGerenciamentoRestaurante.RazorPages.Models;

namespace ProjetoGerenciamentoRestaurante.RazorPages.Pages.Categoria
{
    public class Index : PageModel
    {
        public List<CategoriaModel> CategoriaList { get; set; } = new();
        public Index(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(){
            CategoriaList = await _context.Categoria!.ToListAsync();
            return Page();
        }
    }
}
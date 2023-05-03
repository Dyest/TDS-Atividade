using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Restaurante.API.Data;
using Restaurante.Pages.Models;

namespace ProjetoGerenciamentoRestaurante.RazorPages.Pages.Garcon
{
    public class Index : PageModel
    {

        public List<GarconModel> GarconList { get; set; } = new();
        public Index(){
           // _context = context;//
        }

        public async Task<IActionResult> OnGetAsync(){
            GarconList = await _context.Garcon!.ToListAsync();
            return Page();
        }
    }
}
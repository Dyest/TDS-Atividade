using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Restaurante.API.Data;
using Restaurante.Pages.Models;

namespace Restaurante.RazorPages.Pages.Mesa
{
    public class Index : PageModel
    {

        public List<MesaModel> MesaList { get; set; } = new();
        public Index(){
            //_context = context;//
        }

        public async Task<IActionResult> OnGetAsync(){
            MesaList = await _context.Mesa!.ToListAsync();
            return Page();
        }
    }
}
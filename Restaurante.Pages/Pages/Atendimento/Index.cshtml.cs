using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Restaurante.API.Data;
using Restaurante.Pages.Models;

namespace Restaurante.Pages.Pages.Atendimento
{
    public class Index : PageModel
    {

        public List<AtendimentoModel> AtendimentoList { get; set; } = new();
        public Index(){
           // _context = context;//
        }

        public async Task<IActionResult> OnGetAsync(){
            AtendimentoList = await _context.Atendimento!
            .Include(p => p.Mesa)
            .ToListAsync();
            
            return Page();
        }
    }
}
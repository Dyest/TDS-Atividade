using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Restaurante.API.Data;
using Restaurante.Pages.Models;

namespace Restaurante.Pages.Pages.Garcon
{
    public class Create : PageModel
    {
        [BindProperty]
        public GarconModel GarconModel { get; set; } = new();
        public Create(AppDbContext context){
            _context = context;
        }
        
        public async Task<IActionResult> OnPostAsync(int id){
            if(!ModelState.IsValid){
                return Page();
            }
            try{
                _context.Add(GarconModel);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Garcon/Index");
            } catch(DbUpdateException){
                return Page();
            }
            
        }
    }
}
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

        public Details(){ 
        }

        public async Task<IActionResult> OnGetAsync(int? id){
            if(id == null || _context.Categoria == null){
                return NotFound();
            }

            var httpClient = new HttpClient();
            var url = $"http://localhost:5085/Categoria/Details/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            if(!response.IsSuccessStatusCode){
                return NotFound();
            }

            var content = await response.Content.ReadAsStringAsync();
            CategoriaModel = JsonConvert.DeserializeObject<CategoriaModel>(content)!;
            
            return Page();
        }
    }
}
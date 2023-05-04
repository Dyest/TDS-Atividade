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
        public Index(){
        }

        public async Task<IActionResult> OnGetAsync(){
            var httpClient = new HttpClient();
            var url = "http://localhost:5085/Categoria";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadAsStringAsync();

            CategoriaList = JsonConvert.DeserializeObject<List<CategoriaModel>>(content)!;
            
            return Page();
        }
    }
}
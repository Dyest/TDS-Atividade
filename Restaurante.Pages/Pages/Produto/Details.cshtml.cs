using Restaurante.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Restaurante.Pages.Pages.Produto
{
    public class Details : PageModel
    {
        public ProdutoModel ProdutoModel { get; set; } = new();

        public Details(){
        }

        public async Task<IActionResult> OnGetAsync(int? id){
            if(id == null){
                return NotFound();
            }

            var httpClient = new HttpClient();
            var url = $"http://localhost:5085/Produto/Details/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            if(!response.IsSuccessStatusCode){
                return NotFound();
            }

            var content = await response.Content.ReadAsStringAsync();
            ProdutoModel = JsonConvert.DeserializeObject<ProdutoModel>(content)!;
            
            return Page();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurante.Pages.Models;
using Newtonsoft.Json;

namespace Restaurante.Pages.Pages.Produto
{
    public class Index : PageModel
    {
        public List<ProdutoModel> ProdutoList { get; set; } = new();
        public Index(){
        }

        public async Task<IActionResult> OnGetAsync(){
            var httpClient = new HttpClient();
            var url = "http://localhost:5085/Produto";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadAsStringAsync();

            ProdutoList = JsonConvert.DeserializeObject<List<ProdutoModel>>(content)!;
            
            return Page();
        }
    }
}
using Restaurante.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Restaurante.Pages.Pages.Produto
{
    public class Edit : PageModel
    {   
        [BindProperty]
        public ProdutoModel ProdutoModel { get; set; } = new();
        public List<CategoriaModel> CategoriaList { get; set; } = new();

        public Edit(){
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpClient = new HttpClient();
            var url = $"http://webapi/Produto/Details/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var content = await response.Content.ReadAsStringAsync();
            ProdutoModel = JsonConvert.DeserializeObject<ProdutoModel>(content)!;

            // fazer uma nova requisição HTTP GET para obter a lista de categorias
            var httpClientCategoria = new HttpClient();
            var urlCategoria = "http://webapi/Categoria";
            var requestMessageCategoria = new HttpRequestMessage(HttpMethod.Get, urlCategoria);
            var responseCategoria = await httpClientCategoria.SendAsync(requestMessageCategoria);
            var contentCategoria = await responseCategoria.Content.ReadAsStringAsync();

            CategoriaList = JsonConvert.DeserializeObject<List<CategoriaModel>>(contentCategoria)!;

            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int id){
            if(!ModelState.IsValid){
                return Page();
            }

            var httpClient = new HttpClient();
            var url = $"http://webapi/Produto/Edit/{id}";
            var produtoJson = Newtonsoft.Json.JsonConvert.SerializeObject(ProdutoModel);

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, url);
            requestMessage.Content = new StringContent(produtoJson, Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(requestMessage);

            if(!response.IsSuccessStatusCode){
                return Page();
            }

            return RedirectToPage("/Produto/Index");
        }
    }
}
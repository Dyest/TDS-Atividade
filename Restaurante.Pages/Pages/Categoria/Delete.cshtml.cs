using Restaurante.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Restaurante.Pages.Pages.Categoria
{
    public class Delete : PageModel
    {
        [BindProperty]

            public CategoriaModel CategoriaModel { get; set; } = new();
            public Delete(){
            }

        public async Task<IActionResult> OnGetAsync(int? id){
            if(id == null || _context.Categoria == null){
                return NotFound();
            }
 var httpClient = new HttpClient();
            var url = "http://localhost:5085/Categoria/Create";
            var categoriaJson = JsonConvert.SerializeObject(CategoriaModel);
            var content = new StringContent(categoriaJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            
            if(response.IsSuccessStatusCode){
                return RedirectToPage("/Categoria/Index");
            } else {
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync(int id){
            var httpClient = new HttpClient();
            var url = $"http://localhost:5085/Categoria/Delete/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, url);
            var response = await httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Categoria/Index");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            else
            {
                return Page();
            }
        }
    }
}
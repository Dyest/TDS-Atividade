using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurante.Pages.Models;
using Newtonsoft.Json;
using System.Text;

namespace Restaurante.Pages.Pages.Mesa
{
    public class Index : PageModel
    {

        public List<MesaModel> MesaList { get; set; } = new();
        public Index(){
        }

        public async Task<IActionResult> OnGetAsync(){
            var httpClient = new HttpClient();
            var url = "http://webapi/Mesa";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadAsStringAsync();

            MesaList = JsonConvert.DeserializeObject<List<MesaModel>>(content)!;
            
            return Page();
        }
    }
}
using Restaurante.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Restaurante.Pages.Pages.Atendimento
{
    public class Create : PageModel
    {
        [BindProperty]
        public AtendimentoModel AtendimentoModel { get; set; } = new();
        public List<MesaModel> MesaList { get; set; } = new();
        public Create(){
        }

        public async Task<IActionResult> OnGetAsync(){
            var httpClientMesa = new HttpClient();
            var urlMesa = "http://webapi/Mesa";
            var requestMessageMesa = new HttpRequestMessage(HttpMethod.Get, urlMesa);
            var responseMesa = await httpClientMesa.SendAsync(requestMessageMesa);
            var contentMesa = await responseMesa.Content.ReadAsStringAsync();

            MesaList = JsonConvert.DeserializeObject<List<MesaModel>>(contentMesa)!;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id){
            if(!ModelState.IsValid){
                return RedirectToPage("/Atendimento/Create");
            }
            
            var httpClient = new HttpClient();
            var atendimentoJson = JsonConvert.SerializeObject(AtendimentoModel);
            var content = new StringContent(atendimentoJson, Encoding.UTF8, "application/json");
            var url = "http://webapi/Atendimento/Create";
            var response = await httpClient.PostAsync(url, content);
            

            if(response.IsSuccessStatusCode){
                return RedirectToPage("/Atendimento/Index");
            } else {
                return Page();
            }
        }
    }
}
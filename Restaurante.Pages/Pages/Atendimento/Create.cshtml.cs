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
            var urlMesa = "http://localhost:5085/Mesa";
            var requestMessageMesa = new HttpRequestMessage(HttpMethod.Get, urlMesa);
            var responseMesa = await httpClientMesa.SendAsync(requestMessageMesa);
            var contentMesa = await responseMesa.Content.ReadAsStringAsync();

            MesaList = JsonConvert.DeserializeObject<List<MesaModel>>(contentMesa)!;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id){
            if(!ModelState.IsValid){
                return Page();
            }
            
            AtendimentoModel.DataCriacao = DateTime.Now;
            var httpClient = new HttpClient();
            var url = "http://localhost:5085/Atendimento/Create";
            var atendimentoJson = JsonConvert.SerializeObject(AtendimentoModel);
            var content = new StringContent(atendimentoJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            

            if(response.IsSuccessStatusCode){
                return RedirectToPage("/Atendimento/Index");
            } else {
                return Page();
            }
        }
    }
}
using Restaurante.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;


namespace Restaurante.Pages.Pages.Atendimento
{
    public class Edit : PageModel
    {
        [BindProperty]
        public AtendimentoModel AtendimentoModel { get; set; } = new();
        public MesaModel MesaModel { get; set; } = new();
        public List<MesaModel> MesaList { get; set; } = new();
        public Edit(){
        }

        public async Task<IActionResult> OnGetAsync(int? id){
            if (id == null)
            {
                return NotFound();
            }

            var httpClientAtendimento = new HttpClient();
            var urlAtendimento = $"http://webapi/Atendimento/Details/{id}";
            var responseAtendimento = await httpClientAtendimento.GetAsync(urlAtendimento);

            if (!responseAtendimento.IsSuccessStatusCode)
            {
                return NotFound();
            }
            var contentAtendimento = await responseAtendimento.Content.ReadAsStringAsync();
            AtendimentoModel = JsonConvert.DeserializeObject<AtendimentoModel>(contentAtendimento)!;
            
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
                return Page();
            }

            var httpClient = new HttpClient();
            var url = $"http://webapi/Atendimento/Edit/{id}";
            var atendimentoJson = Newtonsoft.Json.JsonConvert.SerializeObject(AtendimentoModel);

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, url);
            requestMessage.Content = new StringContent(atendimentoJson, Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(requestMessage);

            if(!response.IsSuccessStatusCode){
                return Page();
            }

            return RedirectToPage("/Atendimento/Index");
        }
    }
}
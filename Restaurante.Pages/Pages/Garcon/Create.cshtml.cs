using Restaurante.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net;

namespace Restaurante.Pages.Pages.Garcon
{
    public class Create : PageModel
    {
        [BindProperty]
        public GarconModel GarconModel { get; set; } = new(); 

        public Create(){
        }

        public async Task<IActionResult> OnPostAsync(int id){
            if(!ModelState.IsValid){
                return Page();
            }
            
            var httpClient = new HttpClient();
            var url = "http://localhost:5085/Garcon/Create";
            var garconJson = JsonConvert.SerializeObject(GarconModel);
            var content = new StringContent(garconJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            
            if(response.IsSuccessStatusCode){
                return RedirectToPage("/Garcon/Index");
            } else {
                return Page();
            }
        }
    }
}
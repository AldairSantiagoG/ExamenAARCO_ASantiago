using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ExamenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> ConsultaCodigo(string codigoPostal)
        {
            string result = "";

            //JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };//serializa correctamente, ignora mayusculas 
            string url = "https://api-test.aarco.com.mx/api-examen/api/examen/sepomex/" + codigoPostal;
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
               
                result = await response.Content.ReadAsStringAsync();
                 
            }

            return Json(result);
        }
    }
}

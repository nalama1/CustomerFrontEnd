using Microsoft.AspNetCore.Mvc; //probar con esta cedula: 0900000011


namespace Front.Cliente.Controllers
{
    public class ClienteController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ClienteController> _logger;
        public ClienteController(HttpClient httpClient, IConfiguration configuration, ILogger<ClienteController> logger)
        {            
            _logger = logger;
            string? url = configuration["ApiBaseUrl"];

            if (string.IsNullOrEmpty(url))
            {
                _logger.LogError("La URL no está configurada.");
                throw new InvalidOperationException("La URL no está configurada.");
            }

            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(url);          
        }

        public async Task<IActionResult> ObtenerCliente(string cedula)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(cedula) || cedula.Length != 10 || !cedula.All(char.IsDigit))
                {
                    _logger.LogError("La cédula no contiene 10 dígitos.");
                    ModelState.AddModelError("cedula", "La cédula debe contener 10 dígitos.");
                    return BadRequest(ModelState);
                }
                var response = await _httpClient.GetAsync($"api/Cliente/{cedula}"); //Web API

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode);

                var cliente = await response.Content.ReadAsStringAsync();
                return Content(cliente, "application/json");

            }
            else
            {
                _logger.LogError("ModelState no es válido.");
                ModelState.AddModelError("modelo", "ModelState no es válido.");
                return BadRequest(ModelState);
            }


        }


    }
}


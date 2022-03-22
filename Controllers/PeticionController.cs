using Microsoft.AspNetCore.Mvc;
using PeticionService.Policies;

namespace PeticionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeticionController : ControllerBase
    {
        //private readonly ClientePolicy _clientePolicy;
        private readonly IHttpClientFactory _clienteFactory;

        /*
        public PeticionController(ClientePolicy clientePolicy, IHttpClientFactory clienteFactory)
        {
            _clientePolicy = clientePolicy; //usamos obxeto ClientePolicy no constructor con dependency injection
            _clienteFactory = clienteFactory;
        }
        */

        public PeticionController(IHttpClientFactory clienteFactory)
        {
            _clienteFactory = clienteFactory;
        }

        // GET api/peticion
        [HttpGet]
        public async Task<ActionResult> FacerPeticion()
        {
            //var cliente = new HttpClient();

            var cliente = _clienteFactory.CreateClient("Test"); //utilizamos a client factory para crear un cliente e asignarlle unha conexion

            var resposta = await cliente.GetAsync("https://localhost:7117/api/resposta/25"); //chamando ao endpoint da outra API

            //var resposta = await _clientePolicy.LinearHttpRetry.ExecuteAsync(() => cliente.GetAsync("https://localhost:7117/api/resposta/25"));

            if (resposta.IsSuccessStatusCode)
            {
                Console.WriteLine("--> RespostaService devolve SUCCESS");
                return Ok();
            }

            Console.WriteLine("--> Resposta service devolve un ERROR");

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
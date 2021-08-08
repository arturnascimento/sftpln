using Microsoft.AspNetCore.Mvc;
using SoftplanApi1.Models;


namespace SoftplanApi1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class taxaJurosController : ControllerBase
    {
        /// <summary>
        /// Endpoint responsável por retornar a taxa de juros que deverá ser consumida pela Api2.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()//action que retorna o valor dos juros
        {
            var StaticJuros = new TaxaJuros();//Juros
            StaticJuros.Juros = 0.01; // atributo do objeto recebendo seu valor estático.


            return Ok(StaticJuros);//reotrno do valor.
            
            
        }
    }
}

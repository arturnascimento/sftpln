using Microsoft.AspNetCore.Mvc;
using SoftplanApi2.Models;

namespace SoftplanApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class showmethecodeController : ControllerBase
    {
        /// <summary>
        /// Endpoint responsável por retornar o repositório do GitHub.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  IActionResult GetLink()//action para retornar o link do repositório.
        {
            var linkGit = new LinkGitHub(); //Instancia da classe LinkGitHub

             linkGit.Link = "https://github.com/arturnascimento/sftpln.git"; //atributo link recebe a string contendo o endereço do repositório.

            return Ok(linkGit.Link);//retorno do método com o link.
        }

    }
}

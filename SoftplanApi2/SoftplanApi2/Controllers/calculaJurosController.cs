using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoftplanApi2.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SoftplanApi2.Controllers
{
    /// <summary>
    /// Endpoint responsável por retornar um cálculo de juros. O usuário deverá informar o valor e o tempo em meses.
    /// </summary>
    [Route("/[controller]")]
    [ApiController]
    public class calculaJurosController : ControllerBase
    {

        public static async Task<Juros> GetJuros()//método assíncrono que busca a taxa de juros na Api1
        {
            HttpClient client = new HttpClient();//cliente é um objeto do tipo HttpClient
            var response = await client.GetAsync("https://localhost:44399/taxaJuros");//response recebe o resultado do metodo GetAsync, recebendo o link localhost da Api1.
            var content = await response.Content.ReadAsStringAsync();//serialização da requisição em uma cadeia de strings.
            var api1return = new Juros(); // api1reurn sendo chamada, um objeto tipo Juros
            api1return = JsonConvert.DeserializeObject<Juros>(content); //Deserialização da cadeia de strings em objeto tipo Juros.
            return api1return; //retorno do método com o double 0.01.

        }

        public static decimal calcularValorFinal(decimal valor, int tempo, double juros)//metodo que recebe 3 parametros de calculo.
        {
            double v = Convert.ToDouble(valor);//convertendo valor de decimal para double
            double resulDouble = v * Math.Pow((1 + juros), tempo);//fazendo os calculos de juros
            decimal result = Convert.ToDecimal(resulDouble);//convertendo o valor do calculo em decimal
            return result;//retornando o valor em decimal
        }

        public static double TruncarResult(double resultado, double decimals)//metodo que realiza o truncamento do retorno esperado
        {
            var trunc = Math.Pow(10, decimals); 
            return Math.Truncate(resultado * trunc) / trunc;
        }

        [HttpPost]

        public IActionResult Calcular(decimal valor, int tempo)//action que recebe os parametros externos informados pelo usuário
        {
            var jurosApi = GetJuros(); //variável recebendo o valor recebido da Api1
            var calculo = new CalculaJuros();//calculo como objeto do tipo CalculaJuros
            calculo.ValorInicial = valor;//valorinicial recebe o valor inicial inserido pelo usuário
            calculo.Meses = tempo;//Meses recebe o tempo em meses informado pelo usuário
            calculo.Juros = jurosApi.Result.JurosAPI;//Juros recebe valor do atributo JurosAPI
            calculo.ValorFinal = calcularValorFinal(calculo.ValorInicial, calculo.Meses, calculo.Juros); //valor final recebe o seu resultado em decimal
            var resultTruncado = Convert.ToDecimal(TruncarResult(Convert.ToDouble(calculo.ValorFinal), 2)); // resultado truncado com 2 casas decimais
            return Ok(resultTruncado.ToString("F2"));//retorno do resultado para exibição na API2
        }




        
    }
}

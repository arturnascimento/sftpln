using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoftplanApi2.Controllers;
using SoftplanApi2.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SoftplanApi2Test
{
    public class calculaJurosControllerTest
    {
        CalculaJuros _fakeCalculaJuros;
        public calculaJurosControllerTest()
        {
            _fakeCalculaJuros = new CalculaJuros();
            _fakeCalculaJuros.ValorInicial = 100;
            _fakeCalculaJuros.Meses = 5;
            _fakeCalculaJuros.Juros = GetJuros().Result.JurosAPI;
            _fakeCalculaJuros.ValorFinal = calcularValorFinal(100, 5, 0.01);
        }

        public static async Task<Juros> GetJuros()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://localhost:44399/taxaJuros");
            var content = await response.Content.ReadAsStringAsync();
            var api1return = new Juros();
            api1return = JsonConvert.DeserializeObject<Juros>(content);
            return api1return;

        }

        [Theory]
        [InlineData(100, 5, 0.01)]
        public static decimal calcularValorFinal(decimal valor, int tempo, double juros)
        {
            double v = Convert.ToDouble(valor);
            double resulDouble = v * Math.Pow((1 + juros), tempo);
            decimal result = Convert.ToDecimal(resulDouble);
            return result;
        }
        public static double TruncarResult(double resultado, double decimals)
        {
            var trunc = Math.Pow(10, decimals);
            return Math.Truncate(resultado * trunc) / trunc;
        }

        [Theory]
        [InlineData(100, 5)]

        public void calcularValorFinalTest(decimal valor, int tempo)
        {
          
            var controller = new calculaJurosController();
            var result = controller.Calcular(valor, tempo) as OkObjectResult;
            var resultDecimal = Convert.ToDecimal(result.Value);

            var jurosApi = GetJuros();
            var calculo = new CalculaJuros();
            calculo.ValorInicial = valor;
            calculo.Meses = tempo;
            calculo.Juros = jurosApi.Result.JurosAPI;
            calculo.ValorFinal = calcularValorFinal(calculo.ValorInicial, calculo.Meses, calculo.Juros);
            var resultTruncado = Convert.ToDecimal(TruncarResult(Convert.ToDouble(calculo.ValorFinal), 2));

            Assert.Equal(resultDecimal.ToString("F2"), resultTruncado.ToString("F2"));

        }
    }
}


namespace SoftplanApi2.Models
{
    public class CalculaJuros
    {
        public decimal ValorInicial { get; set; }
        public int Meses { get; set; }

        public double Juros { get; set; }
        public decimal ValorFinal { get; set; }

        public CalculaJuros()
        {

        }
    }
}

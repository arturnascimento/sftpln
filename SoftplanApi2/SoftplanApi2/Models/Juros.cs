using Newtonsoft.Json;


namespace SoftplanApi2.Models
{
    public class Juros
    {
        [JsonProperty("juros")]
        public double JurosAPI { get; set; }
    }
}

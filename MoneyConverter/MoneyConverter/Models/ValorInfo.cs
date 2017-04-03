using Newtonsoft.Json;
using Xamarin.Forms;

namespace MoneyConverter.Models
{
    public class ValorInfo
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("valor")]
        public float Valor { get; set; }
        [JsonProperty("ultima_consulta")]
        public long UltimaConsulta { get; set; }
        [JsonProperty("fonte")]
        public string Fonte { get; set; }
    }
}

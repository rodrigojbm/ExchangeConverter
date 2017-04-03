using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoneyConverter.Models
{
    public class RootObject
    {
        [JsonProperty("status")]
        public bool Status { get; set; }
        [JsonProperty("valores")]
        public Dictionary<string, ValorInfo> Valores { get; set; }
    }
}

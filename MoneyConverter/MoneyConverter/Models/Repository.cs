using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MoneyConverter.Models
{
    public class Repository
    {
        private static async Task<string> GetStringAsync(string url)
        {

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "linqpad");

                var httpRequest = new HttpRequestMessage(new HttpMethod("GET"), url);
                var response = client.SendAsync(httpRequest).Result;

                var jsonString = await response.Content.ReadAsStringAsync();
                var objeto = JObject.Parse(jsonString).ToString();

                return objeto;
            }
        }

        public async Task<RootObject> DeserializeJson()
        {
            var json = await GetStringAsync("http://api.promasters.net.br/cotacao/v1/valores");

            return JsonConvert.DeserializeObject<RootObject>(json);
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MoonDate
{
    public class HijriMonthInfo
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Root
        {
            public List<List<string>> data { get; set; }
            public bool error { get; set; }
        }

        public static void GetHijriMonthInfo(String month, ref String history, ref String significance)
        {
            Task<string> taskResult = GetHijriMonthInformationAsync();
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(taskResult.Result);
            history = "No historical data found";
            significance = "No significance info found";
            foreach(List<String> data in myDeserializedClass.data)
            {
                if (data[0].Equals(month))
                {
                    history = data[1];
                    significance = data[2];
                }
            }
            return;
        }
        private static async Task<String> GetHijriMonthInformationAsync()
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://script.googleusercontent.com/macros/echo?user_content_key=ifiopw7rix9zQVu5zGiBxI5CeVFAReE4xz3zVkvttRmhEwBieUfQwcTQWbr7kI-ztUrbHnJp7qKFYZNPTMNs3Twgp7qKzdHOm5_BxDlH2jW0nuo2oDemN9CCS2h10ox_1xSncGQajx_ryfhECjZEnDxLRdLpf2cp9Rgwk43QfN4G7OjTnk3yjLqjWeJ0oMGxlpE02lslWftgD2Fmg1dZ2zimBndlPCDUjh6EgHFLP0MSfvp-Ai-eWNz9Jw9Md8uu&lib=ML9O0yliSlu8gIgavwHcREgPOQRluht6F");
            var response = await httpClient.SendAsync(request).ConfigureAwait(continueOnCapturedContext: false);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            return content;

        }
    }
}

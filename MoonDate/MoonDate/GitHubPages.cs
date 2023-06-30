using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MoonDate
{
    public class GitHubPages
    {
        static public String GetMainPageHtml()
        {
            Task<string> taskResult = GetMainPageHtmlAsync();
            return taskResult.Result;
        }

        public static async Task<String> GetMainPageHtmlAsync()
        {

            using (var httpClient = new HttpClient())
            {
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;

                String mainPage = "https://abunuha.github.io/HijriMonths/MoonDateMainPage.html";
                //using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://script.googleusercontent.com/macros/echo?user_content_key=BnsPRHHuxBcfhM32CInhBLdEgNFb05xCXdcvRLRgRnU7fT5ZnSm_YN7ERuP9NmbInZqL99LlJyXejPOu2M4o_dUmZXjVvOw1m5_BxDlH2jW0nuo2oDemN9CCS2h10ox_1xSncGQajx_ryfhECjZEnFY9VcwcNDzlmBFkXA-AuWMsVbLuNMtg95xGFRF89iN_uwsrtfrwFBNtrqhCC94BpkYPYKfS2A_XIH_1soS6bdUvLd5iryZzAdz9Jw9Md8uu&lib=MXcfO9jVcfCTHDb4XJYM8YwPOQRluht6F"))
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), mainPage))
                {
                    
                    var response = await httpClient.SendAsync(request).ConfigureAwait(continueOnCapturedContext: false);
                    response.EnsureSuccessStatusCode();
                    string content = await response.Content.ReadAsStringAsync();
                    return content;
                }
            }
        }
    }
}

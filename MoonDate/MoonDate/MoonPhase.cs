//using Android.Media.TV;
using Microsoft.Maui.ApplicationModel;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MoonDate
{
    public class MoonPhase
    {
        public class Data
        {
            public string imageUrl { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
        }

        //Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class HijriRoot
        {
            public List<List<string>> data { get; set; }
            public bool error { get; set; }
        }

        public static String GetCurrentMoonPhaseImageOld()
        {
            var appId = "4c0ec821-a246-4bf3-8f2c-3610ec7a81f9";
            var appIdSecret = "782af285e66d2f445ea01fed4764845150eb75bcbaba6f8aa921fe14450fde320104b13ac23ec08ba062126144cc556f2707fa7ef8cdef2da402e40e273c30f80b70ad89546894f8987f82a2599133e2ed5fe80b560cc4b4e828ba93a1567535c3c23c2b00158e8c44982a1cf492858a";
            
            var combine = appId + "=" + appIdSecret;
            String auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(combine));
            var client = new RestClient("https://api.astronomyapi.com/api/v2/studio/moon-phase");
            //var request = new RestRequest(Method.POST);
            var request = new RestRequest("POST");
            request.AddHeader("postman-token", "ddd31ed2-f795-e0d4-d16a-b7e79018d2a7");
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("Authorization", "Basic NGMwZWM4MjEtYTI0Ni00YmYzLThmMmMtMzYxMGVjN2E4MWY5Ojc4MmFmMjg1ZTY2ZDJmNDQ1ZWEwMWZlZDQ3NjQ4NDUxNTBlYjc1YmNiYWJhNmY4YWE5MjFmZTE0NDUwZmRlMzIwMTA0YjEzYWMyM2VjMDhiYTA2MjEyNjE0NGNjNTU2ZjI3MDdmYTdlZjhjZGVmMmRhNDAyZTQwZTI3M2MzMGY4MGI3MGFkODk1NDY4OTRmODk4N2Y4MmEyNTk5MTMzZTJlZDVmZTgwYjU2MGNjNGI0ZTgyOGJhOTNhMTU2NzUzNWMzYzIzYzJiMDAxNThlOGM0NDk4MmExY2Y0OTI4NThh");
            request.AddHeader("Authorization", "Basic " + auth);
            request.AddHeader("Content-type", "application/json");
            request.AddParameter("application/json", "{\n    \"format\": \"png\",\n    \"style\": {\n        \"moonStyle\": \"sketch\",\n        \"backgroundStyle\": \"stars\",\n        \"backgroundColor\": \"red\",\n        \"headingColor\": \"white\",\n        \"textColor\": \"red\"\n    },\n    \"observer\": {\n        \"latitude\": 6.56774,\n        \"longitude\": 79.88956,\n        \"date\": \"2020-11-01\"\n    },\n    \"view\": {\n        \"type\": \"portrait-simple\",\n        \"orientation\": \"south-up\"\n    }\n}", ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            String imgUrl = "https://widgets.astronomyapi.com/moon-phase/generated/b0152bf0ad34617b479b9f3c70b862eb825ff620a2bc0b12e6593beb21d96c2e.png";
            return imgUrl;
        }

        public static String GetCurrentMoonPhaseImage() 
        {
            Task<string> taskResult = GetCurrentMoonPhaseImageAsync();
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(taskResult.Result);
            String imageUrl = myDeserializedClass.data.imageUrl;
            return imageUrl;
        }

        
        public static async Task<string> GetCurrentMoonPhaseImageAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.astronomyapi.com/api/v2/studio/moon-phase"))
                {
                    request.Headers.TryAddWithoutValidation("authorization", "Basic NGMwZWM4MjEtYTI0Ni00YmYzLThmMmMtMzYxMGVjN2E4MWY5Ojc4MmFmMjg1ZTY2ZDJmNDQ1ZWEwMWZlZDQ3NjQ4NDUxNTBlYjc1YmNiYWJhNmY4YWE5MjFmZTE0NDUwZmRlMzIwMTA0YjEzYWMyM2VjMDhiYTA2MjEyNjE0NGNjNTU2ZjI3MDdmYTdlZjhjZGVmMmRhNDAyZTQwZTI3M2MzMGY4MGI3MGFkODk1NDY4OTRmODk4N2Y4MmEyNTk5MTMzZTJlZDVmZTgwYjU2MGNjNGI0ZTgyOGJhOTNhMTU2NzUzNWMzYzIzYzJiMDAxNThlOGM0NDk4MmExY2Y0OTI4NThh");
                    request.Headers.TryAddWithoutValidation("cache-control", "no-cache");
                    request.Headers.TryAddWithoutValidation("postman-token", "948aa679-ddb1-8553-68ad-7dd65bdadd04");

                    //request.Content = new StringContent("{\n    \"format\": \"png\",\n    \"style\": {\n        \"moonStyle\": \"sketch\",\n        \"backgroundStyle\": \"stars\",\n        \"backgroundColor\": \"red\",\n        \"headingColor\": \"white\",\n        \"textColor\": \"red\"\n    },\n    \"observer\": {\n        \"latitude\": 6.56774,\n        \"longitude\": 79.88956,\n        \"date\": \"2020-11-01\"\n    },\n    \"view\": {\n        \"type\": \"portrait-simple\",\n        \"orientation\": \"south-up\"\n    }\n}");
                    request.Content = MoonPhaseBodyJson.GetJson();
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                     var response = await httpClient.SendAsync(request).ConfigureAwait(continueOnCapturedContext: false);
                    response.EnsureSuccessStatusCode();
                    string content = await response.Content.ReadAsStringAsync();
                    return content;
                }
            }
            //String imgUrl = "https://widgets.astronomyapi.com/moon-phase/generated/b0152bf0ad34617b479b9f3c70b862eb825ff620a2bc0b12e6593beb21d96c2e.png";
            //return imgUrl;
        }

        public static void GetHijriMonthStart(ref String hijriMonth,  ref String startDate, ref String lastNewMoon)
        {
            Task<string> taskResult = GetHijriMonthStartAsync();
            HijriRoot hijri = JsonConvert.DeserializeObject<HijriRoot>(taskResult.Result);
            hijriMonth = hijri.data[0][0];
            startDate = hijri.data[0][1];
            lastNewMoon = hijri.data[0][2];

            return;
        }

        public static async Task<String> GetHijriMonthStartAsync()
        {
            
            using (var httpClient = new HttpClient())
            {
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;

                String apiUrl = "https://script.googleusercontent.com/macros/echo?user_content_key=2qiIPUwwa4QVHhdEfaZfG_HT_wmfNPLoUKUlM8tYbwteGwyTVY2yxpIyS_EXdzwMzc3xpOb39Y2oTb-oD0G48tvfLqvDc57gm5_BxDlH2jW0nuo2oDemN9CCS2h10ox_1xSncGQajx_ryfhECjZEnPuabIxi8UFt6yUq0BV2KR95fEv4crN9eXWM-DndkUACotPTPmsqlECXgCjMyOBs5m0Dv11ClYWTdxFBX613DnwP-6Y5oFxMMg&lib=MsTtme7fiu8N2SnpngQ1fBOKl_bsOE08m";
                //using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://script.googleusercontent.com/macros/echo?user_content_key=BnsPRHHuxBcfhM32CInhBLdEgNFb05xCXdcvRLRgRnU7fT5ZnSm_YN7ERuP9NmbInZqL99LlJyXejPOu2M4o_dUmZXjVvOw1m5_BxDlH2jW0nuo2oDemN9CCS2h10ox_1xSncGQajx_ryfhECjZEnFY9VcwcNDzlmBFkXA-AuWMsVbLuNMtg95xGFRF89iN_uwsrtfrwFBNtrqhCC94BpkYPYKfS2A_XIH_1soS6bdUvLd5iryZzAdz9Jw9Md8uu&lib=MXcfO9jVcfCTHDb4XJYM8YwPOQRluht6F"))
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), apiUrl))
                {
                    request.Headers.TryAddWithoutValidation("cache-control", "no-cache");
                    request.Headers.TryAddWithoutValidation("postman-token", "5fc6577f-c1ef-5ea8-8a0b-c5371a097516");
                    //System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    var response = await httpClient.SendAsync(request).ConfigureAwait(continueOnCapturedContext: false);
                    response.EnsureSuccessStatusCode();
                    string content = await response.Content.ReadAsStringAsync();
                    return content;
                }
            }
        }

        public static DateTime GetLastNewMoonTime()
        {
            //""May 19, 2023 at 3:53 pm GMT time of new moon in May 2023
            DateTime refNewMoonDate = DateTime.SpecifyKind(DateTime.ParseExact("05 19 2023 15 53", "MM dd yyyy HH mm", CultureInfo.CurrentCulture), DateTimeKind.Utc);
            // Convert to current time zone

            return refNewMoonDate.ToLocalTime();
        }

        public static DateTime GetNextNewMoonTime(DateTime lastMoonTime)
        {
            TimeSpan moonLife = new TimeSpan(29, 12, 44, 0);
            return lastMoonTime.Add(moonLife);
        }
    }
}

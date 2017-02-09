using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Xamarin_WebApiPost.Manager
{
    public class DataAccessPort
    {
        public string GetToken()
        {
            using (var client = new HttpClient())
            {

                var body = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("username", "nermin.yilmaz@anahtarfinans.com"),
                    new KeyValuePair<string, string>("password", "Anahtar123")
                };

                var content = new FormUrlEncodedContent(body);


                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsync("http://appapi.anahtarfinans.com/api/account/token/", content).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                var jsonobj = JsonConvert.DeserializeObject<Token>(result);
                return jsonobj.AccessToken;
            }
        }
        public List<OfiiceListDto> CallOfficeList(string token)
        {
            var officeDtoList = new List<OfiiceListDto>();
            using (var client = new HttpClient())
            {

                var body = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Controller", "Office"),
                    new KeyValuePair<string, string>("Action", "List"),
                    new KeyValuePair<string, string>("Method", "GET")
                };
                var content = new FormUrlEncodedContent(body);

                if (!string.IsNullOrWhiteSpace(token))
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
                var response = client.PostAsync("http://appapi.anahtarfinans.com/api/route/execute", content).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                dynamic jsonobj = JsonConvert.DeserializeObject(result);

                var officeList = jsonobj.Content.officeList.Items;

                foreach (var item in officeList)
                {
                    officeDtoList.Add(new OfiiceListDto()
                    {
                        Address = item.Address,
                        OfficeName = item.OfficeName,
                        PhoneCell = item.PhoneCell
                    });
                }


            }

            return officeDtoList;
        }

        public string AddNewOffice(string token)
        {
            using (var client = new HttpClient())
            {
                var officeContent = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("OfficeName", "Name"),
                    new KeyValuePair<string, string>("OfficeStatusValue", "Value"),
                    new KeyValuePair<string, string>("isApiRequest", "true")

                };

                var body = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Controller", "Office"),
                    new KeyValuePair<string, string>("Action", "AddOffice"),
                    new KeyValuePair<string, string>("Method", "POST"),
                    new KeyValuePair<string, string>("Paramaters", officeContent.ToString())
                   
                };



            }


            return null;
        }
    }
}
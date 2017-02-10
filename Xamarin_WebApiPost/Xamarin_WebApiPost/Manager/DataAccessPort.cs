using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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

        public List<CityDto> CallCityList(string token)
        {
            var cityDtoList = new List<CityDto>();
            using (var client = new HttpClient())
            {
                var body = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Controller", "Async"),
                    new KeyValuePair<string, string>("Action", "GetCityList"),
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

                var cityList = jsonobj.Content;

                foreach (var item in cityList)
                {
                    cityDtoList.Add(new CityDto()
                    {
                        CityName = item.Text,
                        CityNumber = item.Value
                    });
                }


            }
            return cityDtoList;

        } 

        public  AddOfficeDto AddNewOffice(string token, AddOfficeDto addOfficeDto)
        {
            

            using (var client = new HttpClient())
            {
                
                var officeParameter = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Id", addOfficeDto.Id),
                    new KeyValuePair<string, string>("OfficeName", addOfficeDto.OfficeName),
                    new KeyValuePair<string, string>("OfficeStatusValue", addOfficeDto.OfficeStatusValue),
                    new KeyValuePair<string, string>("İsApiRequest", addOfficeDto.IsApi.ToString()),
                    new KeyValuePair<string, string>("StartDate", addOfficeDto.StartDate.ToString("MMMM dd, yyyy")),
                    new KeyValuePair<string, string>("EndDate", addOfficeDto.EndDate.ToString("MMMM dd, yyyy"))

                };
                dynamic jsonobj1 = JsonConvert.DeserializeObject(officeParameter.ToString());

                var body = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Controller", "Office"),
                    new KeyValuePair<string, string>("Action", "AddOffice"),
                    new KeyValuePair<string, string>("Method", "POST"),
                    new KeyValuePair<string, string>("Paramaters",jsonobj1 )
                   
                };


                var content = new FormUrlEncodedContent(body);

                client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsync("http://appapi.anahtarfinans.com/api/route/execute", content).Result;
                var result = response.Content.ReadAsStringAsync().Result;


                dynamic jsonobj = JsonConvert.DeserializeObject(result);

                var officeList = jsonobj.Content.officeList.Items;




            }


            return null;
        }       
    }
}
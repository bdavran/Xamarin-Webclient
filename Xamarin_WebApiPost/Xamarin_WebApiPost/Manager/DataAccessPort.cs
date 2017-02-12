using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
                var obj = new LoginObject()
                {
                    Controller = "Office",
                    Action = "List",
                    Method = "GET"
                };


                var Json = JsonConvert.SerializeObject(obj);

                var content = new StringContent(Json, Encoding.UTF8, "application/json");

                if (!string.IsNullOrWhiteSpace(token))
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));
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
                var obj = new LoginObject()
                {
                    Controller = "Async",
                    Action = "GetCityList",
                    Method = "GET"
                };

                var Json = JsonConvert.SerializeObject(obj);

                var content = new StringContent(Json, Encoding.UTF8, "application/json");

                if (!string.IsNullOrWhiteSpace(token))
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));
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

        public List<CountryDto> CallCountyList(string token, AddOfficeDto getCityId)
        {
            
            var countryDtoList = new List<CountryDto>();
            using (var client = new HttpClient())
            {

                getCityId = new AddOfficeDto()
                {
                    CityId = getCityId.CityId
                };

                var jsonobj1 = JsonConvert.SerializeObject(getCityId);

                var obj = new LoginObject()
                {
                    Controller = "Async",
                    Action = "GetCityList",
                    Method = "GET",
                    Parameters = jsonobj1
                };

                var Json = JsonConvert.SerializeObject(obj);

                var content = new StringContent(Json, Encoding.UTF8, "application/json");

                if (!string.IsNullOrWhiteSpace(token))
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));
                }
                var response = client.PostAsync("http://appapi.anahtarfinans.com/api/route/execute", content).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                dynamic jsonobj = JsonConvert.DeserializeObject(result);

                var stateList = jsonobj.Content;

                foreach (var item in stateList)
                {
                    countryDtoList.Add(new CountryDto()
                    {
                        CountryName = item.Text,
                        CountryNumber = item.Value
                    });
                }


            }
            return countryDtoList;

        }
        public  AddOfficeDto AddNewOffice(string token, AddOfficeDto addOfficeDto)
        {

            
            using (var client = new HttpClient())
            {
                addOfficeDto = new AddOfficeDto()
                {
                    Id = addOfficeDto.Id,
                    OfficeName = addOfficeDto.OfficeName,
                    Phone1 = addOfficeDto.Phone1,
                    Address = addOfficeDto.Address,
                    OfficeStatusValue = addOfficeDto.OfficeStatusValue,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    CityId = addOfficeDto.CityId,
                    CountyId = addOfficeDto.CountyId
                };

                var jsonobj1 = JsonConvert.SerializeObject(addOfficeDto);
                
                var body = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Controller", "Office"),
                    new KeyValuePair<string, string>("Action", "AddOffice"),
                    new KeyValuePair<string, string>("Method", "POST"),
                    new KeyValuePair<string, string>("Paramaters",jsonobj1)
                   
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
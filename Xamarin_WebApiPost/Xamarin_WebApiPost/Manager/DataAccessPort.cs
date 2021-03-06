﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xamarin_WebApiPost.Manager;

namespace Xamarin_WebApiPost.Manager
{
    public class DataAccessPort
    {
        private AddOfficeLoginObject addOfficeLoginObject;
        private AddOfficeParameters AddOfficeParameters;
        private AddOfficeModel AddOfficeModel;
        public DataAccessPort()
        {
            AddOfficeModel = new AddOfficeModel();
        }
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


                var json = JsonConvert.SerializeObject(obj);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

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

        public List<CountyDto> CallCountyList(string token, int cityId)
        {

            var countyDtoList = new List<CountyDto>();
            using (var client = new HttpClient())
            {

                var obj = new CallCountyListLoginObject()
                {
                    Controller = "Async",
                    Action = "GetCountyListByCityId",
                    Method = "GET",
                    CountyListFilter = new CountyListFilter()
                    {
                        CityId = cityId
                    }
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
                    countyDtoList.Add(new CountyDto()
                    {
                        CountyName = item.Text,
                        CountyNumber = item.Value
                    });
                }


            }
            return countyDtoList;

        }
        public string AddNewOffice(string token, AddOfficeDto addOfficeDto)
        {
            using (var client = new HttpClient())
            {
                AddOfficeModel = new AddOfficeModel()
                {
                    Id = 0,
                    CityId = addOfficeDto.CityId,
                    OfficeName = addOfficeDto.OfficeName,
                    StartDate = DateTime.Now.ToString("d.M.yyyy hh:mm"),
                    EndDate = DateTime.Now.ToString("d.M.yyyy hh:mm"),
                    OfficeStatusValue = 1,
                    Phone1 = addOfficeDto.Phone1,
                    Address = addOfficeDto.Address,
                    CountyId = addOfficeDto.CountyId,
                    IsApi = true.ToString()
                };



                var modelConv = JsonConvert.SerializeObject(AddOfficeModel).Replace("\"", "\'");

                AddOfficeParameters = new AddOfficeParameters()
                {
                    Model = modelConv
                };
                //var addOfficeParConv = JsonConvert.SerializeObject(AddOfficeParameters);

                var obj = new AddOfficeLoginObject()
                {
                    Controller = "Office",
                    Action = "AddOffice",
                    Method = "POST",
                    AddOfficeListFilter = AddOfficeParameters
                };

                var addOfficeListFilter = JsonConvert.SerializeObject(obj).Replace("\\", String.Empty);


                var content = new StringContent(addOfficeListFilter, Encoding.UTF8, "application/json");

                if (!string.IsNullOrWhiteSpace(token))
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));
                }

                var response = client.PostAsync("http://appapi.anahtarfinans.com/api/route/execute", content).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
        }
    }
}
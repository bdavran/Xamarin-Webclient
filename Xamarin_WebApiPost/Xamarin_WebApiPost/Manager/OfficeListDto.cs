using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Xamarin_WebApiPost.Manager
{

    public class OfiiceListDto
    {        
        public string OfficeName { get; set; }
        public string PhoneCell { get; set; }
        public string Address { get; set; }
    }

    public class Token
    {
        public string AccessToken { get; set; }
    }

    public class AddOfficeDto
    {
        public int Id { get; set; }
        public string OfficeName { get; set; }
        public DateTime StartDate { get ; set; }
        public DateTime EndDate { get; set; }
        public string OfficeStatusValue { get; set; }
        public long Phone1 { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int CountyId { get; set; }

    }

    public class CityDto
    {
        public string CityName { get; set; }
        public int CityNumber { get; set; }

    }

    public class CountyDto
    {
        public string CountyName { get; set; }
        public int CountyNumber { get; set; }
    }

    public class CallCountyListLoginObject
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Method { get; set; }
        [JsonProperty(PropertyName = "Parameters")]
        public CountyListFilter CountyListFilter { get; set; }
    }

    public class AddOfficeLoginObject
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Method { get; set; }
        [JsonProperty(PropertyName = "Parameters")]
        public AddOfficeListFilter AddOfficeListFilter { get; set; }
    }

    public class LoginObject
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Method { get; set; }
        
    }

    public class AddOfficeListFilter
    {

        [JsonProperty(PropertyName = "cityId")]
        public int CityId { get; set; }
        [JsonProperty(PropertyName = "Id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "OfficeName")]
        public string OfficeName { get; set; }
        [JsonProperty(PropertyName = "StartDate")]
        public DateTime StartDate { get; set; }
        [JsonProperty(PropertyName = "EndDate")]
        public DateTime EndDate { get; set; }
        [JsonProperty(PropertyName = "OfficeStatusValue")]
        public string OfficeStatusValue { get; set; }
        [JsonProperty(PropertyName = "Phone1")]
        public long Phone1 { get; set; }
        [JsonProperty(PropertyName = "Address")]
        public string Address { get; set; }
        [JsonProperty(PropertyName = "countyId")]
        public int CountyId { get; set; }

    }

    public class CountyListFilter
    {
        [JsonProperty(PropertyName = "cityId")]
        public int CityId { get; set; }      
    }
}




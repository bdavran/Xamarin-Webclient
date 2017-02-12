using System;

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
        public int Phone1 { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int CountyId { get; set; }

    }

    public class CityDto
    {
        public string CityName { get; set; }
        public int CityNumber { get; set; }

    }

    public class CountryDto
    {
        public string CountryName { get; set; }
        public int CountryNumber { get; set; }
    }

    public class LoginObject
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Method { get; set; }
        public string Parameters { get; set; }
    }
}




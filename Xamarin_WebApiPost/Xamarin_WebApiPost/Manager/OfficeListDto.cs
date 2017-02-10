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
        public string Id { get; set; }
        public string OfficeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string OfficeStatusValue { get; set; }
        public bool IsApi { get; set; }

    }

    public class CityDto
    {
        public string CityName { get; set; }
        public int CityNumber { get; set; }

    }

}

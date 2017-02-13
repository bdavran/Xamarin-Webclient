using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin_WebApiPost.Manager;

namespace Xamarin_WebApiPost.TabbedPages
{
    public partial class GetCountyTab : ContentPage
    {
        private DataAccessPort dataAccessPort;
        public GetCountyTab()
        {
            Title = "Get State List";
            dataAccessPort = new DataAccessPort();
            var addOfficeDto = new AddOfficeDto();
            var getToken = dataAccessPort.GetToken();
            addOfficeDto.CityId = 34;


            Title = "County Tab";
            InitializeComponent();

            var callCountyList = dataAccessPort.CallCountyList(getToken, addOfficeDto.CityId);

            foreach (var countyDtoList in callCountyList)
            {
                lblabel.Text += string.Format("\n County Name : {0} \n County Number : {1} ", countyDtoList.CountyName, countyDtoList.CountyNumber);
            }

            var scroll = new ScrollView()
            {
                Content = lblabel
            };

            Content = scroll;
        }
    }
}

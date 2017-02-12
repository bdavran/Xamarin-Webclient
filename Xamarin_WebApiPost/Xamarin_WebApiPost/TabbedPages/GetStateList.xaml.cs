using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin_WebApiPost.Manager;

namespace Xamarin_WebApiPost.TabbedPages
{
    public partial class GetStateList : ContentPage
    {
        private DataAccessPort dataAccessPort;
        public GetStateList()
        {
            Title = "Get State List";
            dataAccessPort = new DataAccessPort();
            var addOfficeDto = new AddOfficeDto();
            var getToken = dataAccessPort.GetToken();
            addOfficeDto.CityId = 34;


            Title = "Ctiy Tab";
            InitializeComponent();

            var CallCityList = dataAccessPort.CallCityList(getToken);
            var CallStateList = dataAccessPort.CallCountyList(getToken, addOfficeDto);

            foreach (var stateDtoList in CallStateList)
            {
                lblabel.Text += string.Format("\n City Name : {0} \n City Number : {1} ", stateDtoList.CountryName, stateDtoList.CountryNumber);
            }

            var scroll = new ScrollView()
            {
                Content = lblabel
            };

            Content = scroll;
            InitializeComponent();
        }
    }
}

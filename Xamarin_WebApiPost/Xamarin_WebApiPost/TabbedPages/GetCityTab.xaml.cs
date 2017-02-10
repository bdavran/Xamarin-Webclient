using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin_WebApiPost.Manager;

namespace Xamarin_WebApiPost.TabbedPages
{
    public partial class GetCityTab : ContentPage
    {
        private DataAccessPort dataAccessPort;
        public GetCityTab()
        {
            dataAccessPort = new DataAccessPort();

            var getToken = dataAccessPort.GetToken();
            

            Title = "Ctiy Tab";
            InitializeComponent();

            var CallCityList = dataAccessPort.CallCityList(getToken);

            foreach (var cityDtoList in CallCityList)
            {
                lblabel.Text += string.Format("\n City Name : {0} \n City Number : {1} ", cityDtoList.CityName, cityDtoList.CityNumber);
            }

            var scroll = new ScrollView()
            {
                Content = lblabel
            };

            Content = scroll;
        }
    }
}

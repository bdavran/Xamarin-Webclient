using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin_WebApiPost.Manager;

using Xamarin.Forms;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Xamarin_WebApiPost.Views
{
    public partial class MyListPage : ContentPage
    {
        private DataAccessPort dataAccessPort;
        public MyListPage()
        {
            dataAccessPort = new DataAccessPort();

            var getToken = dataAccessPort.GetToken();
            InitializeComponent();

            var lblLabel = new Label();

            var callApi = dataAccessPort.CallOfficeList(getToken);

            foreach (var ofiiceListDto in callApi)
            {
                lblLabel.Text += string.Format("\n{0} \n{1} \n{2}", ofiiceListDto.OfficeName, ofiiceListDto.Address, ofiiceListDto.PhoneCell);
            }

            lblLabel.HorizontalOptions = LayoutOptions.Center;
            lblLabel.VerticalOptions = LayoutOptions.Center;


            Content = lblLabel;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin_WebApiPost.Manager;

namespace Xamarin_WebApiPost.TabbedPages
{
    public partial class GetListTab : ContentPage
    {
        private DataAccessPort dataAccessPort;
        public GetListTab()
        {
            Title = "Get List";
            dataAccessPort = new DataAccessPort();

            var getToken = dataAccessPort.GetToken();

            InitializeComponent();
            

            var CallOfficeList = dataAccessPort.CallOfficeList(getToken);

            foreach (var ofiiceListDto in CallOfficeList)
            {
                lblabel.Text += string.Format("\n Office Name : {0} \n Address : {1} \n Telephone Number : {2}", ofiiceListDto.OfficeName, ofiiceListDto.Address, ofiiceListDto.PhoneCell);
            }



            Content = lblabel;           
        }
    }
}

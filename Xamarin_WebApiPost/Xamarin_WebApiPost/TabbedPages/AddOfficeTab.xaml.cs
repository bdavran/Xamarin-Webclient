using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin_WebApiPost.Manager;

namespace Xamarin_WebApiPost.TabbedPages
{
    public partial class AddOfficeTab : ContentPage
    {
        private AddOfficeDto addOfficeDto;
        private DataAccessPort dataAccessPort;
        public AddOfficeTab()
        {
            addOfficeDto = new AddOfficeDto();
            dataAccessPort = new DataAccessPort();
            var getToken = dataAccessPort.GetToken();
            addOfficeDto = new AddOfficeDto()
            {
                CityId = 34,
                OfficeName = "Baros emlak3",
                OfficeStatusValue = "1",
                Phone1 = "5328974525",
                Address = "Bağdat Caddesi No:436 Daire:5 Bağlarbaşı",
                CountyId = 2049,
            };
            dataAccessPort.AddNewOffice(getToken,addOfficeDto);
            Title = "Add Office";
            InitializeComponent();
            

            Content = AddOfficeStackLayout;
        }

        private async void onCityChoosen(AddOfficeDto addOfficeDto , SelectedItemChangedEventArgs e)
        {
            var getToken = dataAccessPort.GetToken();
            var getCities = dataAccessPort.CallCityList(getToken);

            foreach (var item in getCities)
            {
                cityString += item.CityName;
            }
            var selectedCity = cityPicker.SelectedIndex;
            addOfficeDto.CityId = selectedCity;
            var getCounty = dataAccessPort.CallCountyList(getToken, selectedCity);

            foreach (var item in getCounty)
            {
                countyString += item.CountyName;
            }
            var selectedCounty = countyPicker.SelectedIndex;
            addOfficeDto.CountyId = selectedCounty;
        }



        private async void Button_OnClicked(object sender, EventArgs e)
        {

            

            var getToken = dataAccessPort.GetToken();
            var getCities = dataAccessPort.CallCityList(getToken);


            if (AddOfficeName.Text != null)
            {
                addOfficeDto.OfficeName = AddOfficeName.Text;
            }
            else
            {

            }
            if (AddPhone.Text != null)
            {
                addOfficeDto.Phone1 = AddPhone.Text;
            }
            else
            {

            }
            if (AddAddress.Text != null)
            {
                addOfficeDto.Address = AddAddress.Text;
            }
            else
            {

            }

            //onCityChoosen(addOfficeDto);
            dataAccessPort.AddNewOffice(getToken, addOfficeDto);
            AddButton.Clicked += delegate
            {
               dataAccessPort.AddNewOffice(getToken, addOfficeDto);
            };
            throw new NotImplementedException();
        }
    }
}

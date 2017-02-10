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
            dataAccessPort = new DataAccessPort();;
            Title = "Add Office";
            InitializeComponent();

            Content = EntryStackLayout;
        }





        private void Button_OnClicked(object sender, EventArgs e)
        {

            if (AddOfficeName.Text != null)
            {
                addOfficeDto.OfficeName = AddOfficeName.Text;     
            }
            else
            {
                
            }
            
            //addOfficeDto.StartDate = AddStartDate.Text;
            //addOfficeDto.EndDate = AddEndDate.Text;
            //addOfficeDto.OfficeStatusValue = AddOfficeStatus.Text;
            //addOfficeDto.IsApi = IsApi.IsEnabled;

            throw new NotImplementedException();
        }
    }
}

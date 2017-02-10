using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin_WebApiPost.TabbedPages;

namespace Xamarin_WebApiPost.Views
{
    public class MyTabbedPage : TabbedPage
    {
        public MyTabbedPage()
        {
            Children.Add(new GetListTab());
            Children.Add(new AddOfficeTab());
            Children.Add(new GetCityTab());
        }
    }
}

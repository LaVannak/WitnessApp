using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WitnessProject.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();
		}

        private void TapHelp_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HelpPage());
        }

        private void TapViewIncident_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new IncidentView());
        }

        private void TapNewIncident_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewIncidentPage());
        }

        private void TapMyIncident_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new IncidentView());
        }
    }
}
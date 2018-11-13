using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.Gms.Common.Apis;
using Java.Nio.FileNio.Attributes;
using Newtonsoft.Json;
using WitnessProject.Models;
using WitnessProject.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WitnessProject.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
	   
        public HomePage (string getEmail)
		{
			InitializeComponent ();
		    
           TbLogout.Text = getEmail;

            // Create new Witness when user load application
            // API will responsible for checking if any doublication Witness.
		    ApiServices apiServices = new ApiServices();
		    Witness witnes = new Witness() { Email = getEmail };

            apiServices.RegisterWitness(witnes);

            //Get witness ID next serivces;
            //The Id will assign to a static varible
		    getWitnessID(getEmail);

		}

        private async void getWitnessID(string email)
        {
            ApiServices apiServices = new ApiServices();
            var witnes = await apiServices.findWitness(email);

            TSvr.WId = witnes.Id;
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
            Navigation.PushAsync(new TabbedPage
            {
                Children =
                {
                    new IncidentListView(),
                    new IncidentMapView()
                }
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WitnessProject.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WitnessProject.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IncidentListView : ContentPage
	{
		public IncidentListView ()
		{
			InitializeComponent ();
            getIncident();
        }

        private async void getIncident()
        {
            try
            {
                HttpClient client = new HttpClient();

                var response = await client.GetStringAsync("https://witnesspro.azurewebsites.net/api/incidents");
                var incident = JsonConvert.DeserializeObject<List<Incident>>(response);

                lstIncident.ItemsSource = incident;

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }


        }
    }
}
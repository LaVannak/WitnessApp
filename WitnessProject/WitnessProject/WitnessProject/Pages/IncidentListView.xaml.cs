using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WitnessProject.Models;
using WitnessProject.Services;
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
                ApiServices apiServices = new ApiServices();
                var incidents = await apiServices.GetIncidentList();
                
                lstIncident.ItemsSource = incidents;

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }


        }
    }
}
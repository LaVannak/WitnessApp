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
	    private string _email;
	    private ObservableCollection<Witness> Witness;
       private ObservableCollection<Incident> Incidents;
	    private List<Incident> _incident;
	    public int j = 1;
        Witness mrwit;

        public HomePage (string getEmail)
		{
			InitializeComponent ();
		    _email = getEmail;
            
            ApiServices apiServices = new ApiServices();
            //Witness witnes = new Witness(){Email = _email};
            // Create new Witness when user load application
            // API will responsible for checking if any doublication Witness.
            //apiServices.RegisterWitness(witnes);
            //GetWitnesses();


            thiswork();
            


            //Witness = new ObservableCollection<Witness>();
            //Incidents = new ObservableCollection<Incident>();
            //TbLogout.Text = _email;
            //      GetWitnesses();

            //      Witness.Add(new Witness() { Id = 6, Email = "vannak@gmail.com" });
            //      int a = Witness.Count(x => x.Id ==7);

            //TbLogout.Text = j.ToString();

            ////TbLogout.Text = Witness.Exists(e => e.Email == "Vannak@gmail.com").ToString();

            ////TbLogout.Text = Witness.Count.ToString();

        }

        private async void thiswork()
        {
            ApiServices apiServices = new ApiServices();
            mrwit = await apiServices.findWitness(_email);
            

            TbLogout.Text = mrwit.Id.ToString();
        }

	    private async void GetWitnesses()
	    {
	        int i = 0;
	        ApiServices apiServices = new ApiServices();
	        var users = await apiServices.WitnessList();
	        foreach (var witness in users)
	        {
	            Witness.Add(witness);
	            j++;
                System.Console.WriteLine( j++);
	        }
	    }

        private void Testingabc()
        {
            j = 10;
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
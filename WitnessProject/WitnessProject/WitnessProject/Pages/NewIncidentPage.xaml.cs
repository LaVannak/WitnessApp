using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.Media;
using System.Threading.Tasks;
using Android.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WitnessProject.Helpers;
using Plugin.Media.Abstractions;
using WitnessProject.Models;
using WitnessProject.Services;
using Xamarin.Forms.Maps;

namespace WitnessProject.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewIncidentPage : ContentPage
	{
	    public MediaFile file;
	    private Witness witnes;


        public NewIncidentPage ()
		{
			InitializeComponent ();
            //Get Witness for updating the incident record of the Witness
            witnes = new Witness();

            getWitness(TSvr.WId);
		}


	    private async void TapOpenCamera_OnTapped(object sender, EventArgs e)
	    {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            imgCamImage.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }

	    private async void BtnSend_OnClicked(object sender, EventArgs e)
	    {
            
	        
	        //Get Image as Binary
            var imageArray = FilesHelper.ReadFully(file.GetStream());
            file.Dispose();
            //-----------------------------------------------------

            //Get Longitude and Latitude of the Location
            double lon = 0;
	        double lat = 0;
            Geocoder gc = new Geocoder();

	        IEnumerable<Position> result = await gc.GetPositionsForAddressAsync(txtLocation.Text);

	        foreach (var position in result)
	        {
	            lon = position.Longitude;
	            lat = position.Latitude;
	        }
            //-----------------------------------------------------

            //Convert Date Time to Int

	        DateTime datetime = DatePicker.Date;
	        int d = Convert.ToInt32(datetime.ToOADate());
            //-----------------------------------------------------

            //Upload incident into API
            var incident = new Incident()
            {
                Description = txtDescription.Text,
                Titile = txtTitle.Text,
                Location = txtLocation.Text,
                Longitude = lon,
                Latitude = lat,
                Date = d,
                ImageArray = imageArray,
                WitnesId = TSvr.WId
            };

            

            ApiServices apiServices = new ApiServices();
            var response = await apiServices.NewIncident(incident);
            if (!response)
            {
                DisplayAlert("Alert", "Somthing wrong", "Cancel");

            }
            else
            {
                //await DisplayAlert("Hi", "Your record has been added successfully", "Alright");
                Navigation.PushAsync(new IncidentView());
                // witnes.Incident.Add(incident);
                //var updated = await apiServices.UpdateWitness(witnes);
                // if (updated)
                // {
                //     await DisplayAlert("Hi", "Your record has been added successfully", "Alright");
                // }
                // else
                // {
                //     DisplayAlert("Alert", "Somthing wrong", "Cancel");
                // }

            }

        }

	    private async void getWitness(int Id)
	    {
	        ApiServices apiServices = new ApiServices();
	        witnes = await apiServices.GetWitness(Id);     
	    }
    }
}
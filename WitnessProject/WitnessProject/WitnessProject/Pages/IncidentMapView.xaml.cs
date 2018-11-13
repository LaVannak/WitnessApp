using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Hardware;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;
using Xamarin.Essentials;


namespace WitnessProject.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IncidentMapView : ContentPage
	{
	    public IncidentMapView()
	    {
	        InitializeComponent();

            var center = new Xamarin.Forms.Maps.Position(37.79752, 122.40188);
            var span = new Xamarin.Forms.Maps.MapSpan(center, 2, 2);
            MapView.MoveToRegion(span);

            var pin = new Pin()
            {
                Position = new Position(37.79752, 122.40188),
                Label = "My Pin"
            };

	        var pin2 = new Pin()
	        {
	            Position = new Position(37.78762, 122.30198),
	            Label = "My Pin"
	        };

            MapView.Pins.Add(pin);
	        MapView.Pins.Add(pin2);


        }
	}
}
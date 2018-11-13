using System;
using WitnessProject.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace WitnessProject
{
	public partial class App : Application
	{
        public static double ScreenWidth { get; internal set; }
        public static double ScreenHeight { get; internal set; }

        public App (string getEmail)
		{
			InitializeComponent();

            //MainPage = new NavigationPage (new SignInPage());
            MainPage = new NavigationPage(new HomePage(getEmail));
            //MainPage = new NavigationPage(new IncidentListView());
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

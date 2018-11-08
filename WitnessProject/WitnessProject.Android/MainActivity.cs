using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Essentials;
using Android.Content;
using Android.Accounts;
using Android;

namespace WitnessProject.Droid
{
    [Activity(Label = "WitnessProject", Icon = "@drawable/Icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private string email=null;

        private readonly string[] Permission =
        {
            Manifest.Permission.Internet,
            Manifest.Permission.GetAccounts,
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation,
            Manifest.Permission.AccessLocationExtraCommands,
            Manifest.Permission.AccessMockLocation,
            Manifest.Permission.AccessWifiState,
            Manifest.Permission.AccessNetworkState,
        };

        private const int RequestId = 0;
        private bool granted = false;


        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);
            RequestPermissions(Permission, RequestId);


            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::Xamarin.FormsMaps.Init(this, bundle);

            /*Because Xamarin Form started before the permistion granted; and
             the email variable is not assinged imidiately after permision granted;
             therefore we have loop the of getting email. This to ensure that the application
             will not start without user account*/

            //email = getEmail(this);
            while (email == null)
            {
                email = getEmail(this);

            }

            //Passing email to Xamarin form.
            //email = "vannak.la@gmail.com";
            LoadApplication(new App(email));
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            

        }

        static string getEmail(Context context)
        {
            AccountManager accManager = AccountManager.Get(context);
            Account account = getAccount(accManager);
            if (account == null)
            {
                return null;
            }
            else
            {
                return account.Name;
            }

        }

        private static Account getAccount(AccountManager accManagner)
        {
            //Account[] accounts = accManagner.GetAccountsByType("com.google");
            Account[] accounts = accManagner.GetAccounts();
            Account account;
            if (accounts.Length > 0)
            {
                account = accounts[0];
            }
            else
            {
                account = null;
            }
            return account;

        }
    }
}


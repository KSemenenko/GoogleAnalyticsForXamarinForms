using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Plugin.GoogleAnalytics;
using Xamarin.Forms;

namespace TestFormsApp
{
    public class App : Application
    {
        public App()
        {
            GAServiceManager.Current.PayloadSent += delegate (object s, PayloadSentEventArgs ev)
            {
                Debug.WriteLine($"Payload sent! Response:\n{ev.Response}");
            };

            GAServiceManager.Current.PayloadFailed += delegate (object s, PayloadFailedEventArgs ev)
            {
                Debug.WriteLine($"Payload Failed! Error: {ev.Error}");
            };

            GAServiceManager.Current.PayloadMalformed += delegate (object s, PayloadMalformedEventArgs ev)
            {
                Debug.WriteLine($"Payload Malformed! HttpStatusCode: {ev.HttpStatusCode}");
            };

           // B_Clicked(null, null);
            var button = new Button();
            button.Text = "ClickMe";
            button.Clicked += B_Clicked;

            // The root page of your application
            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        new Label
                        {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "Welcome to Xamarin Forms!"
                        },
                        button,
                    }
                }
            };
        }

        private void B_Clicked(object sender, EventArgs e)
        {

            GoogleAnalytics.Current.Config.TrackingId = "UA-11111111-1";
            GoogleAnalytics.Current.Config.AppId = "AppID";
            GoogleAnalytics.Current.Config.AppName = "TEST";
            //GoogleAnalytics.Current.Config.Debug = true;
            GoogleAnalytics.Current.InitTracker();

            try
            {
                GoogleAnalytics.Current.Tracker.SendView("MainPage");
                GoogleAnalytics.Current.Tracker.SendException(new Exception("oops"), false);
            }
            catch(Exception ex)
            {
                int a = 5;
            }

            throw  new Exception("ex");
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
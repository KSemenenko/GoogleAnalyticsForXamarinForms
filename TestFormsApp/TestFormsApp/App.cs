using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoogleAnalytics;
using GoogleAnalytics.Core;
using Xamarin.Forms;


namespace TestFormsApp
{
    public class App : Application
    {
        public App ()
        {

            var button = new Button();
            button.Text = "ClickMe";
            button.Clicked += B_Clicked;

            // The root page of your application
            MainPage = new ContentPage {
                Content = new StackLayout {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Label {
                            XAlign = TextAlignment.Center,
                            Text = "Welcome to Xamarin Forms!"
                        },
                       button,
                    }
                }
            };
        }

        private void B_Clicked(object sender, EventArgs e)
        {

        
            var config = new TrackerConfig();
            config.AppVersion = "1.0.0.0";
            config.TrackingId = "UA-11111111-1";
            config.AppId = "AppID";
            config.AppName = "TEST";
            config.AppInstallerId = Guid.NewGuid().ToString();

            config.Debug = true;

            TrackerFactory.Config = config;

            Tracker tracker;

            try
            {
                tracker = new TrackerFactory().GetTracker();
                tracker.SendView("MainPage");
            }
            catch (Exception ex)
            {

                int a = 5;
            }

            
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

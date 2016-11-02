using System;
using System.Diagnostics;
using Plugin.GoogleAnalytics;
using Xamarin.Forms;

namespace TestFormsApp
{
    public class App : Application
    {
        public App()
        {
            GAServiceManager.Current.PayloadSent += delegate(object s, PayloadSentEventArgs ev) { Debug.WriteLine($"Payload sent! Response:{ev.Response}"); };

            GAServiceManager.Current.PayloadFailed += delegate(object s, PayloadFailedEventArgs ev) { Debug.WriteLine($"Payload Failed! Error: {ev.Error}"); };

            GAServiceManager.Current.PayloadMalformed += delegate(object s, PayloadMalformedEventArgs ev) { Debug.WriteLine($"Payload Malformed! HttpStatusCode: {ev.HttpStatusCode}"); };

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
                Debug.WriteLine("Start");
                Stopwatch sw = new Stopwatch();
                sw.Start();
                GoogleAnalytics.Current.Tracker.SendView("MainPage");
                sw.Stop();
                Debug.WriteLine("SendView: " + sw.Elapsed);
                sw.Restart();
                GoogleAnalytics.Current.Tracker.SendException(new Exception("oops"), false);
                sw.Stop();
                Debug.WriteLine("SendException: " + sw.Elapsed);

                var t = GoogleAnalytics.Current.Tracker;

                sw.Restart();
                Exception exe1 = new Exception("1");
                Exception exe2 = new Exception("2", exe1);
                Exception exe3 = new Exception("3", exe2);
                GoogleAnalytics.Current.Tracker.SendException(exe3, false);
                sw.Stop();
                Debug.WriteLine("SendException and Inner: " + sw.Elapsed);
                sw.Reset();

                try
                {
                    sw.Start();
                    GoogleAnalytics.Current.Tracker.SendView("MainPage");
                    GoogleAnalytics.Current.Tracker.SendEvent("EventCategory1", "Error1");
                    string s = null;
                    string a = s.Substring(2);
                }
                catch(Exception ex)
                {
                    GoogleAnalytics.Current.Tracker.SendException(ex, false);
                    sw.Stop();
                    Debug.WriteLine("SendException from catch: " + sw.Elapsed);
                }
            }
            catch(Exception ex)
            {
                int a = 5;
            }

            //  throw  new Exception("ex");
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
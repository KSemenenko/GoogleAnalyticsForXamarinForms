
using Plugin.GoogleAnalytics.Abstractions;

#if ANDROID
using Android.Runtime;
#endif

#if __IOS__ || __MACOS__
using Foundation;
#endif

namespace Plugin.GoogleAnalytics
{
#if !WINDOWS_UWP
    [Preserve(AllMembers = true)]
#endif
    public class AnalyticsEngine
    {
        private const string Key_AppOptOut = "GoogleAnaltyics.AppOptOut";

        private readonly TrackerManager manager;

        private static AnalyticsEngine current;

        public static AnalyticsEngine Current
        {
            get
            {
                if(current == null)
                {
                    current = new AnalyticsEngine();
                }
                return current;
            }
        }

        protected AnalyticsEngine()
        {
            manager = new TrackerManager(new PlatformInfoProvider());
        }

        private bool isAppOptOutSet;

        public bool AppOptOut
        {
            get
            {
                if(!isAppOptOutSet)
                {
                    LoadAppOptOut();
                }
                return manager.AppOptOut;
            }
            set
            {
                manager.AppOptOut = value;
                isAppOptOutSet = true;

                //ApplicationData.Current.LocalSettings.Values[Key_AppOptOut] = value;
                //IsolatedStorageSettings.ApplicationSettings[Key_AppOptOut] = value;

                if(value)
                {
                    GAServiceManager.Current.Clear();
                }
            }
        }

        private void LoadAppOptOut()
        {
            //if (ApplicationData.Current.LocalSettings.Values.ContainsKey(Key_AppOptOut))
            //{
            //    manager.AppOptOut = (bool)ApplicationData.Current.LocalSettings.Values[Key_AppOptOut];
            //}

            //if (IsolatedStorageSettings.ApplicationSettings.Contains(Key_AppOptOut))
            //{
            //    manager.AppOptOut = (bool)IsolatedStorageSettings.ApplicationSettings[Key_AppOptOut];
            //}
            // else
            {
                manager.AppOptOut = false;
            }
            isAppOptOutSet = true;
        }

        //public IAsyncOperation<bool> RequestAppOptOutAsync()
        //{
        //    return _RequestAppOptOutAsync().AsAsyncOperation();
        //}

        //async Task<bool> _RequestAppOptOutAsync()
        //{
        //    MessageDialog msgDialog = new MessageDialog("Allow anonomous information to be collected to help improve this application?", "Help Improve User Experience");
        //    var optInCommand = new UICommand("Yes");
        //    var optOutCommand = new UICommand("No");
        //    msgDialog.Commands.Add(optInCommand);
        //    msgDialog.Commands.Add(optOutCommand);
        //    var dialogResult = await msgDialog.ShowAsync();
        //    var result = (dialogResult != optInCommand);
        //    AppOptOut = result;
        //    return result;
        //}

        //public bool RequestAppOptOutAsync()
        //{
        //    //var dialogResult = MessageBox.Show("Allow anonomous information to be collected to help improve this application?", "Help Improve User Experience", MessageBoxButton.OKCancel);
        //    //var result = (dialogResult == MessageBoxResult.Cancel);
        //    //AppOptOut = result;
        //    //return result;
        //    return false;
        //}

        public bool IsDebugEnabled
        {
            get { return manager.IsDebugEnabled; }
            set { manager.IsDebugEnabled = value; }
        }

        public Tracker GetTracker(string propertyId)
        {
            return manager.GetTracker(propertyId);
        }

        public void CloseTracker(Tracker tracker)
        {
            manager.CloseTracker(tracker);
        }

        public Tracker DefaultTracker
        {
            get { return manager.DefaultTracker; }
            set { manager.DefaultTracker = value; }
        }

        public IPlatformInfoProvider PlatformInfoProvider
        {
            get { return manager.PlatformTrackingInfo; }
        }
    }
}
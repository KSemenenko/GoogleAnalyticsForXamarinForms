# Google Analytics for Xamarin Forms
This project is a cross platform library for Xamarin Forms, which enables a handy use of Google Analytics in your applications.  
I've decided that there should be a broad-based library with no connection to native libraries Google in Xamarin (too long to install). 

Should you have any comments or suggestions, please let me know. Let's make it an easy-to-use tool for our projects.

### Additional thanks to the project that inspired me:
* Google Analytics SDK for Windows and Windows Phone (https://googleanalyticssdk.codeplex.com/)
* jamesmontemagno/Xamarin.Plugins (https://github.com/jamesmontemagno/Xamarin.Plugins)

## Available at NuGet. 
https://www.nuget.org/packages/ksemenenko.GoogleAnalytics/

## Permission
#### Android:
`
android.permission.INTERNET
android.permission.ACCESS_NETWORK_STATE
`

#### iOS:
`
*
`

#### WP/UWP:
`
*
`

## Example use:

#### Init:
```cs

var config = new TrackerConfig();
config.AppVersion = "1.0.0.0";
config.TrackingId = "UA-11111111-1";
config.AppId = "AppID";
config.AppName = "Google Analytics Test";
config.AppInstallerId = Guid.NewGuid().ToString();
//config.Debug = true;
TrackerFactory.Config = config;
            
var tracker = new TrackerFactory().GetTracker();
tracker.SendView("MainPage");
```

#### Track:
```cs
tracker.SendView("MainPage");
tracker.SendEvent("Category", "Action", "Label", 1);
tracker.SendEvent("Category", "Action");
```

#### Other way:
```cs
var trackerManager = new TrackerManager(new PlatformInfoProvider()
{
    ScreenResolution = new Dimensions(1920, 1080),
    UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko",
    UserLanguage = "en-us",
    ViewPortResolution = new Dimensions(1920, 1080)
});

tracker = trackerManager.GetTracker("UA-11111111-1"); 
tracker.AppName = "My app";
tracker.AppVersion = "1.0.0.0";

// Log something to Google Analytics
tracker.SendView("MainPage");
```
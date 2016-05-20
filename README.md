# Google Analytics for Xamarin Forms

This project is  a cross platform library for Xamarin Forms, which enables a handy use of Google Analytics in your applications.  
I've decided that rhere should be a broad-based library with no connection to native libraries Google in Xamarin (too long to install). 

Should you have any comments or suggestions, please let me know. Let's make it an easy-to-use tool for uour projects.

### Additional thanks to the project that inspired me:
* Google Analytics SDK for Windows and Windows Phone (https://googleanalyticssdk.codeplex.com/)
* XLabs/Xamarin-Forms-Labs (https://github.com/XLabs/Xamarin-Forms-Labs)
* jamesmontemagno/Xamarin.Plugins (https://github.com/jamesmontemagno/Xamarin.Plugins)

## Available at NuGet. 
TDB

### Example use:
#### Init:
```cs

//TDB


```

#### Track:
```cs

//TDB


```

#### Other:
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

// Log something to GA
tracker.SendView("MainPage");
```
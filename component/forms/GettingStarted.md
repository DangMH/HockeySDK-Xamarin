### PCL Projects

## Configuration & Initialization
Configuration and Initialization must be done in platform specific projects using the respective bindings sdks.  Please refer to `iOS Projects` and `Android Projects` below.

## CrashManager

You can check if a crash occured in the last session by adding the following lines:

```csharp
using HockeyApp;
```

```csharp
CrashManager.DidCrashCrashInLastSession;
```

## MetricsManager

You can enable/disable the  MetricsManager and track custom events using the following lines:

```csharp
using HockeyApp;
```

```csharp
MetricsManager.Disabled = false; // Default when MetricsManager is initialized
MetricsManager.TrackEvent("Your-Event-Name");
```

### iOS Projects

## Obtain an App Identifier

Please see the [How to create a new app](http://support.hockeyapp.net/kb/about-general-faq/how-to-create-a-new-app) tutorial. This will provide you with a HockeyApp specific App Identifier to be used to initialize the SDK.

## Add crash reporting

This will add crash reporting capabilities to your app. 

Open your AppDelegate.cs file, and add the following lines:

```csharp
using HockeyApp.iOS;

namespace YourNameSpace
{
	[Register("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			var manager = BITHockeyManager.SharedHockeyManager;
			manager.Configure("Your_App_Id");
			manager.StartManager();
		}
	}
}
```


## Add Update Distribution

This will add the in-app update mechanism to your app.

The feature handles version updates, presents update and version information in an App Store like user interface, collects usage information and provides additional authorization options when using Ad-Hoc provisioning profiles.

This module automatically disables itself when running in an App Store build by default!

This feature can be disabled manually as follows:

```csharp
var manager = BITHockeyManagerSharedHockeyManager;
manager.Configure("Your_App_Id");
manager.SetDisableUpdateManager = true;
manager.StartManager();
```

If you want to see beta analytics, use the beta distribution feature with in-app updates, restrict versions to specific users, or want to know who is actually testing your app, you need to follow the instructions on our guide [Authenticating Users on iOS](https://support.hockeyapp.net/kb/client-integration-ios-mac-os-x-tvos/authenticating-users-on-ios)




## Add in-app feedback

This will add the ability for your users to provide feedback from right inside your app.

```csharp
var feedbackManager = BITHockeyManager.SharedHockeyManager.FeedbackManager;

// Show current feedback
feedbackManager.ShowFeedbackListView();

// Send new feedback
feedbackManager.ShowFeedbackComposeView();
```



## Add authentication

Instructions for iOS Authentication can be found [here](https://support.hockeyapp.net/kb/client-integration-ios-mac-os-x-tvos/authenticating-users-on-ios)



## Control logging output

You can control the amount of log messages from HockeySDK.  By default, we keep the noise as low as possible, only errors will show up. To enable additional logging, i.e. while debugging, add the following line of code:

```csharp
var manager = BITHockeyManager.SharedHockeyManager;
manager.Configure("Your_App_Id");
manager.SetDebugLogEnabled = true;
manager.StartManager();
```


## More Information

For more information, see the [HockeySDK for Xamarin Source Repository](https://github.com/bitstadium/HockeySDK-Xamarin)

### Android Projects

## Obtain an App Identifier

Please see the [How to create a new app](http://support.hockeyapp.net/kb/about-general-faq/how-to-create-a-new-app) tutorial. This will provide you with a HockeyApp specific App Identifier to be used to initialize the SDK.

## Add crash reporting

This will add crash reporting capabilities to your app.

In your `MainActivity.cs` file, add the following lines:

```csharp
using HockeyApp.Android;

namespace YourNameSpace
{
	[Activity(Label = "Your.App", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity 
	{
		protected override void OnCreate(Bundle savedInstanceState) 
		{
			base.OnCreate(savedInstanceState);

			// ... your own OnCreate implementation
			CrashManager.Register(this, "Your-App-Id");
		}
	}
}
```

When the app is resumed, the crash manager is triggered and checks if a new crash was created before. If a previous crash is detected, it presents a dialog to ask the user whether they want to send the crash log to HockeyApp. On app launch the crash manager registers a new exception handler to recognize app crashes.

## Add AppId to manifest

Add the following assembly level attribute in `Properties/AssemblyInfo.cs`

```csharp
[assembly: MetaData ("net.hockeyapp.android.appIdentifier", Value="Your-Api-Key")]
```

This will allow you to set your AppId once and simplify register calls

```csharp
using HockeyApp.Android;

namespace YourNameSpace
{
	[Activity(Label = "Your.App", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity 
	{
		protected override void OnCreate(Bundle savedInstanceState) 
		{
			base.OnCreate(savedInstanceState);

			// ... your own OnCreate implementation
			CrashManager.Register(this);
		}
	}
}
```

## Add Update Distribution

This will add the in-app update mechanism to your app. 

Open the activity where you want to inform the user about eventual updates. We'll assume you want to do this on startup of your main activity.

Add the following lines and make sure to always balance `Register(...)` calls to SDK managers with `Unregister()` calls in the corresponding lifecycle callbacks:

```csharp
using HockeyApp.Android;

namespace YourNameSpace 
{
	[Activity(Label = "Your.App", MainLauncher = true, Icon = "@mipmap/icon")]
	public class YourActivity : Activity 
	{
		protected override void OnCreate(Bundle savedInstanceState) 
		{
			base.OnCreate(savedInstanceState);
	
			// Your own code to create the view
			// ...
   
			CheckForUpdates();
		}

		void CheckForUpdates()
		{
			// Remove this for store builds!
			UpdateManager.Register(this, "Your_App_Id");
		}

		void UnregisterManagers() 
		{
			UpdateManager.Unregister();
		}

		protected override void OnPause() 
		{
			base.OnPause();
			
			UnregisterManagers();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			
			UnregisterManagers();
		}
	}
}
```

When the activity is created, the update manager checks for new updates in the background. If it finds a new update, an alert dialog is shown and if the user presses Show, they will be taken to the update activity. The reason to only do this once upon creation is that the update check causes network traffic and therefore potential costs for your users.



## Add in-app feedback

This will add the ability for your users to provide feedback from right inside your app. 

You'll typically only want to show the feedback interface upon user interaction, for this example we assume you have a button `feedback_button` in your view for this.

Add the following lines to your respective activity, handling the touch events and showing the feedback interface:

```csharp
using HockeyApp.Android;

namespace YourNameSpace
{
	public class YourActivity : Activitiy
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
   			// Your own code to create the view
			// ...

			FeedbackManager.Register(this, "Your-App-Id");

			var feedbackButton = FindViewById<Button>(Resource.Id.feedback_button);

			feedbackButton.Click += delegate {
				FeedbackManager.ShowFeedbackActivity(ApplicationContext);
			});
		}
	}
}
```

When the user taps on the feedback button it will launch the feedback interface of the HockeySDK, where the user can create a new feedback discussion, add screenshots or other files for reference, and act on their previous feedback conversations.


## Add authentication

You can force authentication of your users through the `LoginManager` class. This will show a login screen to users if they are not fully authenticated to protect your app.

Retrieve your app secret from the HockeyApp backend. You can find this on the app details page in the backend right next to the ***App ID*** value. Click ***Show*** to access it.

Open the activity you want to protect, if you want to protect all of your app this will be your main activity.

Add the following lines to this activity:
using HockeyApp.Android;

```csharp
namespace YourNameSpace
{
	[Activity(Label = "Your.App", MainLauncher = true, Icon = "@mipmap/icon")]
	public class YourActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			// Your own code to create the view
			// ...
			
			LoginManager.Register(this, APP_SECRET, 				LoginManager.LOGIN_MODE_EMAIL_PASSWORD);
				LoginManager.VerifyLogin(this, Intent);
		}
	}
}
```

Make sure to replace ***APP_SECRET*** with the value retrieved in the first step. This will launch the login activity every time a user launches your app.



### Permissions

Permissions get automatically merged into your manifest. If your app does not use update distribution you might consider removing the ***Write External Storage*** permission.


### Control output to LogCat

You can control the amount of log messages from HockeySDK that show up in LogCat. By default, we keep the noise as low as possible, only errors will show up. To enable additional logging, i.e. while debugging, add the following line of code:

```csharp
HockeyLog.LogLevel(3);
```

The different log levels match Android's own log levels.

```csharp
HockeyLog.LogLevel(2); // Verbose, show all log statements
HockeyLog.LogLevel(3); // Debug, show most log statements – useful for debugging
HockeyLog.LogLevel(4); // Info, show informative or higher log messages
HockeyLog.LogLevel(5); // Warn, show warnings and errors
HockeyLog.LogLevel(6); // Error, show only errors – the default log level
```

## More Information

For more information, see the [HockeySDK for Xamarin Source Repository](https://github.com/bitstadium/HockeySDK-Xamarin)

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using MauiAppServiceIssue.Platforms.Android;

namespace MauiAppServiceIssue
{
    [Activity(Theme = "@style/Maui.SplashTheme", 
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        internal static readonly string Channel_ID = "AppChannel";
        internal static readonly int NotificationID = 10414;
    }
}

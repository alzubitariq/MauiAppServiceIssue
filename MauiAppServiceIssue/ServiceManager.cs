using Android.Content;
using MauiAppServiceIssue.Platforms.Android;
using Android.App;
using Android.Media;

namespace MauiAppServiceIssue.Platforms
{
    public class ServiceManager : IServiceManager
    {
        private bool IsServiceRunning(Context context, Type serviceClass)
        {
            ActivityManager manager = (ActivityManager)context.GetSystemService(Context.ActivityService);
            foreach (var service in manager.GetRunningServices(int.MaxValue))
            {
                if (service.Process == context.PackageName && service.Service.ClassName.EndsWith(serviceClass.Name))
                {
                    return true;
                }
            }
            return false;
        }
        private void CreateNotificationChannel()
        {
            //if (OperatingSystem.IsAndroidVersionAtLeast(33))
            //{
            //    if (ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.PostNotifications) ==
            //        Permission.Denied)
            //    {
            //        ActivityCompat.RequestPermissions(this, new String[] {
            //            Android.Manifest.Permission.PostNotifications }, 1);
            //    }
            //}

            if (OperatingSystem.IsAndroidVersionAtLeast(26))
            {
                var notificationManager = (NotificationManager)
                    Platform.CurrentActivity.GetSystemService(global::Android.Content.Context.NotificationService);

                if (notificationManager.GetNotificationChannel(MainActivity.Channel_ID) == null)
                {
                    var channel = new NotificationChannel(MainActivity.Channel_ID, "NotificationChannel",
                        NotificationImportance.High);

                    channel.LockscreenVisibility = NotificationVisibility.Public;
                    channel.EnableLights(true);
                    channel.EnableVibration(true);
                    notificationManager.CreateNotificationChannel(channel);
                }
            }
        }

        public void Start()
        {
            CreateNotificationChannel();

            if (!IsServiceRunning(Platform.CurrentActivity.ApplicationContext, typeof(MyForegroundService)))
            {
                var intent = new Intent(global::Android.App.Application.Context,
                    typeof(MyForegroundService));

                if (global::Android.OS.Build.VERSION.SdkInt >= global::Android.OS.BuildVersionCodes.O)
                {
                    Platform.CurrentActivity.StartForegroundService(intent);
                }
                else
                {
                    Platform.CurrentActivity.StartService(intent);
                }
            }
        }

        public void Stop()
        {
           
        }
    }
}

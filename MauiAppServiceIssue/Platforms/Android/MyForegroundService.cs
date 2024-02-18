using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppServiceIssue.Platforms.Android
{
    [Service()]
    public class MyForegroundService : Service
    {
        // This is any integer value unique to the application.
        private const int SERVICE_RUNNING_NOTIFICATION_ID = 10000;
        private CancellationToken token = new CancellationToken();
        public override IBinder? OnBind(Intent? intent)
        {
            return null;
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent? intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            // Code not directly related to publishing the notification has been omitted for clarity.
            // Normally, this method would hold the code to be run when the service is started.

            var notification = new Notification.Builder(this)
                .SetContentTitle("Test")
                .SetContentText("Test Service")
                .SetSmallIcon(Resource.Mipmap.appicon)
                .SetContentIntent(BuildIntentToShowMainActivity())
                .SetOngoing(true)
                .SetChannelId(MainActivity.Channel_ID)
                //.AddAction(BuildRestartTimerAction())
                //.AddAction(BuildStopServiceAction())
                .Build();

            // Enlist this instance of the service as a foreground service
            StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, notification);

            return base.OnStartCommand(intent, flags, startId);
        }

        private PendingIntent? BuildIntentToShowMainActivity()
        {
            var notificationIntent = new Intent(this, typeof(MainActivity));
            notificationIntent.AddFlags(ActivityFlags.ClearTop);
            notificationIntent.AddFlags(ActivityFlags.SingleTop);

            var pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent,
                PendingIntentFlags.OneShot |
                PendingIntentFlags.Immutable |
                PendingIntentFlags.UpdateCurrent);

            return pendingIntent;
        }
    }
}

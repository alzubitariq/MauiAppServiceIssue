using Microsoft.Maui.Controls.PlatformConfiguration;

namespace MauiAppServiceIssue
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }
        private async Task<bool> RequestLocationPermission()
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.PostNotifications>();

            if (status != PermissionStatus.Granted)
                status = await Permissions.RequestAsync<Permissions.PostNotifications>();

            return status == PermissionStatus.Granted;
        }
        private async void MainPage_Loaded(object? sender, EventArgs e)
        {
#if ANDROID
            if (await RequestLocationPermission())
            {
                Application.Current.MainPage.Handler.MauiContext.Services
                    .GetService<IServiceManager>().Start();
            }
#endif
        }
    }
}

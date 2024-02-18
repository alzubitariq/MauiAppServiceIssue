using Microsoft.Extensions.Logging;

#if ANDROID
using MauiAppServiceIssue.Platforms;
#endif
namespace MauiAppServiceIssue
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

#if ANDROID
            builder.Services.AddSingleton<IServiceManager, ServiceManager>();
#endif

            return builder.Build();
        }
    }
}

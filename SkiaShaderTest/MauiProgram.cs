using Microsoft.Extensions.Logging;

namespace SkiaShaderTest;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
		    .ConfigureMauiHandlers((handlers) =>
                {
                    handlers.AddHandler(typeof(Controls.Panel), typeof(SkiaSharp.Views.Maui.Handlers.SKCanvasViewHandler));
                });
#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<MainPage>();
        return builder.Build();
	}
}


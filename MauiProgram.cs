using Microsoft.Extensions.Logging;

using TAREA1.DataAccess;
using TAREA1.View;
using TAREA1.ViewModels;

namespace TAREA1
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            var DbContext = new PDbContext();
            DbContext.Database.EnsureCreated();
            DbContext.Dispose();

            builder.Services.AddDbContext<PDbContext>();

            builder.Services.AddTransient<PersonaPage>();
            builder.Services.AddTransient<PersonaViewModel>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

            Routing.RegisterRoute(nameof(PersonaPage), typeof(PersonaPage));
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
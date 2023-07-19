using Microsoft.Extensions.Logging;
using RisksReports.Data;
using RisksReports.Views;

namespace RisksReports {
	public static class MauiProgram {
		public static MauiApp CreateMauiApp() {
			var builder = MauiApp.CreateBuilder();
			builder.UseMauiApp<App>()
				.ConfigureFonts(fonts => {
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

			builder.Services.AddSingleton<RisksReportsView>();
			builder.Services.AddTransient<RiskReportView>();

			builder.Services.AddSingleton<DatabaseManager>();

#if DEBUG
			builder.Logging.AddDebug();
#endif

			return builder.Build();
		}
	}
}
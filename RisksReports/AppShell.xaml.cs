using RisksReports.Views;

namespace RisksReports {
	public partial class AppShell : Shell {
		public AppShell() {
			InitializeComponent();
			Routing.RegisterRoute(nameof(RiskReportView), typeof(RiskReportView));
		}
	}
}
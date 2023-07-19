//using CloudKit;
using RisksReports.Data;
using RisksReports.Models;

namespace RisksReports.Views;

[QueryProperty("Item", "Item")]
public partial class RiskReportView : ContentPage
{
	private DatabaseManager databaseManager;

	public RiskReport Item {
		get => BindingContext as RiskReport;
		set => BindingContext = value;
	}

	public List<SeverityLevel> SeverityLevels => new List<SeverityLevel>((IEnumerable<SeverityLevel>)Enum.GetValues(typeof(SeverityLevel)));

	public List<ProbabilityLevel> ProbabilityLevels => new List<ProbabilityLevel>((IEnumerable<ProbabilityLevel>)Enum.GetValues(typeof(ProbabilityLevel)));

	public RiskReportView(DatabaseManager databaseManager)
	{
		InitializeComponent();
		this.databaseManager = databaseManager;
	}

	async void OnSaveClicked(object sender, EventArgs e) {
		if (string.IsNullOrWhiteSpace(Item.Title)) {
			await DisplayAlert("Title Required", "Please enter a title for the risk report item.", "OK");
			return;
		}

		await databaseManager.SaveItemAsync(Item);
		await Shell.Current.GoToAsync("..");
	}

	async void OnDeleteClicked(object sender, EventArgs e) {
		if (Item.Id <= 0)
			return;
		await databaseManager.DeleteItemAsync(Item);
		await Shell.Current.GoToAsync("..");
	}

	async void OnCancelClicked(object sender, EventArgs e) {
		await Shell.Current.GoToAsync("..");
	}
}
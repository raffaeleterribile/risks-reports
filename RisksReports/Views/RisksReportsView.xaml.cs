// using CloudKit;
using RisksReports.Data;
using RisksReports.Models;
using System.Collections.ObjectModel;

namespace RisksReports.Views;

public partial class RisksReportsView : ContentPage
{
	private DatabaseManager databaseManager;

	public ObservableCollection<RiskReport> Items { get; set; } = new();

	public RisksReportsView(DatabaseManager databaseManager)
	{
		InitializeComponent();

		this.databaseManager = databaseManager;
		BindingContext = this;
	}

	protected override async void OnNavigatedTo(NavigatedToEventArgs args) {
		base.OnNavigatedTo(args);

		var items = await databaseManager.GetItemsAsync();
		MainThread.BeginInvokeOnMainThread(() => {
			Items.Clear();
			foreach (var item in items)
				Items.Add(item);
		});
	}
	async void OnItemAdded(object sender, EventArgs e) {
		await Shell.Current.GoToAsync(nameof(RiskReportView), true, new Dictionary<string, object> {
			[nameof(RiskReportView.Item)] = new RiskReport()
		});
	}

	private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
		if (e.CurrentSelection.FirstOrDefault() is not RiskReport item)
			return;

		await Shell.Current.GoToAsync(nameof(RiskReportView), true, new Dictionary<string, object> {
			[nameof(RiskReportView.Item)] = item
		});
	}
}
using MauiAppXaml.Views;

namespace MauiAppXaml;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();

		//Button button = new Button()
		//{
		//	Text = "Navigate!",
		//	HorizontalOptions = LayoutOptions.Center,
		//	VerticalOptions = LayoutOptions.Center
		//};

		//button.Clicked += async (sender, args) =>
		//{
		//	await Navigation.PushAsync(new TestPage());
		//};

		//Content = button;
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

	private async void OnNavigateGridClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new GridPage());
    }

    private async void OnNavigateTestClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TestPage());
    }
}


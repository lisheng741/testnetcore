namespace MauiAppXaml.Views;

public partial class TestPage : ContentPage
{
	public TestPage()
	{
		InitializeComponent();
	}

	void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
	{
		valueLabel.Text = e.NewValue.ToString("F3");
	}

	async void OnButtonClicked(object sender, EventArgs e)
	{
		Button button = sender as Button;
		await DisplayAlert("Clicked!", $"The button labeled '{button.Text}'", "OK");
	}
}
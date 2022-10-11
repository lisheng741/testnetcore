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
		if (OperatingSystem.IsAndroid())
        {
            await DisplayAlert("Clicked!", $"Android! The button labeled '{button.Text}'", "OK");
        }
        else if (OperatingSystem.IsWindows())
        {
            await DisplayAlert("Clicked!", $"Windows! The button labeled '{button.Text}'", "OK");
        }
		else
        {
            await DisplayAlert("Clicked!", $"Other! The button labeled '{button.Text}'", "OK");
        }
	}
}
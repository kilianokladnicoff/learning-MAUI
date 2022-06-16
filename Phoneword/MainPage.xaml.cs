namespace Phoneword;

public partial class MainPage : ContentPage
{
	string translatedNumber;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnTranslateClicked(object sender, EventArgs e)
    {
		string enteredNumber = PhoneNumberText.Text;
		translatedNumber = PhonewordTranslator.ToNumber(enteredNumber);

		if (!string.IsNullOrEmpty(translatedNumber))
        {
			CallBtn.IsEnabled = true;
			CallBtn.Text = $"Call {translatedNumber}";
        }
		else
        {
			CallBtn.IsEnabled = false;
			CallBtn.Text = "Call";
        }
    }

	async void OnCallClicked(object sender, EventArgs e)
    {
		if(await DisplayAlert(
			"Dial a number",
			$"Would you like to call {translatedNumber}?",
			"Yes",
            "No"))
        {
            try
            {
				PhoneDialer.Open(translatedNumber);
            }
            catch (ArgumentNullException)
            {
				await DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");
            }
			catch (FeatureNotSupportedException)
            {
				await DisplayAlert("Unable to dial", "Phone dialing not supported.", "OK");
            }
			catch (Exception)
            {
				await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");	
            }
        }
    }
}


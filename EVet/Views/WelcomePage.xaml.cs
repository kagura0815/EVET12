namespace EVet.Views;

public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
		InitializeComponent();
	}
    private async void GetStart_Clicked(object sender, EventArgs e)
    {
        progressLoading.IsVisible = true;
        await Navigation.PushAsync(new PageFront());
        progressLoading.IsVisible = false;
    }

    private void Exitbtn_Clicked(object sender, EventArgs e)
    {
        System.Environment.Exit(0);
    }
}
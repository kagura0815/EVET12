namespace EVet.Views;

public partial class PageSettings : ContentPage
{
	public PageSettings()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        Application.Current!.MainPage = new PageHome();
    }
}
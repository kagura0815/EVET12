namespace EVet.Views;

public partial class PageFront : ContentPage
{
	public PageFront()
	{
		InitializeComponent();
	}
    private async void btnadd_Clicked(object sender, EventArgs e)
    {
       
            progressLoading.IsVisible = true;
            await Navigation.PushAsync(new PageLogin());
        progressLoading.IsVisible = false;
    }

    private async void btnadd_Clicked1(object sender, EventArgs e)
    {
        progressLoading.IsVisible = true;

        await Navigation.PushAsync(new PageLoginAdmin());
        progressLoading.IsVisible = false;
    }
   
}
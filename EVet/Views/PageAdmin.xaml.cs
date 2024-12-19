using System.Reflection;
using System.Xml.Linq;

namespace EVet.Views;

public partial class PageAdmin : ContentPage
{
	public PageAdmin()
	{
		InitializeComponent();
	}
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        Application.Current!.MainPage = new PageSettings();

        //await Navigation.PopAsync();

    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageAdminViewAppointment());
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PetInfo());

    }


    private async void OnAppointmentsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageAdminViewAppointment());
    }
   
    
   
}
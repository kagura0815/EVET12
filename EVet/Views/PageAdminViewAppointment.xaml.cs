using CommunityToolkit.Maui.Alerts;
using EVet.Models;
using EVet.Views;
using static EVet.Includes.GlobalVariables;
namespace EVet.Views;

public partial class PageAdminViewAppointment : ContentPage
{
    Appointment _apptlist = new Appointment();
    public PageAdminViewAppointment()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await FillList();
    }
    private async Task FillList()
    {
        ListAppointments.ItemsSource = await _apptlist.GetAppointments();
    }
    private async void itemeditstudent_Invoked_1(object sender, EventArgs e)
    {
        var item = sender as SwipeItemView;
        if (item == null) return;
        if (item.BindingContext is Appointment details)
        {
            apptkey = await _apptlist.GetAppointmentKey(details.BId);
            await DisplayAlert("Test", apptkey, "OK");
            await Navigation.PushAsync(page: new PageAppointmentNotification());

        }
    }



    private async void itemdeletestudent_OnInvoked(object sender, EventArgs e)
    {
        var item = sender as SwipeItemView;
        if (!(item?.BindingContext is Appointment getID)) return;
        id = getID.BId;
        if (!await DisplayAlert("Delete Student",
                "You are about to delete the student. All associated data from your account with this student will be removed. You will have to enter the class again when you decide to comeback." +
                "Do you really want to Delete?", "Yes", "No")) return;
        //progressLoading.IsVisible = true;
        var a = await _apptlist.DeleteAppointment(id);
        if (a)
        {

            var toast = Toast.Make("Student was removed Successfully!");
            await FillList();
        }
        else
        {
            var toast = Toast.Make("Something went wrong with your request. Please try again later!");
        }
    }

    private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
    {
        ListAppointments.ItemsSource = await _apptlist.FindAppointment(txtsearch.Text);
    }

    private async void txtsearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        ListAppointments.ItemsSource = await _apptlist.GetAllAppointments();
    }
}
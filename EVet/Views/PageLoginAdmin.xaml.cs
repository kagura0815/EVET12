namespace EVet.Views;
using static EVet.Includes.GlobalVariables;
using EVet.Models;
using Microsoft.Maui.Controls;

public partial class PageLoginAdmin : ContentPage
{
    Users _login = new();
    public PageLoginAdmin()
    {
        InitializeComponent();
    }



    private void showPasswordCheckBox1_CheckChanged(object sender, EventArgs e)
    {
        AdminPasswordEntry.IsPassword = !showPasswordCheckBox1.IsChecked;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Application.Current!.MainPage = new PageFront();
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        string username = AdminUsernameEntry.Text;
        string password = AdminPasswordEntry.Text;

        if (username == "admin" && password == "admin123")
        {
            // Navigate to Admin Dashboard
            await Navigation.PushAsync(new PageAdmin());
        }
        else
        {
            // Show error message
            AdminStatusLabel.Text = "Invalid username or password.";
        }
    }
}
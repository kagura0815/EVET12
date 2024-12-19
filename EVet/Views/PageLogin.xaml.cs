namespace EVet.Views;
using static EVet.Includes.GlobalVariables;
using EVet.Models;
using Microsoft.Maui.Controls;
using Firebase.Database.Query;
using Firebase.Database;
using EVet.Includes;
using System.ComponentModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Threading;

public partial class PageLogin : INotifyPropertyChanged
{
    CancellationTokenSource cancellationTokenSource = new();
    Users _login = new();
    public PageLogin()
	{
        Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        InitializeComponent();
       
    }

 


    private async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
        if (e.NetworkAccess != NetworkAccess.Internet)
        {
            nointernet.IsVisible = true;
            var toast = Toast.Make("You may have no internet connection!", ToastDuration.Long, 12);
            await toast.Show(cancellationTokenSource.Token);
        }
        else
        {
            nointernet.IsVisible = false;
            var toast = Toast.Make("Internet connection Restored!", ToastDuration.Long, 12);
            await toast.Show(cancellationTokenSource.Token);
        }
    }
    
    private async void btnadd_Clicked(object sender, EventArgs e)
    {
        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await DisplayAlert("Alert!", "No internet connection. Please check your network settings.", "OK");
            progressLoading.IsVisible = false;
            return;
        }


        Console.WriteLine($"Username: {txtuname.Text}, Password: {txtpword.Text}");

        if (string.IsNullOrEmpty(txtuname.Text))
        {
            await DisplayAlert("Data validation", "Please Enter User Name!", "Got it");
            progressLoading.IsVisible = false;
            return;
        }
        if (string.IsNullOrEmpty(txtpword.Text))
        {
            await DisplayAlert("Data Validation", "Please enter the Password", "Got It");
            progressLoading.IsVisible = false;
            return;
        }
      
        try
        {

            bool a;
            a = await _login.Getuser(txtuname.Text, txtpword.Text);
          
            if (!a)
            {
                await DisplayAlert("", "You Have Entered a Wrong Password", "Okay");
                progressLoading.IsVisible = false;
                return;

            }
            string iD = await GetUserByFirstNameAndLastName(txtuname.Text);

        if (string.IsNullOrEmpty(iD))

        {
            await DisplayAlert("Error", "User  not found 1", "Okay");
            return;
        }
                var user = await client.Child("Users").Child(iD).OnceSingleAsync<Users>();
                if (user == null)
                {
                    await DisplayAlert("Error", "User  not found 2", "Okay");
                    return;
                }
            progressLoading.IsVisible = true;

            string userFullName = $"{user.FirstName} {user.LastName}";
                GlobalVariables.IDD = iD;
                GlobalVariables.Fullname = userFullName;
                    await DisplayAlert("You Have Successfully Logged In", userFullName , "Okay");
            progressLoading.IsVisible = true;
            await Navigation.PushAsync(new PageHome());
            progressLoading.IsVisible = false;
            //Application.Current.MainPage = new PageHome();


        }
        catch (FirebaseException ex)
        {
            await DisplayAlert("Firebase Error", $"An error occurred: {ex.Message}", "Okay");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "Okay");
        }


    }


    

    private void showPasswordCheckBox_CheckChanged(object sender, EventArgs e)
    {
        txtpword.IsPassword = !showPasswordCheckBox.IsChecked;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        progressLoading.IsVisible = true;
        await Navigation.PushAsync(new PageRegister());
        progressLoading.IsVisible = false;
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageFront());
    }

    private async Task<string> GetUserByFirstNameAndLastName(string user)
    {
        var users = await client.Child("Users").OnceAsync<Users>();

        var userWithKey = users.FirstOrDefault(u => u.Object.User.Equals(user, StringComparison.OrdinalIgnoreCase));

        return userWithKey?.Key;
    }

   
       
    
}

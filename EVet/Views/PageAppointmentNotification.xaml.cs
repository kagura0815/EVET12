using EVet.Models;
using static EVet.Includes.GlobalVariables;
using Microsoft.Maui.Controls;
using EVet.Pages;
using CommunityToolkit.Maui.Views;
using System.Xml.Linq;
namespace EVet.Views;

public partial class PageAppointmentNotification : ContentPage
{
    public PageAppointmentNotification(string petName, string ownerName, DateTime appointmentDate, TimeSpan appointmentTime)
    {
        InitializeComponent();

        // Populate the labels with appointment data
        PetNameLabel.Text = petName;
        OwnerNameLabel.Text = ownerName;
        AppointmentDateLabel.Text = appointmentDate.ToString("D"); // Full date
        AppointmentTimeLabel.Text = appointmentTime.ToString(@"hh\:mm"); // Time in HH:mm format
        StatusLabel.Text = "Pending"; // Default status
    }

    public PageAppointmentNotification()
    {
    }

    private async void OnConfirmClicked(object sender, EventArgs e)
    {
        // Logic to confirm the appointment
        StatusLabel.Text = "Confirmed";
        await DisplayAlert("Confirmation", "Your appointment has been confirmed.", "OK");
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        // Logic to cancel the appointment
        StatusLabel.Text = "Canceled";
        await DisplayAlert("Cancellation", "Your appointment has been canceled.", "OK");
    }
}

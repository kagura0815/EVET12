using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using System.Reflection;
using System.Xml.Linq;
using EVet.Includes;
using EVet.Models;
using static EVet.Includes.GlobalVariables;
using Firebase.Database;
using Firebase.Database.Query;

namespace EVet.Views;

public partial class PageLabResults : ContentPage
    {
        public PageLabResults()
        {
            InitializeComponent();
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            // Create a new LabResult object
            var labResult = new LabResult() // Use the LabResult model instead of PageLabResults
            {
                PetName = PetNameEntry.Text,
                OwnerName = OwnerNameEntry.Text,
                Date = DatePicker.Date,
                Time = TimePicker.Time,
                TestName = TestNamePicker.SelectedItem?.ToString(),
                Result = ResultEntry.Text,
                Range = RangeEntry.Text // Capture the range input
            };

            // Validate input
            if (string.IsNullOrWhiteSpace(labResult.PetName) ||
                string.IsNullOrWhiteSpace(labResult.OwnerName) ||
                string.IsNullOrWhiteSpace(labResult.TestName) ||
                string.IsNullOrWhiteSpace(labResult.Result) ||
                string.IsNullOrWhiteSpace(labResult.Range))
            {
                await DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

            // Send the data to Firebase
            try
            {
                await App.FirebaseClient
                    .Child("LabResults") // This is the node where data will be stored
                    .PostAsync(labResult);

                // Show a confirmation message
                await DisplayAlert("Success", "Lab results submitted successfully!", "OK");

                // Clear the form fields
                PetNameEntry.Text = string.Empty;
                OwnerNameEntry.Text = string.Empty;
                ResultEntry.Text = string.Empty;
                RangeEntry.Text = string.Empty; // Clear the range field
                TestNamePicker.SelectedItem = null;
                DatePicker.Date = DateTime.Now;
                TimePicker.Time = TimeSpan.Zero;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to submit results: {ex.Message}", "OK");
            }
        }

        private void OnPetProfileClicked(object sender, EventArgs e)
        {
            // Navigate to the pet profile page
            Navigation.PushAsync(new Views.PageLabResults()); // Ensure this is the correct page
        }

        private void OnAppointmentsClicked(object sender, EventArgs e)
        {
            // Navigate to the appointments page
            Navigation.PushAsync(new PageAdminViewAppointment()); // Ensure this is the correct page
        }
    }





using EVet.Models;
using static EVet.Includes.GlobalVariables;
using Microsoft.Maui.Controls;
using EVet.Pages;
using CommunityToolkit.Maui.Views;
using System.Xml.Linq;
namespace EVet.Views;

public partial class BookAppointment : ContentPage
{
    private List<DateTime> bookedAppointments = new List<DateTime>
    {
        new DateTime(2023, 10, 25, 10, 0, 0), // Example booked appointment
        new DateTime(2023, 10, 25, 11, 0, 0)
    };
    Appointment _appt = new Appointment();
    public Appointment SelectedPet { get; set; } = new Appointment();

    public BookAppointment()
	{
		InitializeComponent();
        AppointmentDatePicker.MinimumDate = DateTime.Today; // Prevent past dates
        InitializePickers();
    }
    private void InitializePickers()
    {
        // Initialize the pet type picker
        ptype.SelectedIndexChanged += OnPetTypeChanged;

        // Initialize the breed pickers
        pbreedDog.SelectedIndexChanged += OnBreedChanged;
        pbreedCat.SelectedIndexChanged += OnBreedChanged;

        // Initialize the service picker
        servicePicker.SelectedIndexChanged += OnServiceChanged;
    }
    public async void OnBookAppointmentClicked(object sender, EventArgs e)
    {
        var bid = Guid.NewGuid().ToString();
        BookingProgressBar.IsVisible = true;
        BookingProgressBar.Progress = 0;

        // Simulate progress
        for (int i = 0; i <= 100; i += 20)
        {
            BookingProgressBar.Progress = i / 100.0;
            await Task.Delay(100); // Simulate some work
        }
        DateOnly selectedDate = DateOnly.FromDateTime(AppointmentDatePicker.Date);
        TimeOnly selectedTime = TimeOnly.FromTimeSpan(AppointmentTimePicker.Time); // Convert TimeSpan to TimeOnly

        // Check if the selected date and time are in the past
        DateTime selectedDateTime = selectedDate.ToDateTime(selectedTime); // Use TimeOnly here
        string formattedDate = AppointmentDatePicker.Date.ToString("MM/dd/yyyy");
        string formattedTime = selectedTime.ToString(@"hh\:mm"); // Format as HH:mm
       
        if (selectedDateTime < DateTime.Now)
        {
            await DisplayAlert("Error", "The selected date have already passed. Please choose a future date .", "OK");
            return; // Exit the method if the time is in the past
        }
        if (IsTimeBooked(selectedDate, selectedTime))
        {
            await DisplayAlert("Error", "The selected time is already booked. Please choose a different time.", "OK");
            return; // Exit the method if the time is booked
        }

      
        var appointment = new Appointment();
        bool result = await appointment.AddAppointments(bid, PetNameEntry.Text, OwnerNameEntry.Text, SelectedPet.Type, SelectedPet.Breed, SelectedPet.Service, selectedDate, AppointmentTimePicker.Time);

        if (result)
        {
            await DisplayAlert("Success", "Your appointment has been booked!", "OK");
            await Navigation.PushAsync(new PageAppointmentNotification(
           PetNameEntry.Text,
           OwnerNameEntry.Text,
           AppointmentDatePicker.Date,
           AppointmentTimePicker.Time));
        }
        else
        {
            await DisplayAlert("Error", "There was an error booking your appointment.", "OK");
        }

        // Reset the progress bar and clear the input fields
        BookingProgressBar.IsVisible = false;
        BookingProgressBar.Progress = 0;
        PetNameEntry.Text = string.Empty;
        OwnerNameEntry.Text = string.Empty;
        ptype.SelectedIndex = -1; // Reset pet type
        pbreedDog.SelectedIndex = -1; // Reset dog breed
        pbreedCat.SelectedIndex = -1; // Reset cat breed
        servicePicker.SelectedIndex = -1; // Reset service
        AppointmentDatePicker.Date = DateTime.Today; // Reset to today's date
        AppointmentTimePicker.Time = TimeSpan.Zero;

    }


    private bool IsTimeBooked(DateOnly date, TimeOnly time)
    {
        // Check if the selected date and time are already booked
        DateTime dateTime = date.ToDateTime(TimeOnly.MinValue);
        TimeSpan selectedTimeSpan = time.ToTimeSpan();

        // Check if the selected date and time are already booked
        return bookedAppointments.Any(appointment =>
            appointment.Date == dateTime.Date &&
            appointment.TimeOfDay == selectedTimeSpan);
    }
    private void OnPetTypeChanged(object sender, EventArgs e)
    {
        var selectedPetType = ptype.SelectedItem as string;
       SelectedPet.Type = selectedPetType;

        if (selectedPetType == "Dog")
        {
            pbreedDog.IsVisible = true;
            pbreedCat.IsVisible = false;
        }
        else if (selectedPetType == "Cat")
        {
            pbreedDog.IsVisible = false;
            pbreedCat.IsVisible = true;
        }
    }
    private void OnServiceChanged(object sender, EventArgs e)
    {
        if (servicePicker.SelectedIndex != -1)
        {
            SelectedPet.Service = servicePicker.SelectedItem.ToString();
        }
    }
    private void OnBreedChanged(object sender, EventArgs e)
    {
        if (pbreedDog.SelectedIndex != -1)
        {
            SelectedPet.Breed = pbreedDog.SelectedItem.ToString();
        }
        else if (pbreedCat.SelectedIndex != -1)
        {
            SelectedPet.Breed = pbreedCat.SelectedItem.ToString();
        }
    }
    private void OnDateSelected(object sender, DateChangedEventArgs e)
        {
            // Reset the TimePicker when a new date is selected
            AppointmentTimePicker.Time = TimeSpan.Zero;

            // Disable times that are already booked for the selected date
            UpdateAvailableTimes(e.NewDate);
        }
        private void UpdateAvailableTimes(DateTime selectedDate)
        {
            var unavailableTimes = bookedAppointments
                   .Where(appointment => appointment.Date == selectedDate.Date)
                   .Select(appointment => appointment.TimeOfDay)
                   .ToList();
            string unavailableMessage = "Unavailable times: " + string.Join("12am-7am,  ", unavailableTimes);
            // You can use a Label to show this message in your UI
            UnavailableTimesLabel.Text = unavailableMessage; // Assuming you have a Label named UnavailableTimesLabel
                                                             // Clear the TimePicker's selected time if it's already booked
            for (int hour = 0; hour < 24; hour++)
            {
                foreach (var booked in bookedAppointments)
                {
                    if (booked.Date == selectedDate.Date && booked.Hour == hour)
                    {
                        // Logic to disable the time (you may need to implement a custom TimePicker)
                        // For example, you can show a message or disable the TimePicker
                    }
                }
            }
        }
}
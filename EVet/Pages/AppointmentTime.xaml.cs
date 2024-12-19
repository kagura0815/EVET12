using System;
using System.Collections.Generic;

namespace EVet.Pages;

public partial class AppointmentTime : ContentView
{
    private List<DateTime> bookedAppointments = new List<DateTime>
    {
        new DateTime(2023, 10, 25, 10, 0, 0), // Example booked appointment
        new DateTime(2023, 10, 25, 11, 0, 0)
    };
    public AppointmentTime()
	{
		InitializeComponent();
        AppointmentDatePicker.MinimumDate = DateTime.Today; // Prevent past dates
       
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
        string unavailableMessage = "Unavailable times: " + string.Join(", ", unavailableTimes);
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
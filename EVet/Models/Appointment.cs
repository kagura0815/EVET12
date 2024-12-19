
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static EVet.App;
using Firebase.Database.Query;
using static EVet.Includes.GlobalVariables;
namespace EVet.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public string BId { get; set; }
        public string PetName { get; set; }
        public string OwnerName { get; set; }
        public string Type { get; set; } // Dog or Cat
        public string Breed { get; set; } // Breed of the pet
        public string Service { get; set; } // Selected service
        public DateOnly AppointmentDate { get; set; }

        public TimeSpan AppointmentTime { get; set; }
        // Constructor


        // Method to add an appointment
        public async Task<bool> AddAppointments(string bid, string petName, string ownerName, string type, string petBreed, string service, DateOnly appointmentDate, TimeSpan appointmentTime)
        {
            // Simulate adding an appointment (e.g., saving to a database)
            try
            {
                var appt = new Appointment()
                {
                    BId = bid,
                    PetName = petName,
                    OwnerName = ownerName,
                    Type = type,
                    Breed = petBreed,
                    Service = service,
                    AppointmentDate = appointmentDate,
                    AppointmentTime = appointmentTime

                };
                await client.Child("Appointments").PostAsync(appt);
                return true;


            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                // Return false if there was an error
                return false;
            }
        }
        public async Task<List<Appointment>> GetAppointments()
        {

            return (await client
                .Child($"Appointments")
                .OnceAsync<Appointment>()).Select(item => new Appointment
                {

                    PetName = item.Object.PetName,
                    OwnerName = item.Object.OwnerName,
                    Type = item.Object.Type,
                    Breed = item.Object.Breed,
                    Service = item.Object.Service,
                    AppointmentDate = item.Object.AppointmentDate,
                    AppointmentTime = item.Object.AppointmentTime

          
                                    

                }).ToList();
        }
        public async Task<List<Appointment>> GetAllAppointments()
        {
            var appointment = await client
               .Child("Appointments")
               .OnceAsync<Appointment>();

            return appointment
               .Select(item => new Appointment
               {
                   BId = item.Object.BId,
                   PetName = item.Object.PetName,
                   OwnerName = item.Object.OwnerName,
                   Type = item.Object.Type,
                   Breed = item.Object.Breed,
                   Service = item.Object.Service,
                   AppointmentDate = item.Object.AppointmentDate,
                   AppointmentTime = item.Object.AppointmentTime
               })
               .ToList();
        }
        public async Task<List<Appointment>> FindAppointment(string fname)
        {
            var queryVisitor = await GetAllAppointments();
            await client
                .Child("Appointments")
                .OnceAsync<Appointment>();
            var searchTerms = fname.Split(' ');
            return queryVisitor.Where(a => searchTerms.Any(term => a.PetName.ToLower().Contains(term.ToLower())
            || a.OwnerName.ToLower().Contains(term.ToLower())
            || a.Type.ToLower().Contains(term.ToLower())
            || a.Breed.ToLower().Contains(term.ToLower())
            || a.Service.ToLower().Contains(term.ToLower())))
                .ToList();

        }
        public async Task<string> GetAppointmentKey(string _bid)
        {
            // Fetch all appointments from the Firebase database
            var evaluateCode = (await client.Child("Appointments")
                .OnceAsync<Appointment>()).FirstOrDefault(a => a.Object.BId == _bid);

            // Check if an appointment with the given BId exists
            if (evaluateCode != null)
            {
                // Optionally, you can store or use other details if needed
                string appointmentKey = evaluateCode.Key;

                // You can also retrieve other properties if needed
                Guid id = evaluateCode.Object.Id;
                petName = evaluateCode.Object.PetName;
                ownerName = evaluateCode.Object.OwnerName;
                type = evaluateCode.Object.Type;
                breed = evaluateCode.Object.Breed;
                service = evaluateCode.Object.Service;
                appointmentDate = evaluateCode.Object.AppointmentDate.ToString();
                appointmentTime = evaluateCode.Object.AppointmentTime.ToString();

                // Return the key of the appointment
                return appointmentKey;
            }

            // Return null if no appointment is found
            return null;
        }
        public async Task<bool> DeleteAppointment(string id)
        {
            try
            {
                var getstudentkey = (await client
                    .Child("Appointments")
                    .OnceAsync<Appointment>()).FirstOrDefault
                    (a => a.Object.BId == id);
                if (getstudentkey != null)
                {
                    await client
                        .Child("Appointments")
                    .Child(getstudentkey.Key)
                        .DeleteAsync();
                    client.Dispose();
                    return true;
                }

                client.Dispose();
                return false;
            }
            catch
            {
                client.Dispose();
                return false;
            }
        }
    }

}
    

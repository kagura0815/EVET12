using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVet.Includes;
using Firebase.Database.Query;
using static EVet.Includes.GlobalVariables;
namespace EVet.Models
{
    class Users
    {
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string UID {  get; set; }
        public string User { get; set; }
        public string Pass { get; set; }


        public async Task<bool> _AddUser(string idd, string fname,
           string lname, string cnum, string address, string user, string pass)
        {
            var uidd = GlobalVariables.uid; // Assuming IDD is a global variable for user ID
            var users = new Users()
            {

                 UID = idd,
                FirstName = fname,
                LastName = lname,
                ContactNumber = cnum,
                Address = address,
                User = user,
                Pass = pass,

            };
            await client.Child("Users").PostAsync(users);
            return true;
        }
        public async Task<bool> Getuser(string user, string pass)
        {
            try
            {
                var evaluateEmail =
                    (await client.Child("Users").OnceAsync<Users>()).FirstOrDefault(a =>
                    a.Object.User == user && a.Object.Pass == pass);
                if (evaluateEmail != null)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> GetUsername(string user)
        {
            try
            {
                var evaluateCourseCode =
                    (await client.Child("Users").OnceAsync<Users>()).FirstOrDefault(b =>
                    b.Object.User == user);
                if (evaluateCourseCode != null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> GetUsers(string Users)
        {
            try
            {
                var evaluateUser =
                    (await client.Child("Users").OnceAsync<Users>()).FirstOrDefault(a =>
                    a.Object.User == Users);
                if (evaluateUser != null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

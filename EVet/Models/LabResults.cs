
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
    public class LabResult
    {
        public string PetName { get; set; }
        public string OwnerName { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string TestName { get; set; }
        public string Result { get; set; }
        public string Range { get; set; } // Added for the range field
    }

}
    

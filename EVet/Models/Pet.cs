using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVet.Models
{
    public class Pet
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string ImageSource { get; set; }
        public string Gender { get; set; }
        public string Weight { get; set; }
        public string PetType { get; set; }
        public string Breed { get; set; }
    }
}

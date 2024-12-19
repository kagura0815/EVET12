using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.Database.Query;
using System.Threading.Tasks;
using static EVet.Includes.GlobalVariables;
using System.Net;
using System.Reflection;
using Microsoft.Maui.Storage;
using Firebase.Storage;
using System.Reflection.Emit;
using System.Threading.Tasks;
using EVet.Includes;
using Firebase.Database;
namespace EVet.Models
{
   

    public class Pets
    {
       
            //public async Task<List<Pet>> GetPet()
            //{
            //    // Simulate data retrieval
            //    return await Task.FromResult(new List<Pet>
            //{
            //    new Pet { Name = "Buddy", ImageSource = "buddy.png", Gender = "Male", Weight = "30 lbs", Breed = "Golden Retriever" },
            //    new Pet { Name = "Mittens", ImageSource = "mittens.png", Gender = "Female", Weight = "10 lbs", Breed = "Siamese" }
            //    // Add more pets as needed
            //});
            //}
        public async Task<List<Pets>> GetPets()
        {

            return (await client
                .Child($"Users/{IDD}/Pets")
                .OnceAsync<Pets>()).Select(item => new Pets
                {
                    Name = item.Object.Name,
                    Breed = item.Object.Breed,
                    PetType = item.Object.PetType,
                    //Birthday = item.Object.Birthday,
                    Gender = item.Object.Gender,
                    Images = item.Object.Images,
                    Weight = item.Object.Weight,



                }).ToList();
        }
        public List<Pets> PetList { get; set; } = new List<Pets>();
        public string Images { get; set; }
        public string PetId { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        //public string Birthday { get; set; }
        public string Gender { get; set; }
        public string PetType { get; set; } // e.g., "Dog", "Cat"
        public string ImageSource { get; set; }
        public string Service { get; set; } // e.g., "Grooming", "Checkup"
    
    public string Weight { get; set; }
    

        public async Task<bool> AddPet(
            string id,
            string name,
            string type,
            string breed,
            //string birthday,
            string gender,
            string weight,
            FileResult mainimg,
            string flename)
        {

            try
            {
                // Attempt to upload the image
                var _mainimg = await UploadImage(await mainimg.OpenReadAsync(), $"{flename}_mainimg.png");

                var idd = GlobalVariables.IDD; // Assuming IDD is a global variable for user ID
                var pets = new Pets()
                {
                    ID = idd,
                    Name = name,
                    Breed = breed,
                    PetType = type,
                    //Birthday = birthday,
                    Gender = gender,
                    Weight = weight,
                    Images = _mainimg
                };
              

                // Attempt to post the pet data to the database
                await client.Child($"Users/{IDD}/Pets").PostAsync(pets);

                return true; // Return true if everything is successful
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                Console.WriteLine($"An error occurred while adding pet: {ex.Message}");

                // You can also log the stack trace if needed
                // Console.WriteLine(ex.StackTrace);

                return false; // Return false to indicate failure
            }
        }
        public void AddPet(Pets pet)
        {
            PetList.Add(pet);
        }

        public class pets
        {
            public string ID { get; set; }
            public string Name { get; set; }
                public string Breed { get; set; }

            public string Weight { get; set; }
            public string Gender { get; set; }
        }
        


        public async Task<bool> Addimage(FileResult mainimg, string flename)
        {
            try
            {

                var _mainimg = await UploadImage(await mainimg.OpenReadAsync(), $"{flename}_mainimg.png");

                client.Dispose();
                return false;
            }
            catch
            {
                client.Dispose();
                return false;
            }
        }
 
        public async Task<string> UploadImage(Stream img, string flename)
        {
            try
            {
                var image = await storage
                    .Child($"Images/PetImage/{flename}")
                    .PutAsync(img);
                return image;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Uploading image {e.Message}");

                return "false";
            }
        }
        //public async Task<List<Students>> GetStudents()
        //{
        //    return (await client
        //        .Child("Students")
        //        .OnceAsync<Students>()).Select(item => new Students
        //        {
        //            ID = item.Object.ID,
        //            Firstname = item.Object.Firstname,
        //            Lastname = item.Object.Lastname,
        //            Gender = item.Object.Gender,
        //            Address = item.Object.Address,
        //            ContactNumber = item.Object.ContactNumber
        //        }).ToList();
        //}
        public async Task<bool> GetCode(string _id)
        {
            try
            {
                var evaluateCode = (await client.Child("PetInfo")
                .OnceAsync<Pets>()).FirstOrDefault(a =>
                a.Object.ID == _id);
                if (evaluateCode == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<Pets>> GetPetsAsync(string userId)
        {
            return (await client
                .Child($"Users/{userId}/Pets")
                .OnceAsync<Pets>())
                .Select(item => new Pets
                {
                    ID = item.Object.ID,
                    Name = item.Object.Name,
                    Breed = item.Object.Breed,
                    Gender = item.Object.Gender,
                    Weight = item.Object.Weight,
                    Images = item.Object.Images
                }).ToList();
        }
        public async Task<String> GetPetkey(string _id)
        {
            var evaluateCode = (await client.Child("Pets")
                .OnceAsync<Pets>()).FirstOrDefault
                (a => a.Object.ID == _id);
            if (evaluateCode != null)
            {

                petkey = evaluateCode.Key;
                id = evaluateCode.Object.ID;
                name = evaluateCode.Object.Name;
                breed = evaluateCode.Object.Breed;
               
                gender = evaluateCode.Object.Gender;
            
                weight = evaluateCode.Object.Weight;
               
                return evaluateCode.Key;
            }
            return null;
        }

        public async Task<bool> DeleteRecipe(string id)
        {
            try

            {

                var getpetkey = (await client
                    .Child("Pets")
                    .OnceAsync<Pets>()).FirstOrDefault
                    (a => a.Object.ID == id);
                if (getpetkey != null)
                {
                    await client
                        .Child("Pets")
                    .Child(getpetkey.Key)
                        .DeleteAsync();
                    client.Dispose();
                    await storage
                    .Child($"Images/StudentImage/{id}_mainimg.png")
                    .DeleteAsync();
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
        public async Task<bool> EditRecipe( string name, string breed, string birthday, string gender, string neutered, string allergies,string weight, string flename)
        {

            var pets = new Pets()
            {
                
                Name = name,
                Breed = breed,
                //Birthday = birthday,
                Gender = gender,
             
                Weight = weight,
                Images = flename
            };
            await client.Child($"Pets/{petkey}").PatchAsync(pets);
            return true;

        }

        //public async Task<List<Pets>> GetAllRecipe()

        //{

        //    return (await client
        //    .Child("Pets")
        //    .OnceAsync<Pets>()).Select(item => new Pets
        //    {
        //        RecipeName = item.Object.RecipeName,
        //        Category = item.Object.Category,
        //        Meal = item.Object.Meal,
        //        Duration = item.Object.Duration,
        //        Code = item.Object.Code,
        //        Ingredient = item.Object.Ingredient,
        //        Instruction = item.Object.Instruction,
        //        Images = item.Object.Images
        //    }).ToList();
        //}
        //public async Task<List<Pets>> FindRecipe(string fname)
        //{
        //    var queryUsers = await GetAllRecipe();
        //    await client
        //        .Child("Pets")
        //        .OnceAsync<Pets>();
        //    var searchTerms = fname.Split(',');
        //    return queryUsers.Where(a => searchTerms.Any(term => a.RecipeName.ToLower().Contains(term.ToLower()) || a.Category.ToLower().Contains(term.ToLower()) || a.Meal.ToLower().Contains(term.ToLower()) || a.Ingredient.ToLower().Contains(term.ToLower()) || a.Code.ToLower().Contains(term.ToLower())

        //    )).ToList();
        //}
        
    }
}


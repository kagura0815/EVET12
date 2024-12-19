using System.Collections.ObjectModel;
using System.ComponentModel;
using EVet.Models;
namespace EVet.ViewModels
{
    public class PetProfileViewModel
    {
        public ObservableCollection<Pet> Pets { get; set; }
        public Pet SelectedPet { get; set; }

        public PetProfileViewModel()
        {
            // Initialize the collection
            Pets = new ObservableCollection<Pet>();

            // Load stored pet data
            LoadStoredPets();

            // Set a default selected pet
            if (Pets.Count > 0)
            {
                SelectedPet = Pets[0]; // Set the first pet as the default selected pet
            }
        }

        private void LoadStoredPets()
        {
            // Replace this with your actual data retrieval logic
            // For example, if you are retrieving from a database or a file:
            var storedPets = GetStoredPets(); // This method should return a List<Pet>

            foreach (var pet in storedPets)
            {
                Pets.Add(pet); // Add each pet to the ObservableCollection
            }
        }

        private List<Pet> GetStoredPets()
        {
            // Simulate retrieving stored pets
            // Replace this with your actual data retrieval logic
            return new List<Pet>
            {
                new Pet { Name = "Buddy", ImageSource = "buddy.png", Gender = "Male", Weight = "30 lbs", Breed = "Golden Retriever" },
                new Pet { Name = "Mittens", ImageSource = "mittens.png", Gender = "Female", Weight = "10 lbs", Breed = "Siamese" }
                // Add more pets as needed
            };
        }
    }
    //public class PetProfileViewModel : INotifyPropertyChanged
    //{
    //    private Pet _selectedPet;

    //    public Pet SelectedPet
    //    {
    //        get => _selectedPet;
    //        set
    //        {
    //            _selectedPet = value;
    //            OnPropertyChanged(nameof(SelectedPet));
    //        }
    //    }

    //    public ObservableCollection<Pet> Pets { get; set; }

    //    public PetProfileViewModel()
    //    {
    //        // Initialize the collection and add some sample data
    //        Pets = new ObservableCollection<Pet>
    //        {
    //            new Pet { Name = "Buddy", ImageSource = "buddy.png", Gender = "Male", Weight = "30 lbs", Breed = "Golden Retriever" },

    //        };

    //        // Set a default selected pet
    //        SelectedPet = Pets[0]; // Set the first pet as the default selected pet
    //    }

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    protected virtual void OnPropertyChanged(string propertyName)
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //    }
    //}
}

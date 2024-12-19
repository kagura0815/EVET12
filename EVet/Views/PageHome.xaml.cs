using EVet.Includes;
using EVet.Models;
using EVet.Views;
using static EVet.Includes.GlobalVariables;
namespace EVet.Views;
    
public partial class PageHome : ContentPage
{
    private int _selectedPetIndex;
    private List<Pets> _pets;
    public PageHome()
	{
		InitializeComponent();
      
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageSettings());
        //await Navigation.PopAsync();

    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BookAppointment());
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PetInfo());

    }
    private void LoadPets()
    {
        // Example: Load pets from a data source
        _pets = new List<Pets>
            {
               
                // Add more pets as needed
            };

        // Example: Set the selected pet index (this could be set based on user interaction)
        _selectedPetIndex = 0; // Assuming the first pet is selected by default
    }

    private async void OnAppointmentsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BookAppointment());
    }
    private Pets GetSelectedPet()
    {
     
        // Logic to retrieve the selected pet
        // This is just a placeholder; implement your logic here
        return new Pets
        {
            Name = name,
            ImageSource = images,
            PetType = petType,
            Gender = gender,
            Weight =weight,
            Breed = breed
        };
    }
    private async void OnPetProfileClicked(object sender, EventArgs e)
    {


        Pets selectedPet = GetSelectedPet(); // Get the currently selected pet

        if (selectedPet != null) // Check if a pet is selected
        {
            // Navigate to the PetProfile page and pass the selected pet
            await Navigation.PushAsync(new PetProfile(selectedPet));
        }
        else
        {
            // Handle the case where no pet is selected
            await DisplayAlert("Error", "No pet selected.", "OK");
        }

    }
}
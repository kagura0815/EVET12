using EVet.Models;

using static EVet.Includes.GlobalVariables;
using EVet.Models;
using Microsoft.Maui.Controls;
using EVet.Pages;
using CommunityToolkit.Maui.Views;
using System.Diagnostics;
namespace EVet.Views;

public partial class PetProfile : ContentPage
{
   
   
	public PetProfile(Pets pets)
	{
       
        InitializeComponent();
        //_pets = pet;
        //BindPetData();
        BindingContext = pets;
    }
    //private void BindPetData()
    //{
    //    if (_pets == null)
    //    {
    //        Debug.WriteLine("Pets object is null.");
    //        return;
    //    }

    //    // Assuming you have labels or other UI elements to display pet data
    //    petNameLabel.Text = _pets.Name; // Replace with actual property names
    //    petBreedLabel.Text = _pets.Breed;
    //    petGenderLabel.Text = _pets.Gender;
    //    petWeightLabel.Text = _pets.Weight;
    //    petImage.Source = _pets.ImageSource; // Assuming you have a property for the image source
    //}
   
    

    //protected override async void OnAppearing()
    //    {
    //        base.OnAppearing();
    //        //await FillList();
    //    }
    //    //private async Task FillList()
    //    //{
    //    //    ListsPets.ItemsSource = await _petslist.GetPet();
    //    //}
    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new SelectPet());
    }
        private void ImageButton_Clicked(object sender, EventArgs e)
    {
        Application.Current!.MainPage = new PageSettings();
    }
    private async void OnHomePageClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageHome());
    }

    private async void OnAppointmentsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BookAppointment());
    }

    private async void OnPetProfileClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageLabResults());
    }

    private async void selectPet(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SelectPet());
    }
}
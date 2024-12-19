    using EVet.Includes;
    using static EVet.Includes.GlobalVariables;
    using EVet.Models;
    using Font = Microsoft.Maui.Font;
    using Color = Microsoft.Maui.Graphics.Color;
    using IImage = Microsoft.Maui.IImage;
    using static EVet.App;
    using System.Collections.ObjectModel;


    namespace EVet.Views;

    public partial class PetInfo : ContentPage
    {
        Pets _pets = new Pets();

        public ObservableCollection<string> Gender { get; set; } = new ObservableCollection<string>();
    public Pets SelectedPet { get; set; } = new Pets();



    string srcs = "f_camera_b.png";
        public PetInfo()
	    {
		    InitializeComponent();
        InitializePickers();
        

     
        }
    private void InitializePickers()
    {
        // Initialize the pet type picker
        pettype.SelectedIndexChanged += OnPetTypeChanged;

        // Initialize the breed pickers
        petbreedDog.SelectedIndexChanged += OnBreedChanged;
        petbreedCat.SelectedIndexChanged += OnBreedChanged;

        
    }
    private async void btnadd_Clicked(object sender, EventArgs e)
        {
            
            var flename = fullNameUser;
            var selectedgen = txtgender.SelectedItem.ToString();
            //var selectedneut = txtneutered.SelectedItem.ToString();
       
            //var selecteddate = txtbirthday.Date.ToString();
            //var selectedml = txtmeal.SelectedItem.ToString();
            var id = Guid.NewGuid().ToString();
            if (string.IsNullOrEmpty(txtname.Text))
            {
                await DisplayAlert("Data validation", "Please Fill up Name!", "Got it");
                return;
            }
      
       
            else if (txtgender.SelectedItem == null)
            {
                await DisplayAlert("Data validation", "Please Enter Category Type!", "Got it");
                return;
            }
            //else if (txtbirthday.SelectedItem == null)
            //{
            //    await DisplayAlert("Data validation", "Please Enter Cooking Duration!", "Got it");
            //    return;
            //}
   
            else if (string.IsNullOrEmpty(weight.Text))
            {
                await DisplayAlert("Data validation", "Please Enter Weight!", "Got it");
                return;
            }
            else if (_mainimgResult == null)
            {
                await DisplayAlert("Data validation", "Please Choose a Photo!", "Got it");
                return;
            }
            string weightWithUnit = weight.Text + " Kg";

            //var adss = await _pets.AddPet(id,
            //    txtname.Text,
            //  SelectedPet.PetType, SelectedPet.Breed ,
            //    selectedgen,
            //    weightWithUnit,
            //    _mainimgResult,flename
            //   );
            //if (!adss)
            //{
            //    await DisplayAlert("Warning!", "Data has been failed to add.", "Okay");

            //}
            //else
            //{
            //    await DisplayAlert("Got it!", "Data has been successfully added.", "Okay");
            Pets newPet = new Pets
            {
                ID = id,
                Name = txtname.Text,
                PetType = SelectedPet.PetType,
                Breed = SelectedPet.Breed,
                Gender = selectedgen,
                Weight = weightWithUnit,
                Images = _mainimgResult.FullPath // Assuming you have a way to get the image source
            };


        var adss = await _pets.AddPet(id,
     newPet.Name,
     newPet.PetType,
     newPet.Breed,
     newPet.Gender,
     newPet.Weight,
     _mainimgResult,
     flename
 );

        if (!adss)
        {
            await DisplayAlert("Warning!", "Data has been failed to add.", "Okay");
        }
        else
        {
            await DisplayAlert("Got it!", "Data has been successfully added.", "Okay");

            // Navigate to PetProfile with the new pet
            await Navigation.PushAsync(new PetProfile(newPet)); // Pass the Pet object
            progressLoading.IsVisible = false;
            // Clear the form
            txtname.Text = string.Empty;
            txtgender.SelectedItem = null;
            pettype.SelectedIndex = -1; // Reset pet type
            petbreedDog.SelectedIndex = -1; // Reset dog breed
            petbreedCat.SelectedIndex = -1; // Reset cat breed
            weight.Text = string.Empty;
            mainimage.Source = srcs;
        }
        PetStore.Pets.Add(newPet);


    }
    //private async void OnPetSelected(object sender, SelectedItemChangedEventArgs e)
    //{
    //    if (e.SelectedItem is Pets selectedPet)
    //    {
    //        // Navigate to the PetDetailPage and pass the selected pet
    //        await Navigation.PushAsync(new PetProfile(selectedPet));
    //    }
    //}
    private void OnPetTypeChanged(object sender, EventArgs e)
    {
        var selectedPetType = pettype.SelectedItem as string;
        SelectedPet.PetType = selectedPetType;

        if (selectedPetType == "Dog")
        {
            petbreedDog.IsVisible = true;
            petbreedCat.IsVisible = false;
        }
        else if (selectedPetType == "Cat")
        {
            petbreedDog.IsVisible = false;
            petbreedCat.IsVisible = true;
        }
    }
    private void OnBreedChanged(object sender, EventArgs e)
    {
        if (petbreedDog.SelectedIndex != -1)
        {
            SelectedPet.Breed = petbreedDog.SelectedItem.ToString();
        }
        else if (petbreedCat.SelectedIndex != -1)
        {
            SelectedPet.Breed = petbreedCat.SelectedItem.ToString();
        }
    }
    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select main image",
                FileTypes = FilePickerFileType.Images
            });
            if (result == null) return;

            FileInfo fi = new(result.FullPath);
            var size = fi.Length;

            if (size > 524288)
            {
                await DisplayAlert("Message", "The image you have selected is more than 0.50MB please ensure that the size of the image is less than the maximum size. Thank you!", "Got It.");
                return;
            }
            var stream = await result.OpenReadAsync();
            _mainimgResult = result;
            mainimage.Source = ImageSource.FromStream(() => stream);
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            //Application.Current!.MainPage = new DisplayRecipe();
        }
    }
using Microsoft.Maui;
using Microsoft.Maui.Controls;
namespace EVet
{
    public partial class MainPage : ContentPage
    {
        private int _currentPageIndex = 0;
        private readonly List<ContentView> _pages;


        public MainPage()
        {
            InitializeComponent();
            // Initialize the pages
            _pages = new List<ContentView>
        {
            new Pages.PetDetails(),
            new Pages.SelectService(),
            new Pages.AppointmentTime(),
            new Pages.Confirmation()
        };

            LoadPage(0);
        }

        private void LoadPage(int index)
        {
            _currentPageIndex = index;
            PageContent.Content = _pages[index];

            // Update progress bar
            PageProgressBar.Progress = (double)(index + 1) / _pages.Count;

            // Enable/Disable navigation buttons
            PreviousButton.IsEnabled = index > 0;
            NextButton.Text = index < _pages.Count - 1 ? "Next" : "Finish";
        }

        private void OnPreviousClicked(object sender, EventArgs e)
        {
            if (_currentPageIndex > 0)
                LoadPage(_currentPageIndex - 1);
        }

        private void OnNextClicked(object sender, EventArgs e)
        {
            if (_currentPageIndex < _pages.Count - 1)
                LoadPage(_currentPageIndex + 1);
            else
                DisplayAlert("Success", "Your appointment has been booked!", "OK");
        }

    }
}

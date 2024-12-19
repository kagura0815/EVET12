using EVet.Models;
using EVet.Pages;
using EVet.Views;
using Firebase.Database;
namespace EVet
{
    public partial class App : Application
    {
        public static FileResult _mainimgResult;
        public static string UserkeyUser, fullNameUser, key;
        public static FirebaseClient FirebaseClient { get; private set; }
        public App()
        {
            InitializeComponent();


            // Initialize Firebase
            FirebaseClient = new FirebaseClient("https://e-vet-52356-default-rtdb.asia-southeast1.firebasedatabase.app/");

            MainPage = new NavigationPage(new PageLabResults());

        }
    }
}

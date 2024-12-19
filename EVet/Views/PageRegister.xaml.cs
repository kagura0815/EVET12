using EVet.Models;
using System.Text.RegularExpressions;
namespace EVet.Views;

public partial class PageRegister : ContentPage
{
    Users _Users = new();
    public PageRegister()
	{
		InitializeComponent();
	}

    private async void btnadd_Clicked(object sender, EventArgs e)
    {
        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await DisplayAlert("Alert!", "No internet connection. Please check your network settings.", "OK");
            return;
        }

        if (!termsCheckbox.IsChecked)
        {
            await DisplayAlert("Information", "Please agree to the terms and conditions & Privacy Policy before you can register.", "OK");
            return;
        }
        var idd = Guid.NewGuid().ToString();
        if (string.IsNullOrEmpty(txtfname.Text))
        {
            await DisplayAlert("Data validation", "Please Enter  Firstname!", "Got it");
            return;
        }
        else if (string.IsNullOrEmpty(txtlname.Text))
        {
            await DisplayAlert("Data validation", "Please Enter Lastname!", "Got it");
            return;
        }
        else if (string.IsNullOrEmpty(txtcnum.Text) || txtcnum.Text.Length != 11 || !Regex.IsMatch(txtcnum.Text, @"^\d{11}$"))
        {
            await DisplayAlert("Data validation", "The number must contain exactly 11 digits.", "Got it");
            return;
        }
        else if (string.IsNullOrEmpty(txtaddress.Text))
        {
            await DisplayAlert("Data validation", "Please Enter Lastname!", "Got it");
            return;
        }
        else if (string.IsNullOrEmpty(txtuser.Text))
        {
            await DisplayAlert("Data validation", "Please Enter  Username!", "Got it");
            return;
        }
       
        else if (string.IsNullOrEmpty(txtpword.Text) || txtpword.Text.Length < 8 || !Regex.IsMatch(txtpword.Text, @"^[a-zA-Z0-9]+$"))
        {
            await DisplayAlert("Data validation", "Please Enter a Password that is at least 8 characters long and contains both letters and numbers!", "Got it");
            return;
        }

        bool a;
        a = await _Users.GetUsers(txtuser.Text);
        if (!a)
        {
            await DisplayAlert("User Name validation", "The User Name. you`ve entered is already in the record or you may have lost your internet connection! Please try again", "Got it");
        }
        else
        {
            await _Users._AddUser(idd,txtfname.Text, txtlname.Text, txtcnum.Text, txtaddress.Text, txtuser.Text, txtpword.Text);

            txtfname.Text = string.Empty;
            txtlname.Text = string.Empty;
            txtcnum.Text = string.Empty;
            txtaddress.Text = string.Empty;
            txtuser.Text = string.Empty;
            txtpword.Text = string.Empty;
            await DisplayAlert("Added Successfully!!!", " ", "OK");
            progressLoading.IsVisible = true;
            await Navigation.PushAsync(new PageLogin());
            return;
        }

    }
    private async void termsCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var result = await DisplayAlert("Terms and Conditions & Privacy Policy for ThreeStar Veterinary Clinic",
   "Welcome to ThreeStar Veterinary Clinic!\r\n\r\n" +
   "By using our application, you agree to comply with and be bound by the following terms and conditions: Please read these terms carefully before using our services.\r\n\r\n" +
   "1. **User  Registration:**\r\n" +
   "   a. You agree to provide accurate and complete information during the registration process.\r\n" +
   "   b. You are responsible for maintaining the confidentiality of your account information.\r\n\r\n" +
   "2. **User  Content:**\r\n" +
   "   a. You are solely responsible for the content you upload, post, or share on the platform.\r\n" +
   "   b. Do not post content that violates intellectual property rights, privacy, or any applicable laws.\r\n\r\n" +
   "3. **Data Collection:**\r\n" +
   "   a. We collect personal information as outlined in our privacy policy.\r\n" +
   "   b. You agree to the collection, use, and disclosure of your data as described in our privacy policy.\r\n\r\n" +
   "4. **Service Listings:**\r\n" +
   "   a. Service providers are responsible for providing accurate and truthful information about their services.\r\n" +
   "   b. Clients are encouraged to verify service details before making a booking.\r\n\r\n" +
   "5. **Clinic Profiles:**\r\n" +
   "   a. Clinic owners are responsible for the accuracy of their clinic information.\r\n" +
   "   b. We reserve the right to verify and validate clinic profiles.\r\n\r\n" +
   "6. **Verification:**\r\n" +
   "   a. Some features may require user verification.\r\n" +
   "   b. Falsifying information during the verification process is prohibited.\r\n\r\n" +
   "7. **Code of Conduct:**\r\n" +
   "   a. Be respectful and considerate in all interactions.\r\n" +
   "   b. Do not engage in harassment, bullying, or any harmful behavior.\r\n\r\n" +
   "8. **Security:**\r\n" +
   "   a. Keep your account credentials secure.\r\n" +
   "   b. Report any security vulnerabilities promptly.\r\n\r\n" +
   "9. **Compliance:**\r\n" +
   "   a. Users must comply with all applicable laws and regulations.\r\n" +
   "   b. Non-compliance may result in account suspension or termination.\r\n\r\n" +
   "10. **Termination:**\r\n" +
   "    a. We reserve the right to terminate accounts for violations of these terms.\r\n" +
   "    b. Users may terminate their accounts at any time.\r\n\r\n" +
   "11. **Dispute Resolution:**\r\n" +
   "    a. Any disputes will be resolved in accordance with our dispute resolution policy.\r\n\r\n" +
   "12. **Changes to Terms:**\r\n" +
   "    a. We reserve the right to modify these terms at any time.\r\n" +
   "    b. Users will be notified of significant changes.\r\n\r\n" +
   "By using our application, you acknowledge that you have read and understood these terms and agree to be bound by them. If you do not agree with any part of these terms, please do not use our services.\r\n\r\n" +
   "For any questions or concerns, please contact us at THREESTARVET@GMAIL.COM.\r\n\r\n" +
   "Privacy Policy for ThreeStar Veterinary Clinic\r\n" +
   "Updated: 06/01/2024\r\n" +
   "1. **Introduction:**\r\n" +
   "   a. Welcome to ThreeStar Veterinary Clinic! This Privacy Policy describes how we collect, use, and disclose personal information when you use our application. By accessing or using ThreeStar Veterinary Clinic, you agree to the terms outlined in this policy.\r\n\r\n" +
   "2. **Information We Collect:**\r\n" +
   "   a. Personal Information: We may collect personal information such as your name, email address, and contact details when you register or use our services.\r\n" +
   "   b. Transaction Information: If you engage in transactions through ThreeStar Veterinary Clinic, we may collect information related to those transactions, including payment information.\r\n" +
   "   c. Usage Data: We may collect information about how you interact with ThreeStar Veterinary Clinic, such as your browsing history, search queries, and device information.\r\n\r\n" +
   "3. **How We Use Your Information:**\r\n" +
   "   a. We use the collected information for various purposes, including:\r\n" +
   "      1. Providing and improving ThreeStar Veterinary Clinic services.\r\n" +
   "      2. Processing transactions and payments.\r\n" +
   "      3. Customizing content and recommendations.\r\n" +
   "      4. Communicating with you about our services and updates.\r\n" +
   "      5. Analyzing usage patterns to enhance user experience.\r\n\r\n" +
   "4. **User -Provided Payment Information:**\r\n" +
   "   a. ThreeStar Veterinary Clinic allows users to choose their preferred payment methods for transactions. We do not store or process payment information directly. All payment transactions are handled by third-party payment processors, and their terms and policies apply.\r\n\r\n" +
   "5. **Data Security:**\r\n" +
   "   a. We implement security measures to protect your personal information. However, no method of transmission over the internet or electronic storage is entirely secure. We encourage you to take steps to protect your information, such as using strong passwords.\r\n\r\n" +
   "6. **Third-Party Links and Services:**\r\n" +
   "   a. ThreeStar Veterinary Clinic may contain links to third-party websites or services. These third-party sites have their privacy policies, and we are not responsible for their practices. We encourage you to review the privacy policies of these third parties.\r\n\r\n" +
   "7. **Changes to Privacy Policy:**\r\n" +
   "   a. We may update this Privacy Policy from time to time. We will notify you of any changes by posting the new policy on this page. We recommend reviewing this Privacy Policy periodically for any updates.\r\n\r\n" +
   "8. **Contact Us:**\r\n" +
   "   a. If you have any questions or concerns about this Privacy Policy, please contact us at THREESTARVET@GMAIL.COM.",
   "Agree", "Disagree");


        if (result)
        {
            // User agreed to the terms and conditions
            // You can add your logic here
        }
        else
        {

            termsCheckbox.IsChecked = false;
            return;// Uncheck the checkbox if the user disagrees
        }
    }


    private async void Linkuptosite_Tapped(object sender, TappedEventArgs e)
    {
        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await DisplayAlert("Alert!", "No internet connection. Please check your network settings.", "OK");
            return;
        }

        try
        {
            progressLoading.IsVisible = true; // Show the loading indicator

            // Open the URI asynchronously
            await Launcher.OpenAsync(new Uri("https://privacy.gov.ph/data-privacy-act/"));
        }
        catch (Exception ex)
        {
            // Handle exceptions, e.g., if the URI is invalid or the app doesn't have permission to open the URI.
            Console.WriteLine($"Error opening URI: {ex.Message}");
        }
        finally
        {
            progressLoading.IsVisible = false; // Hide the loading indicator in both success and failure cases
        }

    }


    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageLogin());
    }
}
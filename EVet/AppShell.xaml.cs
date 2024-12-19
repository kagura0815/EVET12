

namespace EVet
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
        }

        public AppShell(string userkey, string userfullname) : this()
        {
            InitializeComponent();
            App.UserkeyUser = userkey;
            App.fullNameUser = userfullname;
        }
    }
}

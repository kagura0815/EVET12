

namespace EVet
{
    public partial class AppShell2 : Shell
    {
        public AppShell2()
        {
            InitializeComponent();
            
        }

        public AppShell2(string userkey, string userfullname) : this()
        {
            InitializeComponent();
            App.UserkeyUser = userkey;
            App.fullNameUser = userfullname;
        }
    }
}

using StudentManager.MobileApp.Views;

namespace StudentManager.MobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ListStudents());
        }
    }
}

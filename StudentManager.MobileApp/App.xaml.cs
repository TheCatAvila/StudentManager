namespace StudentManager.MobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Views.ListStudents();
        }
    }
}

namespace StudentManager.MobileApp.Views;

public partial class ListStudents : ContentPage
{
	public ListStudents()
	{
		InitializeComponent();
	}

	private void FilterSearchBar_TextChanged(object sender, EventArgs e)
	{

	}

    private async void NewStudentBtn_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new CreateStudent());
    }
}
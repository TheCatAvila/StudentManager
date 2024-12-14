using Firebase.Database;
using StudentManager.Models.Models;
using System.Collections.ObjectModel;

namespace StudentManager.MobileApp.Views;

public partial class ListStudents : ContentPage
{
	FirebaseClient client = new FirebaseClient("https://studentmanager-ac31a-default-rtdb.firebaseio.com/");
	public ObservableCollection<Student> StudentsList {  get; set; } = new ObservableCollection<Student>();
	public ListStudents()
	{
		InitializeComponent();
		BindingContext = this;
		LoadList();
	}

	private void LoadList()
	{
		client.Child("Students").AsObservable<Student>().Subscribe(student =>
		{
			if (student != null)
			{
                StudentsList.Add(student.Object);
			}
		});
	}

    private void FilterSearchBar_TextChanged(object sender, EventArgs e)
	{

	}

    private async void NewStudentBtn_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new CreateStudent());
    }
}
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
                var studentWithId = student.Object;
                studentWithId.Id = student.Key; // Asocia la clave �nica con el modelo.

                // Aseg�rate de no duplicar estudiantes al actualizar la lista.
                if (!StudentsList.Any(s => s.Id == studentWithId.Id))
                {
                    StudentsList.Add(studentWithId);
                }
            }
        });
	}

    private void FilterSearchBar_TextChanged(object sender, EventArgs e)
	{
		string filter = filterSearchBar.Text.ToLower();

		if(filter.Length > 0)
		{
			CollectionList.ItemsSource = StudentsList.Where(x => x.FullName.ToLower().Contains(filter));

        }
		else
		{
			CollectionList.ItemsSource = StudentsList;
		}
	}

    private async void NewStudentBtn_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new CreateStudent());
    }

    private async void UpdateStudentBtn_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button; // El bot�n que dispar� el evento.
        var student = button.BindingContext as Student; // Obt�n el estudiante desde el BindingContext del bot�n.

        if (student != null)
        {
            await Navigation.PushAsync(new UpdateStudent(student)); // Pasa el estudiante al constructor.
        }
        else
        {
            await DisplayAlert("Error", "No se pudo identificar al estudiante.", "OK");
        }
    }

}
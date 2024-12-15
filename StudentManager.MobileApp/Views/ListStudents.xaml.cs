using Firebase.Database;
using StudentManager.Models.Models;
using System.Collections.ObjectModel;

namespace StudentManager.MobileApp.Views;

public partial class ListStudents : ContentPage
{
    FirebaseClient client = new FirebaseClient("https://studentmanager-ac31a-default-rtdb.firebaseio.com/");
    public ObservableCollection<Student> StudentsList { get; set; } = new ObservableCollection<Student>();

    public ListStudents()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadList();
    }

    private void LoadList()
    {
        StudentsList.Clear(); // Limpia la lista para evitar duplicados
        client.Child("Students").AsObservable<Student>().Subscribe(student =>
        {
            if (student != null)
            {
                var studentWithId = student.Object;
                studentWithId.Id = student.Key;

                if (studentWithId.State ?? false)
                {
                    if (!StudentsList.Any(s => s.Id == studentWithId.Id))
                    {
                        StudentsList.Add(studentWithId);
                    }
                }
                else
                {
                    var existingStudent = StudentsList.FirstOrDefault(s => s.Id == studentWithId.Id);
                    if (existingStudent != null)
                    {
                        StudentsList.Remove(existingStudent);
                    }
                }
            }
        });
    }

    private void FilterSearchBar_TextChanged(object sender, EventArgs e)
    {
        string filter = filterSearchBar.Text.ToLower();

        if (filter.Length > 0)
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

    private async void DetailsStudentBtn_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var student = button.BindingContext as Student;

        if (student != null)
        {
            await Navigation.PushAsync(new StudentDetails(student.Id));
        }
        else
        {
            await DisplayAlert("Error", "No se pudo identificar al estudiante.", "OK");
        }
    }
}

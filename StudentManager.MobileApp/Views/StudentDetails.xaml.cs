using Firebase.Database;
using Firebase.Database.Query;
using StudentManager.Models.Models;
using System.Collections.ObjectModel;

namespace StudentManager.MobileApp.Views;

public partial class StudentDetails : ContentPage
{
    FirebaseClient client = new FirebaseClient("https://studentmanager-ac31a-default-rtdb.firebaseio.com/");
    private string studentId;

    public StudentDetails(string studentId)
    {
        InitializeComponent();
        this.studentId = studentId;
        LoadStudentDetails();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadStudentDetails(); 
    }


    private async void LoadStudentDetails()
    {
        var student = await client.Child("Students").Child(studentId).OnceSingleAsync<Student>();


        if (student != null)
        {
            BindingContext = student;
        }
        else
        {
            await DisplayAlert("Error", "Estudiante no encontrado.", "OK");
        }
    }

    private async void DisableStudentBtn_Clicked(object sender, EventArgs e)
    {
        try
        {
            var button = sender as Button;
            if (button?.CommandParameter is not Student student)
            {
                await DisplayAlert("Error", "No se pudo encontrar el estudiante.", "OK");
                return;
            }

            if (string.IsNullOrEmpty(student.Id))
            {
                await DisplayAlert("Error", "El estudiante no tiene un ID válido.", "OK");
                return;
            }

            student.State = false;

            await client
                .Child("Students")
                .Child(student.Id)
                .PutAsync(student);

            await DisplayAlert("Éxito", $"El estudiante {student.FirstName} fue deshabilitado.", "OK");

            LoadStudentDetails();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void UpdateStudentBtn_Clicked(object sender, EventArgs e)
    {
        var student = await client
            .Child("Students")
            .Child(studentId) 
            .OnceSingleAsync<Student>();

        if (student != null)
        {

            student.Id = studentId;

            await Navigation.PushAsync(new UpdateStudent(student));
        }
        else
        {
            await DisplayAlert("Error", "No se pudo cargar el estudiante.", "OK");
        }
    }

}
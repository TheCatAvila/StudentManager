using Firebase.Database;
using Firebase.Database.Query;
using StudentManager.Models.Models;

namespace StudentManager.MobileApp.Views;

public partial class UpdateStudent : ContentPage
{
    FirebaseClient client = new FirebaseClient("https://studentmanager-ac31a-default-rtdb.firebaseio.com/");
    public List<Grade> Grades { get; set; }
    private Student CurrentStudent { get; set; }

    public UpdateStudent(Student student)
	{
		InitializeComponent();
        BindingContext = this;
        CurrentStudent = student;
        _ = ListGradesAsync();
        LoadStudentData();
    }

    private async Task ListGradesAsync()
    {
        var grades = await client.Child("Grade").OnceAsync<Grade>();
        Grades = grades.Select(x => x.Object).ToList();
        EditGradePicker.ItemsSource = Grades;

        // Selecciona el grado actual del estudiante en el picker.
        var currentGrade = Grades.FirstOrDefault(g => g.Name == CurrentStudent.Grade?.Name);
        if (currentGrade != null)
        {
            EditGradePicker.SelectedItem = currentGrade;
        }
    }

    private void LoadStudentData()
    {
        EditFirstNameEntry.Text = CurrentStudent.FirstName;
        EditMiddleNameEntry.Text = CurrentStudent.MiddleName;
        EditLastNameEntry.Text = CurrentStudent.LastName;
        EditSecondLastNameEntry.Text = CurrentStudent.SecondLastName;
        EditEmailEntry.Text = CurrentStudent.Email;
        EditAgeEntry.Text = CurrentStudent.Age.ToString();
    }

    private async void UpdateButton_Clicked(object sender, EventArgs e)
    {

        try
        {

            if (string.IsNullOrEmpty(CurrentStudent.Id))
            {
                await DisplayAlert("Error", "No se puede actualizar el estudiante porque no tiene un ID válido.", "OK");
                return;
            }

            CurrentStudent.FirstName = EditFirstNameEntry.Text;
            CurrentStudent.MiddleName = EditMiddleNameEntry.Text;
            CurrentStudent.LastName = EditLastNameEntry.Text;
            CurrentStudent.SecondLastName = EditSecondLastNameEntry.Text;
            CurrentStudent.Email = EditEmailEntry.Text;
            CurrentStudent.Age = int.Parse(EditAgeEntry.Text);
            CurrentStudent.Grade = EditGradePicker.SelectedItem as Grade;

            await client
                .Child("Students")
                .Child(CurrentStudent.Id) 
                .PutAsync(CurrentStudent);

            await DisplayAlert("Éxito", $"El estudiante {CurrentStudent.FirstName} fue actualizado correctamente.", "OK");
            LoadStudentData();
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

}
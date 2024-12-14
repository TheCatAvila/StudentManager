using Firebase.Database;
using Firebase.Database.Query;
using StudentManager.Models.Models;

namespace StudentManager.MobileApp.Views;

public partial class CreateStudent : ContentPage
{
	FirebaseClient client = new FirebaseClient("https://studentmanager-ac31a-default-rtdb.firebaseio.com/");
	public List<Grade> Grades { get; set; }

	public CreateStudent()
	{
		InitializeComponent();
		ListGrades();
		BindingContext = this;
	}

    private void ListGrades()
    {
        var grades = client.Child("Grade").OnceAsync<Grade>();
        Grades = grades.Result.Select(x => x.Object).ToList();
    }


    private async void saveButton_Clicked(object sender, EventArgs e)
	{
		Grade grade = gradePicker.SelectedItem as Grade;

		var student = new Student
		{
			FirstName = firstNameEntry.Text,
			MiddleName = middleNameEntry.Text,
			LastName = lastNameEntry.Text,
			SecondLastName = SecondLastNameEntry.Text,
			Email = emailEntry.Text,
			Age = int.Parse(ageEntry.Text),
			Grade = grade
		};

		try
		{
			await client.Child("Students").PostAsync(student);
			await DisplayAlert("Éxito", $"El estudiante {student.FirstName} {student.LastName} fue agregado correctamente.", "OK");
			await Navigation.PopAsync();
		}
		catch (Exception ex)
		{
            await DisplayAlert("Error", ex.Message, "OK");
        }
	}
}
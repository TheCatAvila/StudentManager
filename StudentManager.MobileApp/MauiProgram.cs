using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Extensions.Logging;
using StudentManager.Models.Models;

namespace StudentManager.MobileApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            Register();

            return builder.Build();
        }

        public static async Task Register()
        {
            FirebaseClient client = new FirebaseClient("https://studentmanager-ac31a-default-rtdb.firebaseio.com/");

            var grades = await client.Child("Grade").OnceAsync<Grade>();

            if(grades.Count == 0)
            {
                await client.Child("Grade").PostAsync(new Grade { Name = "Pre-kínder" });
                await client.Child("Grade").PostAsync(new Grade { Name = "Kínder" });
                await client.Child("Grade").PostAsync(new Grade { Name = "1° Básico" });
                await client.Child("Grade").PostAsync(new Grade { Name = "2° Básico" });
                await client.Child("Grade").PostAsync(new Grade { Name = "3° Básico" });
                await client.Child("Grade").PostAsync(new Grade { Name = "4° Básico" });
                await client.Child("Grade").PostAsync(new Grade { Name = "5° Básico" });
                await client.Child("Grade").PostAsync(new Grade { Name = "6° Básico" });
                await client.Child("Grade").PostAsync(new Grade { Name = "7° Básico" });
                await client.Child("Grade").PostAsync(new Grade { Name = "8° Básico" });
                await client.Child("Grade").PostAsync(new Grade { Name = "1° Medio" });
                await client.Child("Grade").PostAsync(new Grade { Name = "2° Medio" });
                await client.Child("Grade").PostAsync(new Grade { Name = "3° Medio" });
                await client.Child("Grade").PostAsync(new Grade { Name = "4° Medio" });
            }
            else
            {
                //foreach(var grade in grades)
                //{
                //    if(grade.Object.State == null)
                //    {
                //        var gradeUpdated = grade.Object;
                //        gradeUpdated.State = true;

                //        await client.Child("Grade").Child(grade.Key).PutAsync(gradeUpdated);
                //    }
                //}
            }
        }
    }
}

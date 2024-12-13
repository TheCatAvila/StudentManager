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

        public static void Register()
        {
            FirebaseClient client = new FirebaseClient("https://studentmanager-ac31a-default-rtdb.firebaseio.com/");

            var grade = client.Child("Grade").OnceAsync<Grade>();

            if(grade.Result.Count == 0)
            {
                client.Child("Grade").PostAsync(new Grade { Name = "1° Básico" });
                client.Child("Grade").PostAsync(new Grade { Name = "2° Básico" });
                client.Child("Grade").PostAsync(new Grade { Name = "3° Básico" });
                client.Child("Grade").PostAsync(new Grade { Name = "4° Básico" });
                client.Child("Grade").PostAsync(new Grade { Name = "5° Básico" });
                client.Child("Grade").PostAsync(new Grade { Name = "6° Básico" });
                client.Child("Grade").PostAsync(new Grade { Name = "7° Básico" });
                client.Child("Grade").PostAsync(new Grade { Name = "8° Básico" });
                client.Child("Grade").PostAsync(new Grade { Name = "1° Medio" });
                client.Child("Grade").PostAsync(new Grade { Name = "2° Medio" });
                client.Child("Grade").PostAsync(new Grade { Name = "3° Medio" });
                client.Child("Grade").PostAsync(new Grade { Name = "4° Medio" });
            }
        }
    }
}

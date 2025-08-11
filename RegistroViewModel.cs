using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PROY10DEMO.Helpers;
using PROY10DEMO.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PROY10DEMO.ViewModels;

public partial class RegistroViewModel : ObservableObject
{
    private readonly FirebaseHelper firebaseHelper = new FirebaseHelper();

    [ObservableProperty] private string nombre;
    [ObservableProperty] private string email;
    [ObservableProperty] private string password;
    [ObservableProperty] private string rol = "Usuario";
    [ObservableProperty] private bool isBusy;

    public IAsyncRelayCommand RegistrarCommand { get; }

    public RegistroViewModel()
    {
        RegistrarCommand = new AsyncRelayCommand(Registrar);
    }

    private async Task Registrar()
    {
        if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
        {
            await App.Current.MainPage.DisplayAlert("Error", "Completa todos los campos", "OK");
            return;
        }

        IsBusy = true;

        if (await firebaseHelper.ExisteUsuario(Email))
        {
            IsBusy = false;
            await App.Current.MainPage.DisplayAlert("Error", "Ya existe un usuario con este correo", "OK");
            return;
        }

        Usuario nuevoUsuario = new Usuario
        {
            Nombre = Nombre,
            Email = Email,
            Password = Password,
            Rol = Rol
        };

        await firebaseHelper.AddUsuario(nuevoUsuario);
        IsBusy = false;

        await App.Current.MainPage.DisplayAlert("Éxito", "Usuario registrado correctamente", "OK");
        await Shell.Current.GoToAsync("//LoginPage");
    }
}

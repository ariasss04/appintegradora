using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PROY10DEMO.Helpers;
using PROY10DEMO.Models;

namespace PROY10DEMO.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly FirebaseHelper firebaseHelper = new FirebaseHelper();

    [ObservableProperty] private string email;
    [ObservableProperty] private string password;
    [ObservableProperty] private bool isBusy;

    public IAsyncRelayCommand LoginCommand { get; }

    public LoginViewModel()
    {
        LoginCommand = new AsyncRelayCommand(Login);
    }

    private async Task Login()
    {
        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
        {
            await App.Current.MainPage.DisplayAlert("Error", "Completa los campos", "OK");
            return;
        }

        IsBusy = true;
        var usuario = await firebaseHelper.LoginUsuario(Email, Password);
        IsBusy = false;

        if (usuario != null)
        {
            if (usuario.Rol == "Admin")
                await Shell.Current.GoToAsync("//PanelAdminPage");
            else
                await Shell.Current.GoToAsync("//PanelUsuarioPage");
        }
        else
        {
            await App.Current.MainPage.DisplayAlert("Error", "Correo o contraseña incorrectos", "OK");
        }
    }
}

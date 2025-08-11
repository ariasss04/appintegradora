using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PROY10DEMO.Helpers;
using PROY10DEMO.Models;
using System.Collections.ObjectModel;

namespace PROY10DEMO.ViewModels;

public partial class UsuarioViewModel : ObservableObject
{
    private readonly FirebaseHelper firebaseHelper = new FirebaseHelper();

    [ObservableProperty]
    private ObservableCollection<Usuario> usuarios = new ObservableCollection<Usuario>();

    public UsuarioViewModel()
    {
        CargarUsuariosCommand = new AsyncRelayCommand(CargarUsuarios);
        EliminarUsuarioCommand = new AsyncRelayCommand<string>(EliminarUsuario);
    }

    public IAsyncRelayCommand CargarUsuariosCommand { get; }
    public IAsyncRelayCommand<string> EliminarUsuarioCommand { get; }

    private async Task CargarUsuarios()
    {
        Usuarios.Clear();
        var lista = await firebaseHelper.GetAllUsuarios();
        foreach (var u in lista)
            Usuarios.Add(u);
    }

    private async Task EliminarUsuario(string id)
    {
        await firebaseHelper.DeleteUsuario(id);
        await CargarUsuarios();
    }
}

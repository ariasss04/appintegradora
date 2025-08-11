using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace PROY10DEMO.ViewModels
{
    public partial class ForgotPasswordViewModel : ObservableObject
    {
        [ObservableProperty]
        private string email;

        public ICommand SendResetEmailCommand { get; }

        public ForgotPasswordViewModel()
        {
            SendResetEmailCommand = new RelayCommand(OnSendResetEmail);
        }

        private void OnSendResetEmail()
        {
            // Simula el envío del correo
            Debug.WriteLine($"Correo de recuperación enviado a: {Email}");
        }
    }
}

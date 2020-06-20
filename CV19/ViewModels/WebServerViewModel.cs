using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.Services.Interfaces;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
    internal class WebServerViewModel : ViewModel
    {
        private readonly IWebServerService _Server;

        #region Enabled

        private bool _Enabled;

        public bool Enabled { get => _Enabled; set => Set(ref _Enabled, value); }

        #endregion

        #region StartCommand

        private ICommand _StartCommand;

        public ICommand StartCommand => _StartCommand
            ??= new LambdaCommand(OnStartCommandExecuted, CanStartCommandExecute);

        private bool CanStartCommandExecute(object p) => !_Enabled;

        private void OnStartCommandExecuted(object p)
        {
            Enabled = true;
        }

        #endregion

        #region StopCommand

        private ICommand _StopCommand;

        public ICommand StopCommand => _StopCommand
            ??= new LambdaCommand(OnStopCommandExecuted, CanStopCommandExecute);

        private bool CanStopCommandExecute(object p) => _Enabled;

        private void OnStopCommandExecuted(object p)
        {
            Enabled = false;
        }

        #endregion

        public WebServerViewModel(IWebServerService Server) => _Server = Server;
    }
}

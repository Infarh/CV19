using System.Windows;
using CV19.Infrastructure.Commands.Base;

namespace CV19.Infrastructure.Commands
{
    /// <summary>
    /// Команда закрытие приложения
    /// </summary>
    internal class CloseApplicationCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => Application.Current.Shutdown();
    }
}

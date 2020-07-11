using System;
using System.Windows;
using CV19.Infrastructure.Commands.Base;
using CV19.Views.Windows;

namespace CV19.Infrastructure.Commands
{
    class ManageStudentsCommand : Command
    {
        private StudentsManagementWindow _Window;

        public override bool CanExecute(object parameter) => _Window == null;

        public override void Execute(object parameter)
        {
            var window = new StudentsManagementWindow
            {
                Owner = Application.Current.MainWindow
            };
            _Window = window;
            window.Closed += OnWindowClosed;

            window.ShowDialog();
        }

        private void OnWindowClosed(object Sender, EventArgs E)
        {
            ((Window) Sender).Closed -= OnWindowClosed;
            _Window = null;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace CV19.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Host.Services.GetRequiredService<MainWindowViewModel>();

        public StudentsManagementViewModel StudentManagement => App.Host.Services.GetRequiredService<StudentsManagementViewModel>();
    }
}

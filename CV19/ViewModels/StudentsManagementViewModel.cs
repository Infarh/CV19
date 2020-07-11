using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.Models.Decanat;
using CV19.Services.Interfaces;
using CV19.Services.Students;
using CV19.ViewModels.Base;
using CV19.Views.Windows;

namespace CV19.ViewModels
{
    class StudentsManagementViewModel : ViewModel
    {
        private readonly StudentsManager _StudentsManager;
        private readonly IUserDialogService _UserDialog;

        public IEnumerable<Student> Students => _StudentsManager.Students;

        public IEnumerable<Group> Groups => _StudentsManager.Groups;

        #region Title : string - Заголовок окна

        /// <summary>Заголовок окна</summary>
        private string _Title = "Управление студентами";

        /// <summary>Заголовок окна</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

        #region SelectedGroup : Group - Выбранная группа студентов

        /// <summary>Выбранная группа студентов</summary>
        private Group _SelectedGroup;

        /// <summary>Выбранная группа студентов</summary>
        public Group SelectedGroup { get => _SelectedGroup; set => Set(ref _SelectedGroup, value); }

        #endregion

        #region SelectedStudent : Student - Выбранный студент

        /// <summary>Выбранный студент</summary>
        private Student _SelectedStudent;

        /// <summary>Выбранный студент</summary>
        public Student SelectedStudent { get => _SelectedStudent; set => Set(ref _SelectedStudent, value); }

        #endregion

        #region Команды

        #region EditStudentCommand - Команда редактирования студента

        private ICommand _EditStudentCommand;

        /// <summary>Команда редактирования студента</summary>
        public ICommand EditStudentCommand => _EditStudentCommand ??= new LambdaCommand(OnEditStudentCommandExecuted, CanEditStudentCommandExecute);

        private static bool CanEditStudentCommandExecute(object p) => p is Student;

        private void OnEditStudentCommandExecuted(object p)
        {
            if (_UserDialog.Edit(p))
            {
                _StudentsManager.Update((Student) p);

                _UserDialog.ShowInformation("Студент отредактирован", "Менеджер студентов");
            }
            else
                _UserDialog.ShowWarning("Отказ от редактирования", "Менеджер студентов");
        }

        #endregion

        #region Command CreateNewStudentCommand - Создание нового студента

        /// <summary>Создание нового студента</summary>
        private ICommand _CreateNewStudentCommand;

        /// <summary>Создание нового студента</summary>
        public ICommand CreateNewStudentCommand => _CreateNewStudentCommand
            ??= new LambdaCommand(OnCreateNewStudentCommandExecuted, CanCreateNewStudentCommandExecute);

        /// <summary>Проверка возможности выполнения - Создание нового студента</summary>
        private static bool CanCreateNewStudentCommandExecute(object p) => p is Group;

        /// <summary>Логика выполнения - Создание нового студента</summary>
        private void OnCreateNewStudentCommandExecuted(object p)
        {
            var group = (Group) p;

            var student = new Student();

            if (!_UserDialog.Edit(student) || _StudentsManager.Create(student, group.Name))
            {
                OnPropertyChanged(nameof(Students));
                return;
            }

            if(_UserDialog.Confirm("Не удалось создать студента. Повторить?", "Менеджер студентов"))
                OnCreateNewStudentCommandExecuted(p);
        }

        #endregion

        #region Command TestCommand - Тестовая команда

        /// <summary>Тестовая команда</summary>
        private ICommand _TestCommand;

        /// <summary>Тестовая команда</summary>
        public ICommand TestCommand => _TestCommand
            ??= new LambdaCommand(OnTestCommandExecuted, CanTestCommandExecute);

        /// <summary>Проверка возможности выполнения - Тестовая команда</summary>
        private bool CanTestCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Тестовая команда</summary>
        private void OnTestCommandExecuted(object p)
        {
            var value = _UserDialog.GetStringValue("Введите строку", "123", "Значение по умолчанию");

            _UserDialog.ShowInformation($"Введено: {value}", "123");
        }

        #endregion

        #endregion

        public StudentsManagementViewModel(StudentsManager StudentsManager, IUserDialogService UserDialog)
        {
            _StudentsManager = StudentsManager;
            _UserDialog = UserDialog;
        }
    }
}

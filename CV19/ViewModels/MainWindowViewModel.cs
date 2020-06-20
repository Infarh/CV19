using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using CV19.Infrastructure.Commands;
using CV19.Models.Decanat;
using CV19.Services.Interfaces;
using CV19.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;
using DataPoint = CV19.Models.DataPoint;

namespace CV19.ViewModels
{
    [MarkupExtensionReturnType(typeof(MainWindowViewModel))]
    internal class MainWindowViewModel : ViewModel
    {
        private readonly IAsyncDataService _AsyncData;

        /* ---------------------------------------------------------------------------------------------------- */

        public CountriesStatisticViewModel CountriesStatistic { get; }

        public WebServerViewModel WebServer { get; }

        #region StudentFilterText : string - Текст фильтра студентов

        /// <summary>Текст фильтра студентов</summary>
        private string _StudentFilterText;

        /// <summary>Текст фильтра студентов</summary>
        public string StudentFilterText
        {
            get => _StudentFilterText;
            set
            {
                if (!Set(ref _StudentFilterText, value)) return;
                _SelectedGroupStudents.View.Refresh();
            }
        }

        #endregion

        #region SelectedGroupStudents

        private readonly CollectionViewSource _SelectedGroupStudents = new CollectionViewSource();

        private void OnStudentFiltred(object Sender, FilterEventArgs E)
        {
            if (!(E.Item is Student student))
            {
                E.Accepted = false;
                return;
            }

            var filter_text = _StudentFilterText;
            if (string.IsNullOrWhiteSpace(filter_text))
                return;

            if (student.Name is null || student.Surname is null || student.Patronymic is null)
            {
                E.Accepted = false;
                return;
            }

            if(student.Name.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
            if(student.Surname.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
            if(student.Patronymic.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;

            E.Accepted = false;
        }

        public ICollectionView SelectedGroupStudents => _SelectedGroupStudents?.View;

        #endregion

        #region SelectedPageIndex : int - Номер выбранной вкладки

        /// <summary>Номер выбранной вкладки</summary>
        private int _SelectedPageIndex = 1;

        /// <summary>Номер выбранной вкладки</summary>
        public int SelectedPageIndex
        {
            get => _SelectedPageIndex;
            set => Set(ref _SelectedPageIndex, value);
        }

        #endregion

        #region TestDataPoints : IEnumerable<DataPoint> - Тестовый набор данных для визуализации графиков

        /// <summary>Тестовый набор данных для визуализации графиков</summary>
        private IEnumerable<DataPoint> _TestDataPoints;

        /// <summary>Тестовый набор данных для визуализации графиков</summary>
        public IEnumerable<DataPoint> TestDataPoints
        {
            get => _TestDataPoints;
            set => Set(ref _TestDataPoints, value);
        }

        #endregion

        #region Заголовок окна

        private string _Title = "Анализ статистики CV19";

        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _Title;
            //set
            //{
            //    //if (Equals(_Title, value)) return;
            //    //_Title = value;
            //    //OnPropertyChanged();

            //    Set(ref _Title, value);
            //}
            set => Set(ref _Title, value);
        }

        #endregion

        #region Status : string - Статус программы

        /// <summary>Статус программы</summary>
        private string _Status = "Готов!";

        /// <summary>Статус программы</summary>
        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }

        #endregion

        public IEnumerable<Student> TestStudents =>
            Enumerable.Range(1, App.IsDesignMode ? 10 : 100_000)
               .Select(i => new Student
               {
                   Name = $"Имя {i}",
                   Surname = $"Фамилия {i}"
               });

        #region DataValue : string - Результат длительной асинхронной операции

        /// <summary>Результат длительной асинхронной операции</summary>
        private string _DataValue;

        /// <summary>Результат длительной асинхронной операции</summary>
        public string DataValue { get => _DataValue; private set => Set(ref _DataValue, value); }

        #endregion

        /* ---------------------------------------------------------------------------------------------------- */

        #region Команды

        #region CloseApplicationCommand

        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object p) => true;

        private void OnCloseApplicationCommandExecuted(object p)
        {
            (RootObject as Window)?.Close();
            //Application.Current.Shutdown();
        }

        #endregion

        #region ChangeTabIndexCommand

        public ICommand ChangeTabIndexCommand { get; }

        private bool CanChangeTabIndexCommandExecute(object p) => _SelectedPageIndex >= 0;

        private void OnChangeTabIndexCommandExecuted(object p)
        {
            if (p is null) return;
            SelectedPageIndex += Convert.ToInt32(p);
        }

        #endregion

        #region Command StartProcessCommand - Запуск процесса

        /// <summary>Запуск процесса</summary>
        public ICommand StartProcessCommand { get; }

        /// <summary>Проверка возможности выполнения - Запуск процесса</summary>
        private static bool CanStartProcessCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Запуск процесса</summary>
        private void OnStartProcessCommandExecuted(object p)
        {
            new Thread(ComputeValue).Start()
;       }

        private void ComputeValue()
        {
            DataValue = _AsyncData.GetResult(DateTime.Now);
        }

        #endregion

        #region Command StopProcessCommand - Остановка процесса

        /// <summary>Остановка процесса</summary>
        public ICommand StopProcessCommand { get; }

        /// <summary>Проверка возможности выполнения - Остановка процесса</summary>
        private static bool CanStopProcessCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Остановка процесса</summary>
        private void OnStopProcessCommandExecuted(object p)
        {
            
        }

        #endregion

        #endregion

        /* ---------------------------------------------------------------------------------------------------- */

        public MainWindowViewModel(CountriesStatisticViewModel Statistic, IAsyncDataService AsyncData, WebServerViewModel WebServer)
        {
            _AsyncData = AsyncData;
            CountriesStatistic = Statistic;
            this.WebServer = WebServer;
            Statistic.MainModel = this;
            //CountriesStatistic = new CountriesStatisticViewModel(this);

            #region Команды

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);

            StartProcessCommand = new LambdaCommand(OnStartProcessCommandExecuted, CanStartProcessCommandExecute);
            StopProcessCommand = new LambdaCommand(OnStopProcessCommandExecuted, CanStopProcessCommandExecute);

            #endregion

            var data_points = new List<DataPoint>((int)(360 / 0.1));
            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(x * to_rad);

                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }

            TestDataPoints = data_points;
        }


        /* ---------------------------------------------------------------------------------------------------- */

    }
}

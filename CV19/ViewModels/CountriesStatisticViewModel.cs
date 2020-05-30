using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModel
    {
        private readonly DataService _DataService;

        private MainWindowViewModel MainModel { get; }

        #region Contries : IEnumerable<CountryInfo> - Статистика по странам

        /// <summary>Статистика по странам</summary>
        private IEnumerable<CountryInfo> _Countries;

        /// <summary>Статистика по странам</summary>
        public IEnumerable<CountryInfo> Countries
        {
            get => _Countries;
            private set => Set(ref _Countries, value);
        }

        #endregion

        #region SelectedCountry : CountryInfo - Выбранная страна

        /// <summary>Выбранная страна</summary>
        private CountryInfo _SelectedCountry;

        /// <summary>Выбранная страна</summary>
        public CountryInfo SelectedCountry { get => _SelectedCountry; set => Set(ref _SelectedCountry, value); }

        #endregion

        #region Команды

        public ICommand RefreshDataCommand { get; }

        private void OnRefreshDataCommandExecuted(object p)
        {
            Countries = _DataService.GetData();
        }

        #endregion

        /// <summary>Отладочный конструктор, используемый в процессе разработки в визуальном дизайнере</summary>
        public CountriesStatisticViewModel() : this(null)
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException("Вызов конструктора, непредназначенного для использования в обычном режиме");

            _Countries = Enumerable.Range(1, 10)
               .Select(i => new CountryInfo
               {
                   Name = $"Country {i}",
                   Provinces = Enumerable.Range(1, 10).Select(j => new PlaceInfo
                   {
                       Name = $"Province {i}",
                       Location = new Point(i, j),
                       Counts = Enumerable.Range(1, 10).Select(k => new ConfirmedCount
                       {
                           Date = DateTime.Now.Subtract(TimeSpan.FromDays(100 - k)),
                           Count = k
                       }).ToArray()
                   }).ToArray()
               }).ToArray();
        }

        public CountriesStatisticViewModel(MainWindowViewModel MainModel)
        {
            this.MainModel = MainModel;

            _DataService = new DataService();

            #region Команды

            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecuted);

            #endregion
        }
    }
}

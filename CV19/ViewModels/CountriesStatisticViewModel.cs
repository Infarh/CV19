using System;
using System.Collections.Generic;
using System.Text;
using CV19.Services;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModel
    {
        private DataService _DataService;

        private MainWindowViewModel MainModel { get; }



        public CountriesStatisticViewModel(MainWindowViewModel MainModel)
        {
            MainModel = MainModel;

            _DataService = new DataService();
        }
    }
}

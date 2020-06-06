using System;
using System.Collections.Generic;
using System.Text;
using CV19.Models;

namespace CV19.Services.Interfaces
{
    internal interface IDataService
    {
        IEnumerable<CountryInfo> GetData();
    }
}

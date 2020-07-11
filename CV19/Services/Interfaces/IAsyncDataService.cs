using System;
using System.Collections.Generic;
using System.Text;

namespace CV19.Services.Interfaces
{
    internal interface IAsyncDataService
    {
        string GetResult(DateTime Time);
    }
}

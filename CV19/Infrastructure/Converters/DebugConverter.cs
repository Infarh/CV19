using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CV19.Infrastructure.Converters
{
    internal class DebugConverter : Converter
    {
        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            System.Diagnostics.Debugger.Break();
            return v;
        }

        public override object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            System.Diagnostics.Debugger.Break();
            return v;
        }
    }
}

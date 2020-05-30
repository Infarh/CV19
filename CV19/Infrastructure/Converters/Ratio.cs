using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace CV19.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(Ratio))]
    internal class Ratio : Converter
    {
        [ConstructorArgument("K")]
        public double K { get; set; } = 1;

        public Ratio() { }

        public Ratio(double K) => this.K = K;

        public override object Convert(object value, Type t, object p, CultureInfo c)
        {
            if (value is null) return null;

            var x = System.Convert.ToDouble(value, c);

            return x * K;
        }

        public override object ConvertBack(object value, Type t, object p, CultureInfo c)
        {
            if (value is null) return null;

            var x = System.Convert.ToDouble(value, c);

            return x / K;
        }
    }
}

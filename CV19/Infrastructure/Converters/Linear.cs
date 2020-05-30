using System;
using System.Globalization;

namespace CV19.Infrastructure.Converters
{
    /// <summary>Реализация линейного преобразования f(x) = k*x + b</summary>
    internal class Linear : Converter
    {
        public double K { get; set; } = 1;

        public double B { get; set; }

        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            if (v is null) return null;

            var x = System.Convert.ToDouble(v, c);
            return K * x + B;
        }

        public override object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            if (v is null) return null;

            var y = System.Convert.ToDouble(v, c);

            return (y - B) / K;
        }
    }
}

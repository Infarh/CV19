using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CV19.Infrastructure.Converters
{
    internal class ParametricMultiplicityValueConverter : Freezable, IValueConverter
    {
        #region Value : double - Прибавляемое значение

        /// <summary>Прибавляемое значение</summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(double),
                typeof(ParametricMultiplicityValueConverter),
                new PropertyMetadata(1.0/*, (d,e) => { }*/));

        /// <summary>Прибавляемое значение</summary>
        //[Category("")]
        [Description("Прибавляемое значение")]
        public double Value { get => (double) GetValue(ValueProperty); set => SetValue(ValueProperty, value); }

        #endregion

        public object Convert(object v, Type t, object p, CultureInfo c)
        {
            var value = System.Convert.ToDouble(v, c);

            return value * Value;
        }

        public object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            var value = System.Convert.ToDouble(v, c);

            return value / Value;
        }

        protected override Freezable CreateInstanceCore() => new ParametricMultiplicityValueConverter { Value = Value };
    }
}

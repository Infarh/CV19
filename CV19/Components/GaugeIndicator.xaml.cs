using System;
using System.ComponentModel;
using System.Windows;

namespace CV19.Components
{
    public partial class GaugeIndicator
    {
        #region ValueProperty

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(double),
                typeof(GaugeIndicator),
                new PropertyMetadata(
                    default(double),
                    OnValuePropertyChanged,
                    OnCoerceValue),
                OnValidateValue);

        private static bool OnValidateValue(object O)
        {
            return true;
        }

        private static object OnCoerceValue(DependencyObject D, object Basevalue)
        {
            var value = (double) Basevalue;
            return Math.Max(0, Math.Min(100, value));
        }

        private static void OnValuePropertyChanged(DependencyObject D, DependencyPropertyChangedEventArgs E)
        {
            //var gauge_indicator = (GaugeIndicator) D;
            //gauge_indicator.ArrowRotator.Angle = (double) E.NewValue;
        }

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        } 

        #endregion

        #region Angle : double - Какой-то угол

        /// <summary>Какой-то угол</summary>
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register(
                nameof(Angle),
                typeof(double),
                typeof(GaugeIndicator),
                new PropertyMetadata(default(double)));

        /// <summary>Какой-то угол</summary>
        [Category("Моя категория!")]
        [Description("Какой-то угол")]
        public double Angle { get => (double) GetValue(AngleProperty); set => SetValue(AngleProperty, value); }

        #endregion

        public GaugeIndicator() => InitializeComponent();
    }
}

using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace CV19.Infrastructure.Behaviors
{
    class WindowStateChange : Behavior<Button>
    {
        protected override void OnAttached() => AssociatedObject.Click += OnButtonClick;

        protected override void OnDetaching() => AssociatedObject.Click -= OnButtonClick;

        private void OnButtonClick(object Sender, RoutedEventArgs E)
        {
            if (!(AssociatedObject.FindVisualRoot() is Window window)) return;

            //switch (window.WindowState)
            //{
            //    case WindowState.Normal:
            //        window.WindowState = WindowState.Maximized;
            //        break;
            //    case WindowState.Maximized:
            //        window.WindowState = WindowState.Normal;
            //        break;
            //}

            window.WindowState = window.WindowState switch
            {
                WindowState.Normal => WindowState.Maximized,
                WindowState.Maximized => WindowState.Normal,
                _ => window.WindowState
            };
        }
    }
}

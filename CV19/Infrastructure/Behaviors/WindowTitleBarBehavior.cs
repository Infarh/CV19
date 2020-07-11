using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace CV19.Infrastructure.Behaviors
{
    class WindowTitleBarBehavior : Behavior<UIElement>
    {
        private Window _Window;

        protected override void OnAttached()
        {
            _Window = AssociatedObject as Window ?? AssociatedObject.FindLogicalParent<Window>();
            AssociatedObject.MouseLeftButtonDown += OnMouseDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= OnMouseDown;
            _Window = null;
        }

        private void OnMouseDown(object Sender, MouseButtonEventArgs E)
        {
            switch (E.ClickCount)
            {
                case 1:
                    DragMove();
                    break;
                default:
                    Maximize();
                    break;
            }
        }

        private void DragMove()
        {
            if (!(AssociatedObject.FindVisualRoot() is Window window)) return;
            window.DragMove();
        }

        private void Maximize()
        {
            if (!(AssociatedObject.FindVisualRoot() is Window window)) return;
            window.WindowState = window.WindowState switch
            {
                WindowState.Normal => WindowState.Maximized,
                WindowState.Maximized => WindowState.Normal,
                _ => window.WindowState
            };
        }
    }
}

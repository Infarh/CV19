using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;

namespace CV19WPFTest.Behaviors
{
    public class DragInCanvas : Behavior<UIElement>
    {
        private Point _StartPoint;
        private Canvas _Canvas;

        protected override void OnAttached()
        {
            AssociatedObject.MouseLeftButtonDown += OnButtonDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= OnButtonDown;
            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.MouseUp -= OnMouseUp;
        }

        private void OnButtonDown(object Sender, MouseButtonEventArgs E)
        {
            //if (_Canvas is null)
            //{
            //    _Canvas = VisualTreeHelper.GetParent(AssociatedObject) as Canvas;
            //    if (_Canvas is null) return;
            //}

            //var canvas = _Canvas ??= VisualTreeHelper.GetParent(AssociatedObject) as Canvas;
            //          ==
            //var canvas = _Canvas ?? (_Canvas = VisualTreeHelper.GetParent(AssociatedObject) as Canvas);

            if ((_Canvas ??= VisualTreeHelper.GetParent(AssociatedObject) as Canvas) is null)
                return;

            _StartPoint = E.GetPosition(AssociatedObject);
            AssociatedObject.CaptureMouse();
            AssociatedObject.MouseMove += OnMouseMove;
            AssociatedObject.MouseUp += OnMouseUp;
        }

        private void OnMouseUp(object Sender, MouseButtonEventArgs E)
        {
            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.MouseUp -= OnMouseUp;
            AssociatedObject.ReleaseMouseCapture();
        }

        private void OnMouseMove(object Sender, MouseEventArgs E)
        {
            var obj = AssociatedObject;
            var current_pos = E.GetPosition(_Canvas);

            var delta = current_pos - _StartPoint;

            obj.SetValue(Canvas.LeftProperty, delta.X);
            obj.SetValue(Canvas.TopProperty, delta.Y);
        }
    }
}

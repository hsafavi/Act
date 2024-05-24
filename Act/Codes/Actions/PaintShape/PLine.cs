using System;
using System.Windows;
using System.Windows.Shapes;
using Act.Codes.Controls;

namespace Act.Codes.Actions.PaintShape
{
    class PLine : ShapeBase
    {
        private bool _first = true;
        private Point _p1;
        private bool _moving;
        private Line _line;
        private Point _p;

        public override bool IsNormal
        {
            get
            {
                return false;
            }
        }

        public PLine(myCanvas canvas) : base(canvas)
        {

        }

        public override void End()
        {
            Canvas.MouseLeftButtonDown -= Canvas_MouseLeftButtonDown;
            Canvas.MouseLeftButtonUp -= Canvas_MouseLeftButtonUp;
            Canvas.MouseMove -= Canvas_MouseMove;
        }

        public override Shape New()
        {
            return _line;
        }

        public override void Start()
        {
            _line = new Line();
            Canvas.MouseLeftButtonDown += Canvas_MouseLeftButtonDown;
            Canvas.MouseLeftButtonUp += Canvas_MouseLeftButtonUp;
            Canvas.MouseMove += Canvas_MouseMove;
        }

        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _p = e.GetPosition(Canvas);
            if (_moving)
            {
                _line.X2 = _p.X;
                _line.Y2 = _p.Y;
            }

        }

        private void Canvas_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void Canvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_first)
            {
                _p1 = e.GetPosition(Canvas);
                _line.X1 = _line.X2 = _p1.X;
                _line.Y1 = _line.Y2 = _p1.Y;

                _first = false;
                _moving = true;
                Canvas.Children.Add(_line);
            }
            else
            {
                _first = true;
                End();
                if (_p1.X < _p.X)
                {
                    myCanvas.SetLeft(_line, _p1.X);
                    _line.X1 = 0;
                    _line.X2 -= _p1.X;
                }
                else
                {
                    myCanvas.SetLeft(_line, _p.X);
                    _line.X1 -= _p.X;
                    _line.X2 = 0;
                }

                if (_p1.Y < _p.Y)
                {
                    myCanvas.SetTop(_line, _p1.Y);
                    _line.Y1 = 0;
                    _line.Y2 -= _p1.Y;
                }
                else
                {
                    myCanvas.SetTop(_line, _p.Y);
                    _line.Y1 -= _p.Y;
                    _line.Y2 = 0;
                }
                _line.Width = Math.Abs(_line.X2 - _line.X1);
                _line.Height = Math.Abs(_line.Y2 - _line.Y1);
                _line.Stretch = System.Windows.Media.Stretch.Uniform;
                onCompleted();
            }



        }
    }
}

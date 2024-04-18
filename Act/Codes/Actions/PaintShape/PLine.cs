using System;
using System.Windows;
using System.Windows.Shapes;
using WpfPaint.Codes.Controls;

namespace WpfPaint.Codes.Actions.PaintShape
{
    class PLine : ShapeAbstract
    {
        private bool first = true;
        private Point p1;
        private bool moving;
        private Line line;
        private Point p;

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
            canvas.MouseLeftButtonDown -= Canvas_MouseLeftButtonDown;
            canvas.MouseLeftButtonUp -= Canvas_MouseLeftButtonUp;
            canvas.MouseMove -= Canvas_MouseMove;
        }

        public override Shape New()
        {
            return line;
        }

        public override void Start()
        {
            line = new Line();
            canvas.MouseLeftButtonDown += Canvas_MouseLeftButtonDown;
            canvas.MouseLeftButtonUp += Canvas_MouseLeftButtonUp;
            canvas.MouseMove += Canvas_MouseMove;
        }

        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            p = e.GetPosition(canvas);
            if (moving)
            {
                line.X2 = p.X;
                line.Y2 = p.Y;
            }

        }

        private void Canvas_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void Canvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (first)
            {
                p1 = e.GetPosition(canvas);
                line.X1 = line.X2 = p1.X;
                line.Y1 = line.Y2 = p1.Y;

                first = false;
                moving = true;
                canvas.Children.Add(line);
            }
            else
            {
                first = true;
                End();
                if (p1.X < p.X)
                {
                    myCanvas.SetLeft(line, p1.X);
                    line.X1 = 0;
                    line.X2 -= p1.X;
                }
                else
                {
                    myCanvas.SetLeft(line, p.X);
                    line.X1 -= p.X;
                    line.X2 = 0;
                }

                if (p1.Y < p.Y)
                {
                    myCanvas.SetTop(line, p1.Y);
                    line.Y1 = 0;
                    line.Y2 -= p1.Y;
                }
                else
                {
                    myCanvas.SetTop(line, p.Y);
                    line.Y1 -= p.Y;
                    line.Y2 = 0;
                }
                line.Width = Math.Abs(line.X2 - line.X1);
                line.Height = Math.Abs(line.Y2 - line.Y1);
                line.Stretch = System.Windows.Media.Stretch.Uniform;
                onCompleted();
            }



        }
    }
}

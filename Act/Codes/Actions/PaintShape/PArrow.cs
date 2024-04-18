using System;
using System.Windows;
using System.Windows.Shapes;
using WpfPaint.Codes.Controls;

namespace WpfPaint.Codes.Actions.PaintShape
{
    class PArrow : ShapeAbstract
    {
        private bool first = true;
        private Point p1;
        private bool moving;
        private CustomShapes.Arrow arrow;
        private Point p;

        public override bool IsNormal
        {
            get
            {
                return false;
            }
        }

        public PArrow(myCanvas canvas) : base(canvas)
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

            return arrow;
        }

        public override void Start()
        {
            arrow = new CustomShapes.Arrow();

            arrow.HeadHeight = arrow.StrokeThickness + 8;
            arrow.HeadWidth = arrow.StrokeThickness + 35;
            canvas.MouseLeftButtonDown += Canvas_MouseLeftButtonDown;
            canvas.MouseLeftButtonUp += Canvas_MouseLeftButtonUp;
            canvas.MouseMove += Canvas_MouseMove;
            arrow.Fill = null;

        }

        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            p = e.GetPosition(canvas);
            if (moving)
            {
                arrow.X2 = p.X;
                arrow.Y2 = p.Y;
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
                arrow.X1 = arrow.X2 = p1.X;
                arrow.Y1 = arrow.Y2 = p1.Y;
                arrow.Fill = null;
                first = false;
                moving = true;
                canvas.Children.Add(arrow);
            }
            else
            {
                first = true;
                End();
                if (p1.X < p.X)
                {
                    myCanvas.SetLeft(arrow, p1.X);
                    arrow.X1 = 0;
                    arrow.X2 -= p1.X;
                }
                else
                {
                    myCanvas.SetLeft(arrow, p.X);
                    arrow.X1 -= p.X;
                    arrow.X2 = 0;
                }

                if (p1.Y < p.Y)
                {
                    myCanvas.SetTop(arrow, p1.Y);
                    arrow.Y1 = 0;
                    arrow.Y2 -= p1.Y;
                }
                else
                {
                    myCanvas.SetTop(arrow, p.Y);
                    arrow.Y1 -= p.Y;
                    arrow.Y2 = 0;
                }
                arrow.Width = Math.Abs(arrow.X2 - arrow.X1);
                arrow.Height = Math.Abs(arrow.Y2 - arrow.Y1);
                arrow.Stretch = System.Windows.Media.Stretch.Uniform;
                onCompleted();
            }



        }
    }
}

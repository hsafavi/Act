using System.Windows;
using System.Windows.Shapes;
using WpfPaint.Codes.Controls;

namespace WpfPaint.Codes.Actions.PaintShape
{
    class PFreeForm : ShapeAbstract
    {
        private byte downCount = 0;
        private Point p1;
        //private PolyLineSegment polylineSGM;
        //private Path polylinePath;
        private Point p2;
        Polygon polygon;

        public override bool IsNormal
        {
            get
            {
                return false;
            }
        }

        public PFreeForm(myCanvas canvas) : base(canvas)
        {

        }
        public override void End()
        {
            downCount = 0;
            canvas.MouseLeftButtonDown -= Canvas_MouseLeftButtonDown;
            canvas.MouseMove -= Canvas_MouseMove;
        }

        public override Shape New()
        {
            polygon = new Polygon();
            //polylineSGM = new PolyLineSegment();
            //polylinePath = new Path();
            //polylinePath.StrokeLineJoin = PenLineJoin.Bevel;
            //downCount = 0;
            canvas.MouseLeftButtonDown += Canvas_MouseLeftButtonDown;
            canvas.MouseMove += Canvas_MouseMove;
            //return polylinePath;
            return polygon;
        }

        public override void Start()
        {


        }

        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

            if (downCount > 0)
            {
                // polylineSGM.Points[polylineSGM.Points.Count-1] = e.GetPosition(canvas);
                //p2 = e.GetPosition(canvas);

                polygon.Points[polygon.Points.Count - 1] = e.GetPosition(canvas);
            }


        }



        private void Canvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                End();
                //var r = polylinePath.RenderedGeometry.Bounds;

                //myCanvas.SetLeft(polylinePath, r.X);

                //myCanvas.SetTop(polylinePath, r.Y);

                polygon.Stretch = System.Windows.Media.Stretch.Uniform;

                onCompleted();
            }
            else if (downCount == 0)
            {
                p1 = e.GetPosition(canvas);
                //var myPathFigure = new PathFigure();
                //var myPathSegmentCollection = new PathSegmentCollection();
                //var myPathFigureCollection = new PathFigureCollection();
                //var myPathGeometry = new PathGeometry();
                //myPathFigure.Segments = myPathSegmentCollection;


                //myPathFigureCollection.Add(myPathFigure);
                //myPathSegmentCollection.Add(polylineSGM);

                //myPathGeometry.Figures = myPathFigureCollection;
                //polylinePath.Data = myPathGeometry;
                //myPathFigure.StartPoint = p1;
                //polylineSGM.Points.Add(p1);
                //p2 = new Point(p1.X, p1.Y);
                //polylineSGM.Points.Add(p2);

                //canvas.Children.Add(polylinePath);
                polygon.Points.Add(p1);
                p2 = new Point(p1.X, p1.Y);
                polygon.Points.Add(p2);
                canvas.Children.Add(polygon);
                downCount++;
            }
            else
            {
                //polylineSGM.Points.Remove(p2);

                //polylineSGM.Points.Add(e.GetPosition(canvas));
                //polylineSGM.Points.Add(p2);
                //   polygon.Points.Remove(p2);

                polygon.Points.Add(e.GetPosition(canvas));

                //  polygon.Points.Add(e.GetPosition(canvas));


            }




        }
    }
}

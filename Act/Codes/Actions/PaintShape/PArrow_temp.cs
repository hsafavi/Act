//using System;

//using System.Windows;
//using System.Windows.Media;
//using System.Windows.Shapes;
//using WpfPaint.Codes.Controls;

//namespace WpfPaint.Codes.Actions.PaintShape
//{
//    class PArrow : ShapeAbstract
//    {

//        private bool first = true;
//        private Point p1;
//        private bool moving;
//        private Path arrow;
//        private Point p;

//        private GeometryGroup Group;

//        private RotateTransform transform;
//        private LineGeometry connectorGeometry;

//        PolyLineSegment polylineSGM;
//        private PathFigure myPathFigure;
//        private PathSegmentCollection myPathSegmentCollection;
//        private PathFigureCollection myPathFigureCollection;
//        private PathGeometry myPathGeometry;

//        public override bool IsNormal
//        {
//            get
//            {
//                return false;
//            }
//        }

//        public PArrow(myCanvas canvas) : base(canvas)
//        {

//        }
//        public override void End()
//        {
//            canvas.MouseLeftButtonDown -= Canvas_MouseLeftButtonDown;
//            canvas.MouseLeftButtonUp -= Canvas_MouseLeftButtonUp;
//            canvas.MouseMove -= Canvas_MouseMove;
//        }

//        public override Shape New()
//        {
//            arrow = new Path();

//            // pathGeometry = new PathGeometry();
//            //pathFigure = new PathFigure();
//            polylineSGM = new PolyLineSegment();
//            Group = new GeometryGroup();
//            myPathFigure = new PathFigure();
//            myPathSegmentCollection = new PathSegmentCollection();
//            myPathFigureCollection = new PathFigureCollection();
//            myPathGeometry = new PathGeometry();
//            myPathFigure.Segments = myPathSegmentCollection;
//            //arrow.StrokeLineJoin = PenLineJoin.Miter;
//            //arrow.StrokeMiterLimit = 05 * arrow.StrokeThickness;

//            myPathFigureCollection.Add(myPathFigure);
//            myPathSegmentCollection.Add(polylineSGM);
//            transform = new RotateTransform();


//            connectorGeometry = new LineGeometry();
//            myPathGeometry.Figures = myPathFigureCollection;
//            Group.Children.Add(connectorGeometry);
//            Group.Children.Add(myPathGeometry);

//            arrow.Data = Group;
//            //lineGroup = new GeometryGroup();
//            //seg1 = new LineSegment();


//            //seg2 = new LineSegment();


//            //seg3 = new LineSegment();
//            //seg4 = new LineSegment();

//            //pathFigure.Segments.Add(seg1);
//            //pathFigure.Segments.Add(seg3);
//            //pathFigure.Segments.Add(seg4);
//            //pathGeometry.Figures.Add(pathFigure);
//            //pathFigure.Segments.Add(seg2);
//            //pathFigure.IsClosed = true;
//            //lineGroup.Children.Add(pathGeometry);
//            //lineGroup.Children.Add(connectorGeometry);

//            return arrow;
//        }

//        public override void Start()
//        {

//            canvas.MouseLeftButtonDown += Canvas_MouseLeftButtonDown;
//            canvas.MouseLeftButtonUp += Canvas_MouseLeftButtonUp;
//            canvas.MouseMove += Canvas_MouseMove;

//        }

//        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
//        {
//            p = e.GetPosition(canvas);
//            //p.X += arrow.StrokeThickness;
//            //p.Y += arrow.StrokeThickness;
//            if (moving)
//            {
//                Point lpoint = new Point(p.X + 6, p.Y + 15);
//                Point rpoint = new Point(p.X - 6, p.Y + 15);

//                myPathFigure.StartPoint = p;
//                polylineSGM.Points.Clear();

//                polylineSGM.Points.Add(p);
//                polylineSGM.Points.Add(lpoint);
//                polylineSGM.Points.Add(rpoint);
//                polylineSGM.Points.Add(p);


//                //pathFigure.StartPoint = p;

//                //seg1.Point = lpoint;


//                //seg3.Point = p;

//                //seg2.Point = rpoint;
//                //seg4.Point = p;                
//                double theta = Math.Atan2((p.Y - p1.Y), (p.X - p1.X)) * 180 / Math.PI;
//                transform.Angle = theta + 90;
//                transform.CenterX = p.X;
//                transform.CenterY = p.Y;
//                myPathGeometry.Transform = transform;

//                connectorGeometry.StartPoint = p1;
//                connectorGeometry.EndPoint = p;

//                //  arrow.Data = Group;


//            }

//        }

//        private void Canvas_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
//        {

//        }

//        private void Canvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
//        {
//            if (first)
//            {
//                p1 = e.GetPosition(canvas);
//                canvas.Children.Add(arrow);
//                //pathFigure.StartPoint = p;
//                arrow.Fill = arrow.Stroke;
//                first = false;
//                moving = true;

//            }
//            else
//            {
//                first = true;
//                moving = false;
//                End();

//                arrow.Stretch = System.Windows.Media.Stretch.Uniform;
//                onCompleted();
//            }



//        }
//    }
//}

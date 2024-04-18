//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls.Primitives;
//using System.Windows.Documents;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;
//using adorners;
//using WpfPaint.Codes.Actions.PaintShape;
//using WpfPaint.Codes.Controls;
//using System.Windows.Controls;

//namespace WpfPaint.Codes.Actions
//{

//  public  class ShapeAction:PaintAction
//    {
//     //   public enum ShapeType { Rectangle,Ellipse,Line,Curve,Freeform}
//     //   ShapeType type;
//        PaintShape.ShapeAbstract pshape;
//        private Shape shape;
//        bool pressed;
//        double Xpont, Ypoint;
//        private Point shape_downPoint;
//        private bool isMDownOnMouse;
//        private double left;
//        private double top;
//        private RibbonColorPanel colorPanel;

//        StrokeSettings lineSettings;
//        private Rect rect;
//        ContentControl cc ;
//        public bool Created { get; private set; }

//        public ShapeAction(myCanvas canvas,ShapeAbstract pshape,RibbonColorPanel colorPanel,StrokeSettings lineSettings):base(canvas,false)
//        {
//            this.colorPanel = colorPanel;
//            //  this.type = type;
//            canvas.CurrentAction = this;
//            this.pshape = pshape;
//            this.lineSettings = lineSettings;
//            shape = pshape.New();

//            Start();
//        }
//        public ShapeAction Copy()
//        {
//            if (canvas.Children.Contains(cc))
//                return this;
//            else return null;

//        }
//        public void Paste()
//        {
//            //layer = new myCanvas();
//            //layer.Background = new SolidColorBrush(Colors.Transparent);
//            //layer.Width = canvas.Width;
//            //layer.Height = canvas.Height;
//            //canvas.Children.Add(layer);
//            //canvas.MaxHeight = canvas.ActualHeight;
//            //canvas.MaxWidth = canvas.ActualWidth;
//            ////    canvas.SelectionResized += Canvas_SelectionResized;
//            //if (pshape.IsNormal)
//            //{

//            //    canvas.MouseLeftButtonDown += Canvas_MouseDown;
//            //    canvas.MouseMove += Canvas_MouseMove;
//            //    canvas.MouseLeftButtonUp += Canvas_MouseLeftButtonUp;

//            //}
//            //else
//            //{

//            //    SetStroke();
//            //    SetColor();
//            //    pshape.Completed += Pshape_Completed;


//            //}
//            //Created = true;
//            //pressed = false;
//            //lineSettings.Changed += StrokeSettings_Changed;
//            //colorPanel.ColorChanged += ColorPanel_ColorChanged;
//            //cc.Style = (Style)Application.Current.FindResource("DesignerItemStyle");
//            //if (canvas.Children.Contains(layer))
//            //{
//            //    End();

//            //}
//            Start();
//            Created = true;
//            pressed = false;
//            Selector.SetIsSelected(cc, true);
//            canvas.Children.Add(cc);

//        }
//        private void StrokeSettings_Changed(StrokeSettings sender)
//        {
//            SetStroke();
//        }

//        public override void End()
//        {
//             pshape.End();
//            canvas.MouseLeftButtonDown -= Canvas_MouseDown;
//            canvas.MouseMove -= Canvas_MouseMove;
//            canvas.MouseUp -= Canvas_MouseLeftButtonUp;
//            canvas.MouseLeftButtonUp -= Shape_MouseUp;
//            pshape.Completed -= Pshape_Completed;
//            lineSettings.Changed -= StrokeSettings_Changed;
//            colorPanel.ColorChanged -= ColorPanel_ColorChanged;


//        }

//        protected override void Start()
//        {
//            //switch (type)
//            //{
//            //    case ShapeType.Rectangle:
//            //        pshape = new PRectangle(canvas);
//            //        break;
//            //    case ShapeType.Ellipse:
//            //        pshape = new PEllipse(canvas);
//            //        break;
//            //    case ShapeType.Line:
//            //        pshape = new PLine(canvas);
//            //        break;
//            //    case ShapeType.Curve:
//            //        pshape = new PCurve(canvas);
//            //        break;
//            //    case ShapeType.Freeform:
//            //        pshape = new PFreeForm(canvas);
//            //        break;
//            //}

//            canvas.MaxHeight = canvas.ActualHeight;
//            canvas.MaxWidth = canvas.ActualWidth;
//        //    canvas.SelectionResized += Canvas_SelectionResized;
//            if (pshape.IsNormal)
//            {

//                canvas.MouseLeftButtonDown += Canvas_MouseDown;
//                canvas.MouseMove += Canvas_MouseMove;
//                canvas.MouseLeftButtonUp += Canvas_MouseLeftButtonUp;

//            }
//            else
//            {

//                SetStroke();
//                SetColor();
//                pshape.Completed += Pshape_Completed;


//            }

//            lineSettings.Changed += StrokeSettings_Changed;
//            colorPanel.ColorChanged += ColorPanel_ColorChanged;

//        }

//        private void Pshape_Completed(ShapeAbstract pshape)
//        {
//           // if (Created)
//            {
//                pressed = false;
//                if (shape.ActualWidth <= 1 && shape.ActualHeight <= 1)
//                    canvas.Children.Remove(shape);
//                else
//                {

//                    rect = shape.RenderedGeometry.Bounds;
//                    //left = rect.Left;
//                    //top = rect.Top;
//                    //al = AdornerLayer.GetAdornerLayer(shape);
//                    //ra = new adorners.ResizingAdorner(shape,rect);
//                    //if (al != null)
//                    //{
//                    //    al.Add(ra);
//                    //    MakeMovable();
//                    //}
//                    //Selector.SetIsSelected(shape, true);
//                    cc = new ContentControl();

//                    myCanvas.SetLeft(cc, rect.Left- lineSettings.Weight/2);
//                    myCanvas.SetTop(cc,rect.Top- lineSettings.Weight/2);
//                    cc.MinHeight = 1;
//                    cc.MinWidth = 1;
//                    cc.Width = rect.Width + lineSettings.Weight - 1;
//                    cc.Height = rect.Height + lineSettings.Weight - 1;
//                    canvas.Children.Remove(shape);
//                    shape.Stretch = Stretch.Fill;
//                    shape.Width = shape.Height = double.NaN;
//                    //cc.Width = cc.RenderSize.Width;
//                    //cc.Height = cc.RenderSize.Height;
//                    shape.IsHitTestVisible = false;
//                    cc.Content = shape;
//                    cc.RenderTransform = new RotateTransform(0);
//                    canvas.Children.Add(cc);
//                    //cc.Width = cc.RenderSize.Width;
//                    //cc.Height = cc.RenderSize.Height;
//                    cc.Style = (Style)Application.Current.FindResource("DesignerItemStyle2");
//                    Selector.SetIsSelected(cc, true);
//                    pshape.Start();
//                    shape = pshape.New();

//                    SetStroke();
//                    SetColor();
//                }



//                //       Created = true;
//                canvas.MouseLeftButtonDown += Canvas_AnormalMouseLeftButtonDown;
//            }
//        }


//        private void Canvas_AnormalMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
//        { var p = e.GetPosition(shape);




//            canvas.MouseLeftButtonDown -= Canvas_AnormalMouseLeftButtonDown;


//            //shape = pshape.New();


//        }

//        private void ColorPanel_ColorChanged(RibbonColorPanel sender)
//        {
//            SetColor();
//        }

//        private void SetColor()
//        {
//            shape.Stroke = new SolidColorBrush(colorPanel.MainColor);
//            shape.Fill = new SolidColorBrush(colorPanel.secoundColor);
//        }

//        private void Canvas_SelectionResized(object sender, EventArgs e)
//        {
//            canvas.MaxHeight = canvas.ActualHeight;
//            canvas.MaxWidth = canvas.ActualWidth;
//        }

//        private void Canvas_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
//        {


//        }

//        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
//        {
//            if (pressed)
//            {
//                var p = e.GetPosition(canvas);
//                if (p.X > Xpont)
//                {
//                    shape.Width = p.X - Xpont;
//                }
//                else
//                {
//                    left = p.X;
//                    myCanvas.SetLeft(shape, p.X);
//                    shape.Width = Xpont - p.X;
//                }
//                if (p.Y > Ypoint)
//                {
//                    shape.Height = p.Y - Ypoint;
//                }
//                else
//                {
//                    top = p.Y;
//                    myCanvas.SetTop(shape, p.Y);
//                    shape.Height = Ypoint - p.Y;
//                }
//            }
//        }

//        private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
//        {
//     //       if (!isMDownOnMouse)
//            {
//                if (canvas.SelectedShape != null)
//                {
//                    Selector.SetIsSelected(canvas.SelectedShape, false);
//                    canvas.SelectedShape = null;
//                }
//               if(!pressed)
//                {
//                    shape = pshape.New();

//                    SetStroke();
//                    SetColor();
//                    pressed = true;
//                  //  Created = true;
//                    var p = e.GetPosition(canvas);
//                    Xpont = p.X;
//                    Ypoint = p.Y;
//                    left = Xpont;
//                    top = Ypoint;
//                    myCanvas.SetLeft(shape, Xpont);
//                    myCanvas.SetTop(shape, Ypoint);
//                    canvas.Children.Add(shape);
//                }

//                else
//                {
//                    pressed = false;
//                    if (shape.ActualWidth == 1 && shape.ActualHeight == 1)
//                        canvas.Children.Remove(shape);
//                    else
//                    {
//                        // al = AdornerLayer.GetAdornerLayer(shape);
//                        //ra = new adorners.ResizingAdorner(shape, shape.RenderedGeometry.Bounds);
//                        //if (al != null)
//                        //{
//                        //    al.Add(ra);
//                        //    MakeMovable();
//                        //}

//                        // cc.RenderSize = shape.RenderSize;
//                        cc = new ContentControl();

//                        cc.MouseDoubleClick += Cc_MouseLeftButtonDown;
//                        myCanvas.SetLeft(cc, myCanvas.GetLeft(shape));
//                        myCanvas.SetTop(cc, myCanvas.GetTop(shape));
//                        cc.MinHeight = 1;
//                        cc.MinWidth = 1;
//                        cc.Width = shape.ActualWidth;
//                        cc.Height = shape.ActualHeight;
//                        canvas.Children.Remove(shape);
//                        shape.Stretch = Stretch.Fill;
//                        shape.Width = shape.Height = double.NaN;
//                        shape.IsHitTestVisible = false;
//                        cc.Content = shape;
//                        cc.RenderTransform = new RotateTransform(0);
//                        canvas.Children.Add(cc);
//                        Created = true;

//                        cc.Style = (Style)Application.Current.FindResource("DesignerItemStyle2");


//                        Selector.SetIsSelected(cc, true);
//                        canvas.SelectedShape = cc;
//                    }

//                }
//            }
//        }

//        private void Cc_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
//        {
//           if(canvas.SelectedShape!=null)
//                 Selector.SetIsSelected(canvas.SelectedShape, false);
//            var tmp = (ContentControl)sender;
//            Selector.SetIsSelected(tmp, true);
//            canvas.SelectedShape = tmp;
//        }

//        private void MakeMovable()
//        {
//            shape.MouseLeftButtonDown += Shape_LMouseBDown;
//            shape.MouseMove += Shape_MouseMove;
//            canvas.MouseUp += Shape_MouseUp;
//        }

//        private void Shape_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
//        {
//            isMDownOnMouse = false;
//        }

//        private void Shape_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
//        {
//            var p = e.GetPosition(shape);
//           if(isMDownOnMouse)
//            {
//                left += p.X - shape_downPoint.X;
//                  myCanvas.SetLeft(shape,left);
//                top += p.Y - shape_downPoint.Y;
//                myCanvas.SetTop(shape, top);

//            }
//        }

//        private void Shape_LMouseBDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
//        {
//            shape_downPoint = e.GetPosition(shape);
//            isMDownOnMouse = true;

//        }
//        private void copyToPicture()
//        {

//            Selector.SetIsSelected(cc, false);

//            left = myCanvas.GetLeft(cc);
//            top = myCanvas.GetTop(cc);

//            // var z=canvas.Zoom;
//            // canvas.Zoom = 1;
//            // RenderTargetBitmap rtb2 =
//            //new RenderTargetBitmap((int)width, (int)height, 96d, 96d, PixelFormats.Default);
//            // rtb2.Render(canvas);
//            // var imgs = new ImageBrush(BitmapFrame.Create(rtb2));
//            // imgs.AlignmentX = AlignmentX.Left;

//            // canvas.Picture = imgs;
//            // canvas.Zoom = z;
//            canvas.Picture = SnapShotPNG();
//            canvas.Children.Clear() ;

//           // tmoCv.Children.Add(shape);

//                // RenderTargetBitmap rtb =
//                //new RenderTargetBitmap((int)tmoCv.Width, (int)tmoCv.Height, 96d, 96d, PixelFormats.Default);
//                // rtb.Render(tmoCv);
//                // imgs = new ImageBrush(BitmapFrame.Create(rtb));
//                // canvas.Picture = imgs;
//        }

//        private  ImageBrush SnapShotPNG( )
//        {
//            double zoom = canvas.Zoom;

//                double actualHeight = shape.RenderSize.Height;
//                double actualWidth = shape.RenderSize.Width;

//                double renderHeight = actualHeight * zoom;
//                double renderWidth = actualWidth * zoom;

//                RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)canvas.ActualWidth, (int)canvas.ActualHeight, 96, 96, PixelFormats.Pbgra32);

//                VisualBrush sourceBrush = new VisualBrush(canvas);

//                DrawingVisual drawingVisual = new DrawingVisual();
//                DrawingContext drawingContext = drawingVisual.RenderOpen();

//                using (drawingContext)
//                {

//               //     drawingContext.PushTransform(new ScaleTransform(zoom, zoom));

//                drawingContext.DrawRectangle(canvas.Background,null, new Rect(new Point(0, 0), new Point(canvas.ActualWidth,canvas.ActualHeight)));

//                    drawingContext.DrawRectangle(sourceBrush, null,new Rect(new Point(0,0),new Size(canvas.Width,canvas.Height)));
//                }
//                renderTarget.Render(drawingVisual);
//            return new ImageBrush(BitmapFrame.Create(renderTarget));




//        }

//        private void SetStroke()
//        {
//            if(shape!=null)
//             shape.StrokeThickness = lineSettings.Weight;
//        }

//        public override void Cancel()
//        {

//                canvas.Children.Remove(shape);
//            pressed = false;
//        }
//    }
//}

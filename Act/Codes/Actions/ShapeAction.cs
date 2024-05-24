using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Act.Codes.Actions.PaintShape;
using Act.Codes.Controls;

namespace Act.Codes.Actions
{

    public class ShapeAction : PaintAction
    {
        //   public enum ShapeType { Rectangle,Ellipse,Line,Curve,Freeform}
        //   ShapeType type;
        private readonly PaintShape.ShapeAbstract _paintShape;
        private Shape _shape;
        private bool _pressed;
        private double _xPont, _yPoint;
        private Point _shapeDownPoint;
        private bool _isMDownOnMouse;
        private double _left;
        private double _top;
        private readonly RibbonColorPanel _colorPanel;

        private readonly StrokeSettings _lineSettings;
        private Rect _rect;
        private static bool _startDragging;
        private static Point _startDraggingPoint;
        private static Shape _draggingItem;

        //   ContentControl cc ;
        public bool Created { get; private set; }

        internal override string Description => "در حین ترسیم شکل، برای انصراف کلید Esc را فشار دهید";

        public ShapeAction(myCanvas canvas, ShapeAbstract paintShape, RibbonColorPanel colorPanel, StrokeSettings lineSettings) : base(canvas, false)
        {
            this._colorPanel = colorPanel;
            //  this.type = type;
            canvas.CurrentAction = this;
            this._paintShape = paintShape;
            this._lineSettings = lineSettings;
            if (paintShape != null) _shape = paintShape.New();

            Start();
        }

      
        private void StrokeSettings_Changed(StrokeSettings sender)
        {
            SetStroke();
        }

        public override void End()
        {
            _paintShape.End();
            canvas.MouseLeftButtonDown -= Canvas_MouseDown;
            canvas.MouseMove -= Canvas_MouseMove;
            canvas.MouseUp -= Canvas_MouseLeftButtonUp;
            canvas.MouseLeftButtonUp -= Shape_MouseUp;
            _paintShape.Completed -= PaintShape_Completed;
            _lineSettings.Changed -= StrokeSettings_Changed;
            _colorPanel.ColorChanged -= ColorPanel_ColorChanged;


        }

        protected override void Start()
        {
            canvas.MaxHeight = canvas.ActualHeight;
            canvas.MaxWidth = canvas.ActualWidth;
            //    canvas.SelectionResized += Canvas_SelectionResized;
            if (_paintShape.IsNormal)
            {

                canvas.MouseLeftButtonDown += Canvas_MouseDown;
                canvas.MouseMove += Canvas_MouseMove;
                canvas.MouseLeftButtonUp += Canvas_MouseLeftButtonUp;

            }
            else
            {

                SetStroke();
                SetColor();
                _paintShape.Completed += PaintShape_Completed;


            }

            _lineSettings.Changed += StrokeSettings_Changed;
            _colorPanel.ColorChanged += ColorPanel_ColorChanged;

        }

        private void PaintShape_Completed(ShapeAbstract pshape)
        {
            // if (Created)
            {
                _pressed = false;
                if (_shape.ActualWidth <= 1 && _shape.ActualHeight <= 1)
                    canvas.Children.Remove(_shape);
                else
                {

                    _rect = _shape.RenderedGeometry.Bounds;
                    
                    Rect r = _shape.RenderedGeometry.Bounds;

                    myCanvas.SetLeft(_shape, r.Left - _shape.StrokeThickness / 2);
                    myCanvas.SetTop(_shape, r.Top - _shape.StrokeThickness / 2);
                    _shape.Width = r.Width + _shape.StrokeThickness;
                    _shape.Height = r.Height + _shape.StrokeThickness;
                    AttachEvents();
                    pshape.Start();
                    _shape = pshape.New();

                    SetStroke();
                    SetColor();
                }



                //       Created = true;
                canvas.MouseLeftButtonDown += Canvas_AnormalMouseLeftButtonDown;
            }
        }


        private void Canvas_AnormalMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var p = e.GetPosition(_shape);




            canvas.MouseLeftButtonDown -= Canvas_AnormalMouseLeftButtonDown;


            //shape = pshape.New();


        }

        private void ColorPanel_ColorChanged(RibbonColorPanel sender)
        {
            SetColor();
        }

        private void SetColor()
        {
            _shape.Stroke = new SolidColorBrush(_colorPanel.MainColor);
            _shape.Fill = new SolidColorBrush(_colorPanel.secoundColor);
        }

        private void Canvas_SelectionResized(object sender, EventArgs e)
        {
            canvas.MaxHeight = canvas.ActualHeight;
            canvas.MaxWidth = canvas.ActualWidth;
        }

        private void Canvas_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {


        }

        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_pressed)
            {
                var p = e.GetPosition(canvas);
                if (p.X > _xPont)
                {
                    _shape.Width = p.X - _xPont;
                }
                else
                {
                    _left = p.X;
                    InkCanvas.SetLeft(_shape, p.X);
                    _shape.Width = _xPont - p.X;
                }
                if (p.Y > _yPoint)
                {
                    _shape.Height = p.Y - _yPoint;
                }
                else
                {
                    _top = p.Y;
                    InkCanvas.SetTop(_shape, p.Y);
                    _shape.Height = _yPoint - p.Y;
                }
            }
        }

        private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //       if (!isMDownOnMouse)
            {
                if (canvas.SelectedShape != null)
                {
                    Selector.SetIsSelected(canvas.SelectedShape, false);
                    canvas.SelectedShape = null;
                }
                if (!_pressed)
                {
                    _shape = _paintShape.New();

                    SetStroke();
                    SetColor();
                    _pressed = true;
                    //  Created = true;
                    var p = e.GetPosition(canvas);
                    _xPont = p.X;
                    _yPoint = p.Y;
                    _left = _xPont;
                    _top = _yPoint;
                    InkCanvas.SetLeft(_shape, _xPont);
                    InkCanvas.SetTop(_shape, _yPoint);
                    canvas.Children.Add(_shape);
                }

                else
                {
                    _pressed = false;
                    if (_shape.ActualWidth == 1 && _shape.ActualHeight == 1)
                        canvas.Children.Remove(_shape);
                    else
                    {
                        Created = true;

                        AttachEvents();
                        canvas.SelectedShape = _shape;
                    }

                }
            }
        }



        private void Cc_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (canvas.SelectedShape != null)
                Selector.SetIsSelected(canvas.SelectedShape, false);
            var tmp = (Shape)sender;
            Selector.SetIsSelected(tmp, true);
            canvas.SelectedShape = tmp;
        }

        private void MakeMovable()
        {
            _shape.MouseLeftButtonDown += Shape_LMouseBDown;
            _shape.MouseMove += Shape_MouseMove;
            canvas.MouseUp += Shape_MouseUp;
        }

        private void Shape_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _isMDownOnMouse = false;
        }

        private void Shape_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var p = e.GetPosition(_shape);
            if (_isMDownOnMouse)
            {
                _left += p.X - _shapeDownPoint.X;
                InkCanvas.SetLeft(_shape, _left);
                _top += p.Y - _shapeDownPoint.Y;
                InkCanvas.SetTop(_shape, _top);

            }
        }

        private void Shape_LMouseBDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _shapeDownPoint = e.GetPosition(_shape);
            _isMDownOnMouse = true;

        }
        private void CopyToPicture()
        {
            
            canvas.Picture = SnapShotPng();
            canvas.Children.Clear();
            
        }

        private ImageBrush SnapShotPng()
        {
            double zoom = canvas.Zoom;

            double actualHeight = _shape.RenderSize.Height;
            double actualWidth = _shape.RenderSize.Width;

            double renderHeight = actualHeight * zoom;
            double renderWidth = actualWidth * zoom;

            RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)canvas.ActualWidth, (int)canvas.ActualHeight, 96, 96, PixelFormats.Pbgra32);

            VisualBrush sourceBrush = new VisualBrush(canvas);

            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            using (drawingContext)
            {

                //     drawingContext.PushTransform(new ScaleTransform(zoom, zoom));

                drawingContext.DrawRectangle(canvas.Background, null, new Rect(new Point(0, 0), new Point(canvas.ActualWidth, canvas.ActualHeight)));

                drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0), new Size(canvas.Width, canvas.Height)));
            }
            renderTarget.Render(drawingVisual);
            return new ImageBrush(BitmapFrame.Create(renderTarget));




        }

        private void SetStroke()
        {
            if (_shape != null)
                _shape.StrokeThickness = _lineSettings.Weight;
        }

        public override void Cancel()
        {

            canvas.Children.Remove(_shape);
            _pressed = false;
        }

        private void AttachEvents()
        {

            _shape.MouseLeftButtonDown += Shape_MouseLeftButtonDown;
            canvas.MouseMove += Canvas_MouseMove4ShapeDragOrDelete;
            canvas.MouseLeftButtonUp += Canvas_MouseLeftButtonUp1;
        }

        private void Canvas_MouseLeftButtonUp1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _startDragging = false;
            //if (canvas.CurrentAction != null)
            //    canvas.CurrentAction.MakeNew();

        }

        private void Canvas_MouseMove4ShapeDragOrDelete(object sender, System.Windows.Input.MouseEventArgs e)
        {

            if (_startDragging)
            {

                var p = e.GetPosition(canvas);

                InkCanvas.SetLeft(_draggingItem, InkCanvas.GetLeft(_draggingItem) + p.X - _startDraggingPoint.X);

                InkCanvas.SetTop(_draggingItem, InkCanvas.GetTop(_draggingItem) + p.Y - _startDraggingPoint.Y);
                _startDraggingPoint = p;
            }


        }

        private void Shape_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            if (canvas.CurrentAction is MoveAction)
            {
                _startDragging = true;
                _startDraggingPoint = e.GetPosition(canvas);
                _draggingItem = (Shape)sender;
            }

        }

        public override PaintAction MakeNew()
        {


            return new ShapeAction(canvas, (ShapeAbstract)System.Activator.CreateInstance(_paintShape.GetType()), _colorPanel, _lineSettings);
        }

    }
}

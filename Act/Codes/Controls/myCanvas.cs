using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Act.Codes.Actions;

namespace Act.Codes.Controls
{
    public class myCanvas : InkCanvas
    {
        double zoom = 1;
        private System.Windows.Shapes.Shape selectedShape;

        //  public static readonly DependencyProperty ZoomProperty =
        //DependencyProperty.Register(
        //"Zoom", typeof(double),typeof(myCanvas));
        [System.ComponentModel.DefaultValue(1)]
        public double Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                LayoutTransform = new ScaleTransform(zoom, zoom);
            }
        }
        PaintAction currentAction;
        public PaintAction CurrentAction
        {
            get { return currentAction; }
            set { if (currentAction != null) currentAction.End(); var p = currentAction; currentAction = value; if (ActionChanged != null) ActionChanged(this, currentAction); }
        }
        internal delegate void ActionChangedEH(myCanvas canvas, PaintAction previousAction);
        internal event ActionChangedEH ActionChanged;
        public myCanvas()
        {
            //var d = DefaultDrawingAttributes;
            //d.StylusTipTransform = new Matrix(0.5, 0, 0, 0.5, 0, 0);
            //d.Width = 2;
            //d.Height = 2;
            //d.IgnorePressure = true;
            ClipToBounds = true;
            EraserShape = new EllipseStylusShape(30, 30);


        }
        public ImageBrush Picture
        {
            get
            {
                if (Background is ImageBrush) return (ImageBrush)Background;
                RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)ActualWidth, (int)ActualHeight, 96, 96, PixelFormats.Pbgra32);
                VisualBrush sourceBrush = new VisualBrush(this);

                DrawingVisual drawingVisual = new DrawingVisual();
                DrawingContext drawingContext = drawingVisual.RenderOpen();

                using (drawingContext)
                {
                    drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0), new Size(ActualWidth, ActualHeight)));
                }
                renderTarget.Render(drawingVisual);
                var img = new ImageBrush(BitmapFrame.Create(renderTarget));
                Background = img;
                return img;

            }
            set { value.AlignmentY = AlignmentY.Top; value.AlignmentX = AlignmentX.Left; value.Stretch = Stretch.Uniform; Background = value; }
        }

        public Shape SelectedShape { get => selectedShape; set => selectedShape = value; }

        public void PrintOnPicture(ImageBrush imgb, Point location)
        {

        }

    }
}

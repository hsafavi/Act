using System.Windows.Shapes;
using Act.Codes.Controls;

namespace Act.Codes.Actions
{
    public class MoveAction : PaintAction
    {
        internal override string Description
        {
            get
            {
                return "شکل ها را جابجا کنید";
            }
        }

        public MoveAction(myCanvas canvas) : base(canvas, true)
        {

            canvas.CurrentAction = this;
            Start();

        }

        private void ColorPanel_ColorChanged(RibbonColorPanel sender)
        {

            //d.Color = colorPanel.MainColor;
        }

        public override void Cancel()
        {

        }

        public override void End()
        {
            foreach (var c in Canvas.Children)
            {
                var s = c as Shape;
                if (s != null)
                    s.Cursor = Canvas.Cursor;
            }

            //colorPanel.ColorChanged -= ColorPanel_ColorChanged;

        }

        protected override void Start()
        {
            //var d = canvas.DefaultDrawingAttributes;
            //d.Color = colorPanel.MainColor;

            foreach (var c in Canvas.Children)
            {
                var s = c as Shape;
                if (s != null)
                    s.Cursor = System.Windows.Input.Cursors.SizeAll;
            }



        }



        private void Canvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void Canvas_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //    copyToPicture();

        }

        public override PaintAction MakeNew()
        {
            return new MoveAction(Canvas);
        }
        //private void copyToPicture()
        //{
        //    canvas.Picture = SnapShotPNG();

        //    layer.Strokes.Clear();

        //}
        //private ImageBrush SnapShotPNG()
        //{
        //    double zoom = canvas.Zoom;

        //    double actualHeight = layer.RenderSize.Height;
        //    double actualWidth = layer.RenderSize.Width;

        //    double renderHeight = actualHeight * zoom;
        //    double renderWidth = actualWidth * zoom;

        //    RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)canvas.ActualWidth, (int)canvas.ActualHeight, 96, 96, PixelFormats.Pbgra32);
        //    VisualBrush sourceBrush = new VisualBrush(layer);

        //    DrawingVisual drawingVisual = new DrawingVisual();
        //    DrawingContext drawingContext = drawingVisual.RenderOpen();

        //    using (drawingContext)
        //    {

        //        //     drawingContext.PushTransform(new ScaleTransform(zoom, zoom));

        //        drawingContext.DrawRectangle(canvas.Background, null, new Rect(new Point(0, 0), new Point(canvas.ActualWidth, canvas.ActualHeight)));

        //        drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0), new Size(actualWidth, actualHeight)));
        //    }
        //    renderTarget.Render(drawingVisual);
        //    return new ImageBrush(BitmapFrame.Create(renderTarget));




        //}

    }
}

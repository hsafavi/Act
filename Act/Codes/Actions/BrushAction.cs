using Act.Codes.Controls;

namespace Act.Codes.Actions
{
    public class BrushAction : PaintAction
    {
        StrokeSettings strokeSettings;

        RibbonColorPanel colorPanel;

        internal override string Description
        {
            get
            {
                return "رنگ و اندازه قلم قابل تغییر است";
            }

        }

        public BrushAction(myCanvas canvas, RibbonColorPanel colorpanel, StrokeSettings strokeSettings) : base(canvas, true)
        {
            colorPanel = colorpanel;
            canvas.CurrentAction = this;
            this.strokeSettings = strokeSettings;
            Start();


        }

        private void ColorPanel_ColorChanged(RibbonColorPanel sender)
        {
            var d = Canvas.DefaultDrawingAttributes;
            d.Color = colorPanel.MainColor;

        }

        public override void Cancel()
        {

        }

        public override void End()
        {
            Canvas.EditingMode = System.Windows.Controls.InkCanvasEditingMode.None;
            colorPanel.ColorChanged -= ColorPanel_ColorChanged;
            strokeSettings.Changed -= StrokeSettings_Changed;
            Canvas.UseCustomCursor = false;
        }

        protected override void Start()
        {
            colorPanel.ColorChanged += ColorPanel_ColorChanged;
            strokeSettings.Changed += StrokeSettings_Changed;
            Canvas.UseCustomCursor = true;
            if (strokeSettings.Weight < 1)
                strokeSettings.Weight = 1;
            var d = Canvas.DefaultDrawingAttributes;

            d.Color = colorPanel.MainColor;
            d.Width =  strokeSettings.Weight;
            d.Height = d.Width;
            d.FitToCurve=true;
            d.StylusTipTransform = new System.Windows.Media.Matrix(1, 0, 0, 1.5, 0, 0);
            Canvas.EditingMode = System.Windows.Controls.InkCanvasEditingMode.Ink;



        }

        private void StrokeSettings_Changed(StrokeSettings sender)
        {
            var d = Canvas.DefaultDrawingAttributes;
            if (strokeSettings.Weight < 1)
                strokeSettings.Weight = 1;
            d.Width = d.Height = strokeSettings.Weight;

        }

        private void Canvas_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //    copyToPicture();

        }

        public override PaintAction MakeNew()
        {
            return new BrushAction(Canvas, colorPanel, strokeSettings);
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

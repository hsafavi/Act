using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Act.Codes.Controls;

namespace Act.Codes.Actions
{
    public class EraserAction : PaintAction
    {

        private Cursor cursor;

        internal override string Description
        {
            get
            {
                return "در حالت عادی دستنویس را پاک می کند. برای حذف شکل و متن کلید ctrl را نگه دارید";
            }
        }

        public EraserAction(myCanvas canvas) : base(canvas, true)
        {

            canvas.CurrentAction = this;

            Start();


        }

        private void ColorPanel_ColorChanged(RibbonColorPanel sender)
        {
            var d = canvas.DefaultDrawingAttributes;
            //d.Color = colorPanel.MainColor;


        }

        public override void Cancel()
        {

        }

        public override void End()
        {
            canvas.EditingMode = System.Windows.Controls.InkCanvasEditingMode.None;
            //colorPanel.ColorChanged -= ColorPanel_ColorChanged;
            //     canvas.MouseMove -= Canvas_MouseMove;
            foreach (UIElement e in canvas.Children)
                e.MouseMove -= E_MouseMove;
            Keyboard.RemovePreviewKeyDownHandler(canvas.Parent, Canvas_KeyDown);
            Keyboard.RemovePreviewKeyUpHandler(canvas.Parent, Canvas_KeyUp);
            canvas.Cursor = cursor;
        }

        private void E_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                canvas.Children.Remove((UIElement)sender);
        }

        protected override void Start()
        {
            //var d = canvas.DefaultDrawingAttributes;
            //d.Color = colorPanel.MainColor;
            //inkPresenter = GetVisualChild<InkPresenter>(canvas);


            Keyboard.AddPreviewKeyDownHandler(canvas.Parent, Canvas_KeyDown);
            Keyboard.AddPreviewKeyUpHandler(canvas.Parent, Canvas_KeyUp);
            canvas.EditingMode = System.Windows.Controls.InkCanvasEditingMode.EraseByPoint;
            cursor = canvas.Cursor;
            Uri uri = new Uri("/images/eraser.cur", UriKind.Relative);
            canvas.Cursor = new Cursor(Act.App.GetResourceStream(uri).Stream);
            foreach (UIElement e in canvas.Children)
                e.MouseMove += E_MouseMove;

        }

        private void Canvas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                canvas.EditingMode = System.Windows.Controls.InkCanvasEditingMode.EraseByPoint;
                canvas.UseCustomCursor = false;
            }
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                canvas.EditingMode = System.Windows.Controls.InkCanvasEditingMode.None;
                canvas.UseCustomCursor = true;
            }

        }

        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                VisualTreeHelper.HitTest(canvas, null, new HitTestResultCallback(myHTresult), new PointHitTestParameters(e.GetPosition(canvas)));


                //var o = VisualTreeHelper.HitTest(canvas, e.GetPosition(canvas));

                //if (o != null)
                //{
                //    var cc = o.VisualHit as AdornedElementPlaceholder;
                //    if (cc != null && cc.Child is Shape)
                //        canvas.Children.Remove(cc);
                //}


            }
        }

        private HitTestResultBehavior myHTresult(HitTestResult result)
        {
            var s = result.VisualHit as Shape;
            if (s != null)
            {

                canvas.Children.Remove(s);
            }


            return HitTestResultBehavior.Continue;
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
            return new EraserAction(canvas);
        }
        public T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                    break;
            }
            return child;
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

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using Act.Codes.Controls;

namespace Act.Codes.Actions
{
    class TextAction : PaintAction
    {
        ContentControl cc;
        Rectangle blackBoundary;
        //Canvas layer;
        RichTextBox rtb;
        private bool pressed;
        private bool Created;
        private double Xpont;
        private double Ypoint;

        RibbonColorPanel colorPanel;
        FontSettings fontSettings;
        private RichTextBox focusedRtb;
        private Rectangle whiteBoundary;

        internal override string Description
        {
            get
            {
                return "کادر متن را بکشید و سپس متن را اضافه کنید\n با کلیک بیرون از کادر متن می توانید دوباره اقدام کنید";
            }
        }

        public TextAction(myCanvas canvas, FontSettings fontSettings, RibbonColorPanel colorPanel) : base(canvas, true)
        {
            if (fontSettings.Visibility != Visibility.Visible)
            {
                this.colorPanel = colorPanel;
                this.fontSettings = fontSettings;
                fontSettings.Visibility = Visibility.Visible;
                Start();
            }
        }
        public override void Cancel()
        {

        }

        public override void End()
        {
            //cc.Style = null;
            Selector.SetIsSelected(cc, false);
            //cc.Height = Double.NaN;
            fontSettings.Visibility = Visibility.Hidden;
            //cc.Content = null;

            //canvas.Children.Add(rtb);
            //canvas.Children.Remove(cc);
            //myCanvas.SetLeft(rtb, myCanvas.GetLeft(cc) + 5);

            //myCanvas.SetTop(rtb, myCanvas.GetTop(cc) + 5);

            //rtb.Width = cc.Width-10;
            if (string.IsNullOrEmpty(new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd).Text))
            {
                Canvas.Children.Remove(cc);
            }

            fontSettings.FontChanged -= FontSettings_FontChanged;
            rtb.Selection.Changed -= Selection_Changed;
            Canvas.MouseLeftButtonDown -= Canvas_MouseLeftButtonDown;
            colorPanel.ColorChanged -= ColorPanel_ColorChanged;
            Canvas.MouseMove -= Layer_MouseMove;
        }

        protected override void Start()
        {
            Canvas.CurrentAction = this;
            init();

            fontSettings.FontChanged += FontSettings_FontChanged;

            Canvas.MouseLeftButtonDown += Canvas_MouseLeftButtonDown;
            colorPanel.ColorChanged += ColorPanel_ColorChanged;
            Canvas.MouseMove += Layer_MouseMove;
        }

        private void init()
        {
            cc = new ContentControl();
            //layer = new Canvas();
            rtb = new RichTextBox();

            rtb.Margin = new Thickness(5);
            cc.Content = rtb;

            rtb.BorderBrush = null;
            rtb.Background = new SolidColorBrush(colorPanel.secoundColor);
            rtb.BorderThickness = new Thickness(0);
            //layer.Background = new SolidColorBrush(Colors.Transparent);
            cc.Style = (Style)Application.Current.FindResource("MsrDesignerItemStyle");
            //layer.HorizontalAlignment = HorizontalAlignment.Stretch;
            //layer.VerticalAlignment = VerticalAlignment.Stretch;
            //layer.Width = canvas.ActualWidth;
            //layer.Height = canvas.ActualHeight;
            //canvas.Children.Add(layer);


            rtb.Selection.Changed += Selection_Changed;
            rtb.GotFocus += Rtb_GotFocus;
            rtb.LostFocus += Rtb_LostFocus;
            focusedRtb = rtb;

        }

        private void Rtb_LostFocus(object sender, RoutedEventArgs e)
        {
            var c = (ContentControl)((RichTextBox)sender).Parent;
            Selector.SetIsSelected(c, false);
            //c.Style = null;
        }

        private void Rtb_GotFocus(object sender, RoutedEventArgs e)
        {

            focusedRtb = (RichTextBox)sender;
            var c = (ContentControl)focusedRtb.Parent;
           
            Selector.SetIsSelected(c, true);
            c.Style = (Style)Application.Current.FindResource("MsrDesignerItemStyle");

        }

        private void ColorPanel_ColorChanged(RibbonColorPanel sender)
        {

            focusedRtb.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(colorPanel.MainColor));
            rtb.Background = new SolidColorBrush(colorPanel.secoundColor);
            //focusedRtb.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.Transparent/*colorPanel.secoundColor*/));
        }

        private void Selection_Changed(object sender, EventArgs e)
        {

            var rt = focusedRtb;
            if (rt == null)
                rt = rtb;
            fontSettings.FontChanged -= FontSettings_FontChanged;
            fontSettings.Set(rt.Selection);
            fontSettings.Set4Paragraph(rt.CaretPosition.Paragraph);
            fontSettings.FontChanged += FontSettings_FontChanged;

        }

        private void FontSettings_FontChanged(FontSettings settingsPanel)
        {
            var rt = focusedRtb;

            if (rt != null)
            {
                var t = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd).Text;
                //}
                //catch(Exception x) { MessageBox.Show(x.ToString()); }
                if (string.IsNullOrWhiteSpace(t.Replace("\r\n", "")))
                {
                    
                    fontSettings.SetInputProperties(new TextRange( rt.Document.ContentStart,rt.Document.ContentEnd), rt.CaretPosition.Paragraph);
                    
                }
                else
                    fontSettings.SetInputProperties(rt.Selection, rt.CaretPosition.Paragraph);

                rt.Focus();

            }
            
        }

        private void Layer_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (pressed)
            {
                var p = e.GetPosition(Canvas);
                if (p.X > Xpont)
                {
                    whiteBoundary.Width = blackBoundary.Width = p.X - Xpont;
                }
                else
                {
                    //      left = p.X;
                    myCanvas.SetLeft(blackBoundary, p.X);
                    myCanvas.SetLeft(whiteBoundary, p.X);

                    whiteBoundary.Width = blackBoundary.Width = Xpont - p.X;

                }
                if (p.Y > Ypoint)
                {
                    whiteBoundary.Height = blackBoundary.Height = p.Y - Ypoint;
                }
                else
                {
                    //    top = p.Y;
                    myCanvas.SetTop(blackBoundary, p.Y);
                    myCanvas.SetTop(whiteBoundary, p.Y);

                    whiteBoundary.Height = blackBoundary.Height = Ypoint - p.Y;
                }
            }
        }


        private void Canvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Created)
            {
                Selector.SetIsSelected(cc, false);
                //cc.Style = null;
                //cc.Height = Double.NaN;
                //try
                //{ 
                var t = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd).Text;
                //}
                //catch(Exception x) { MessageBox.Show(x.ToString()); }
                if (string.IsNullOrEmpty(t.Replace("\r\n", "")))
                {
                    Canvas.Children.Remove(cc);
                }
                init();
                Created = false;

            }
            else if (!pressed)
            {

                blackBoundary = new Rectangle();
                whiteBoundary = new Rectangle();
                blackBoundary.StrokeThickness = whiteBoundary.StrokeThickness = 1;
                blackBoundary.Stroke = new SolidColorBrush(Colors.Black);
                whiteBoundary.Stroke = new SolidColorBrush(Colors.White);
                blackBoundary.StrokeDashArray = new DoubleCollection() { 5, 5 };
                whiteBoundary.StrokeDashArray = new DoubleCollection() { 0, 5, 0 };
                rtb.Document.Blocks.Clear();
                fontSettings.SetInputProperties(rtb.Selection, rtb.CaretPosition.Paragraph);
                pressed = true;
                //Created = true;
                var p = e.GetPosition(Canvas);
                Xpont = p.X;
                Ypoint = p.Y;
                //left = Xpont;
                //top = Ypoint;
                myCanvas.SetLeft(blackBoundary, Xpont);
                myCanvas.SetTop(blackBoundary, Ypoint);
                Canvas.Children.Add(blackBoundary);
                myCanvas.SetLeft(whiteBoundary, Xpont);
                myCanvas.SetTop(whiteBoundary, Ypoint);
                Canvas.Children.Add(whiteBoundary);
            }
            else
            {
                pressed = false;
                if (blackBoundary.ActualWidth == 1 && blackBoundary.ActualHeight == 1)
                {
                    Canvas.Children.Remove(blackBoundary);
                    Canvas.Children.Remove(whiteBoundary);
                }
                else
                {
                    // al = AdornerLayer.GetAdornerLayer(shape);
                    //ra = new adorners.ResizingAdorner(shape, shape.RenderedGeometry.Bounds);
                    //if (al != null)
                    //{
                    //    al.Add(ra);
                    //    MakeMovable();
                    //}

                    // cc.RenderSize = shape.RenderSize;

                    myCanvas.SetLeft(cc, myCanvas.GetLeft(blackBoundary));
                    myCanvas.SetTop(cc, myCanvas.GetTop(blackBoundary));

                    cc.MinHeight = 1;
                    cc.MinWidth = 1;

                    cc.Width = blackBoundary.ActualWidth;
                    cc.Height = blackBoundary.ActualHeight;

                    Canvas.Children.Remove(blackBoundary);
                    Canvas.Children.Remove(whiteBoundary);


                    //baundary.Stretch = Stretch.Fill;
                    //baundary.Width = baundary.Height = double.NaN;
                    //baundary.IsHitTestVisible = false;

                    cc.RenderTransform = new RotateTransform(0);
                    Canvas.Children.Add(cc);

                    Created = true;


                    Selector.SetIsSelected(cc, true);
                    rtb.Focus();
                    rtb.FontSize = fontSettings.Font_Size;
                    rtb.FontFamily = fontSettings.Font_Family;
                    rtb.FontWeight = fontSettings.IsBold ? FontWeights.Bold : FontWeights.Normal;
                    rtb.FontStyle = fontSettings.IsItalic ? FontStyles.Italic : FontStyles.Normal;
                    var p = new Paragraph();
                    p.FlowDirection = FlowDirection.RightToLeft;
                    rtb.Document.Blocks.Add(p);
                    rtb.SelectAll();
                    rtb.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(colorPanel.MainColor));
                    //rtb.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(colorPanel.secoundColor));
                    
                }

            }
        }

        public override PaintAction MakeNew()
        {
            return new TextAction(Canvas, fontSettings, colorPanel);
        }
    }
}

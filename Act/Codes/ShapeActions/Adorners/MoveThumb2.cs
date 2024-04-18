using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfPaint.Codes.Controls;

namespace DiagramDesigner2
{
    public class MoveThumb2 : Thumb
    {
        public MoveThumb2()
        {
            DragDelta += new DragDeltaEventHandler(this.MoveThumb_DragDelta);
        }

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Shape designerItem = DataContext as Shape;

            if (designerItem != null)
            {
                Point dragDelta = new Point(e.HorizontalChange, e.VerticalChange);

                RotateTransform rotateTransform = designerItem.RenderTransform as RotateTransform;
                if (rotateTransform != null)
                {
                    dragDelta = rotateTransform.Transform(dragDelta);

                }
                designerItem.Margin = new Thickness(designerItem.Margin.Left + dragDelta.X, designerItem.Margin.Top + dragDelta.Y, designerItem.Margin.Right - dragDelta.X, designerItem.Margin.Bottom - dragDelta.Y);
                myCanvas.SetLeft(designerItem, myCanvas.GetLeft(designerItem) + dragDelta.X);
                myCanvas.SetTop(designerItem, myCanvas.GetTop(designerItem) + dragDelta.Y);
                //System.Diagnostics.Debug.WriteLine(myCanvas.GetTop(designerItem));
            }
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Act.Codes.ShapeActions.Adorners
{
    public class MoveThumb : Thumb
    {
        public MoveThumb()
        {
            DragDelta += new DragDeltaEventHandler(this.MoveThumb_DragDelta);
        }

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Control designerItem = DataContext as Control;

            if (designerItem != null)
            {
                Point dragDelta = new Point(e.HorizontalChange, e.VerticalChange);

                RotateTransform rotateTransform = designerItem.RenderTransform as RotateTransform;
                if (rotateTransform != null)
                {
                    dragDelta = rotateTransform.Transform(dragDelta);

                }
                designerItem.Margin = new Thickness(designerItem.Margin.Left + dragDelta.X, designerItem.Margin.Top + dragDelta.Y, designerItem.Margin.Right - dragDelta.X, designerItem.Margin.Bottom - dragDelta.Y);
                //Canvas.SetLeft(designerItem, Canvas.GetLeft(designerItem) + dragDelta.X);
                //Canvas.SetTop(designerItem, Canvas.GetTop(designerItem) + dragDelta.Y);
                //System.Diagnostics.Debug.WriteLine(myCanvas.GetTop(designerItem));
            }
        }
    }
}

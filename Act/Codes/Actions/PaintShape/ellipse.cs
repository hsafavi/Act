using System.Windows.Shapes;
using WpfPaint.Codes.Controls;

namespace WpfPaint.Codes.Actions.PaintShape
{
    class PEllipse : ShapeAbstract
    {
        public PEllipse(myCanvas canvas) : base(canvas)
        {

        }

        public override bool IsNormal
        {
            get
            {
                return true;
            }
        }

        public override void End()
        {

        }

        public override Shape New()
        {
            return new Ellipse();
        }

        public override void Start()
        {
        }
    }
}

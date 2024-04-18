using System.Windows.Shapes;
using WpfPaint.Codes.Controls;

namespace WpfPaint.Codes.Actions.PaintShape
{
    class PRoundedRectangle : ShapeAbstract
    {
        public override bool IsNormal
        {
            get
            {
                return true;
            }
        }

        public PRoundedRectangle(myCanvas canvas) : base(canvas)
        { }
        public override void End()
        {

        }

        public override void Start()
        {


        }
        public override Shape New()
        {
            var r = new Rectangle();
            r.RadiusX = 10;
            r.RadiusY = 10;
            return r;
        }


    }
}

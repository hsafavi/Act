using System.Windows.Shapes;
using WpfPaint.Codes.Controls;

namespace WpfPaint.Codes.Actions.PaintShape
{
    class PRectangle : ShapeAbstract
    {
        public override bool IsNormal
        {
            get
            {
                return true;
            }
        }

        public PRectangle(myCanvas canvas) : base(canvas)
        { }
        public override void End()
        {

        }

        public override void Start()
        {


        }
        public override Shape New()
        {
            return new Rectangle();
        }


    }
}

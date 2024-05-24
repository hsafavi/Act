using System.Windows.Shapes;
using Act.Codes.Controls;

namespace Act.Codes.Actions.PaintShape
{
    class PRectangle : ShapeBase
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

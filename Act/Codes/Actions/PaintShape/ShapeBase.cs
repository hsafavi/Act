using System.Windows.Shapes;
using Act.Codes.Controls;

namespace Act.Codes.Actions.PaintShape
{
    public abstract class ShapeBase
    {
        protected myCanvas Canvas { get; }

        public delegate void CompletedEH(ShapeBase pshape);
        public event CompletedEH Completed;

        public abstract void Start();
        public  abstract void End();
        public abstract Shape New();
        public abstract bool IsNormal { get; }
        protected void onCompleted()
        {
            if (Completed != null)
                Completed(this);
        }
        public ShapeBase(myCanvas canvas)
        {
            this.Canvas = canvas;
            Start();

        }

    }
}

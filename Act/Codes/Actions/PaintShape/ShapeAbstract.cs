using System.Windows.Shapes;
using Act.Codes.Controls;

namespace Act.Codes.Actions.PaintShape
{
    public abstract class ShapeAbstract
    {
        protected myCanvas canvas;
        public delegate void CompletedEH(ShapeAbstract pshape);
        public event CompletedEH Completed;

        public abstract void Start();
        public abstract void End();
        public abstract Shape New();
        public abstract bool IsNormal { get; }
        protected void onCompleted()
        {
            if (Completed != null)
                Completed(this);
        }
        public ShapeAbstract(myCanvas canvas)
        {
            this.canvas = canvas;
            Start();

        }

    }
}

using WpfPaint.Codes.Controls;

namespace WpfPaint.Codes.Actions
{
    public abstract class PaintAction
    {
        protected myCanvas canvas;
        protected abstract void Start();
        public abstract void End();
        public abstract void Cancel();
        public bool Active { protected set; get; }
        public abstract PaintAction MakeNew();
        internal abstract string Description { get; }
        internal PaintAction(myCanvas canvas, bool ClearSelection)
        {
            this.canvas = canvas;

        }

    }
}

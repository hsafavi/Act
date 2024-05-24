using Act.Codes.Controls;

namespace Act.Codes.Actions
{
    public abstract class PaintAction
    {
        protected myCanvas Canvas { get; }
        protected abstract void Start();
        public abstract void End();
        public abstract void Cancel();
        public bool Active { protected set; get; }
        public abstract PaintAction MakeNew();
        internal abstract string Description { get; }
        internal PaintAction(myCanvas canvas, bool clearSelection)
        {
            this.Canvas = canvas;

        }

    }
}

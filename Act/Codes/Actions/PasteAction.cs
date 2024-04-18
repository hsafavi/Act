using WpfPaint.Codes.Controls;

namespace WpfPaint.Codes.Actions
{
    public class PasteAction : PaintAction
    {
        public static object PainObject { set; private get; }

        internal override string Description
        {
            get
            {
                return "";
            }
        }

        PaintAction action;

        public PasteAction(myCanvas canvas) : base(canvas, true)
        {
            Start();

        }
        public override void Cancel()
        {

        }

        public override void End()
        {
            var sa = PainObject as ShapeAction;
            if (sa != null)
            {
                sa.End();
            }
        }

        protected override void Start()
        {
            action = canvas.CurrentAction;
            if (action != null)
                action.End();
            var sa = PainObject as ShapeAction;
            if (sa != null)
            {
                sa.Paste();
            }

        }

        public override PaintAction MakeNew()
        {
            return null;
        }
    }
}

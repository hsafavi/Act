﻿using System.Windows.Shapes;
using Act.Codes.Controls;

namespace Act.Codes.Actions.PaintShape
{
    class PEllipse : ShapeBase
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

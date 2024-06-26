﻿using System.Windows.Input;

namespace Act.Codes.Commands
{
    public static class MyCommands
    {
        public static readonly RoutedCommand CancelShape = new RoutedCommand(
           "CancelShape", typeof(MyCommands), new InputGestureCollection() { new KeyGesture(Key.Escape) }
      );
        public static readonly RoutedCommand Minimize = new RoutedCommand(
          "Minimize", typeof(MyCommands), new InputGestureCollection() { new KeyGesture(Key.M, ModifierKeys.Control) }
     );
        public static readonly RoutedCommand Delete = new RoutedCommand(
        "Delete", typeof(MyCommands), new InputGestureCollection() { new KeyGesture(Key.D, ModifierKeys.Control) }
   );
        public static readonly RoutedCommand Brush = new RoutedCommand(
            "Brush", typeof(MyCommands), new InputGestureCollection() { new KeyGesture(Key.B, ModifierKeys.Control) }
       );
        public static readonly RoutedCommand Rectangle = new RoutedCommand(
           "Rectangle", typeof(MyCommands), new InputGestureCollection() { new KeyGesture(Key.R, ModifierKeys.Control) }
      );
        public static readonly RoutedCommand Ellipse = new RoutedCommand(
          "ellipse", typeof(MyCommands), new InputGestureCollection() { new KeyGesture(Key.E, ModifierKeys.Control) }
     );
        public static readonly RoutedCommand Line = new RoutedCommand(
          "Line", typeof(MyCommands), new InputGestureCollection() { new KeyGesture(Key.L, ModifierKeys.Control) }
     );

        public static readonly RoutedCommand FreeForm = new RoutedCommand(
        "FreeForm", typeof(MyCommands), new InputGestureCollection() { new KeyGesture(Key.F, ModifierKeys.Control) }
   );
        public static readonly RoutedCommand Arrow = new RoutedCommand(
       "Arrow", typeof(MyCommands), new InputGestureCollection() { new KeyGesture(Key.A, ModifierKeys.Control) }
  );
        public static readonly RoutedCommand HideControlPanel = new RoutedCommand(
       "HideControlPanel", typeof(MyCommands), new InputGestureCollection() { new KeyGesture(Key.H, ModifierKeys.Control) }
  );
        public static readonly RoutedCommand Move = new RoutedCommand(
       "Move", typeof(MyCommands), new InputGestureCollection() { new KeyGesture(Key.N, ModifierKeys.Control) }
  );
        public static readonly RoutedCommand ScreenShot = new RoutedCommand(
      "ScreenShot", typeof(MyCommands), new InputGestureCollection() { new KeyGesture(Key.P, ModifierKeys.Control) }
 );
        public static readonly RoutedCommand Transparent = new RoutedCommand(
    "Transparent", typeof(MyCommands), new InputGestureCollection() { new KeyGesture(Key.T, ModifierKeys.Control | ModifierKeys.Alt) }
);
        public static readonly RoutedCommand ClearAll = new RoutedCommand(
    "ClearAll", typeof(MyCommands), new InputGestureCollection() { new KeyGesture(Key.C, ModifierKeys.Control|ModifierKeys.Alt) }
);
        public static readonly RoutedCommand Text = new RoutedCommand(
   "Text", typeof(MyCommands), new InputGestureCollection() { new KeyGesture(Key.T, ModifierKeys.Control) }
);
        public static readonly RoutedCommand Record = new RoutedCommand(
   "Record", typeof(MyCommands), new InputGestureCollection() { new KeyGesture(Key.C, ModifierKeys.Control) }
);
    }

}


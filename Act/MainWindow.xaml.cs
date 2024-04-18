using Act;
using dastyar.Codes.Actions;
using Microsoft.Win32;
using NHotkey;
using ScreenRecorderLib;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WpfPaint.Codes.Actions;
using WpfPaint.Codes.Actions.PaintShape;
using WpfPaint.Codes.Controls;

namespace dastyar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum backType { Static, Trans, Solid }
        backType type = backType.Trans;
        private bool cpVisisbie = true;
        myCanvas canvas, whiteCanvas1, blackCanvas1, blackCanvas2, whiteCanvas2;
         TextBlock resumeContent = new TextBlock() { Text = "\u25B8",FontSize=28, VerticalAlignment = VerticalAlignment.Bottom, Margin = new Thickness() { Top = 1, Bottom = 1 } };
         TextBlock pauseContent = new TextBlock() { Text = "\u2016",FontSize=18,FontWeight=FontWeights.Bold ,VerticalAlignment = VerticalAlignment.Bottom, Margin = new Thickness() { Top = 5, Bottom = 5 } };
        //private ScreenCaptureJob recordJob;
        public MainWindow()
        {
            InitializeComponent();

            cp.PauseBtn.Click += PauseBtn_Click;
            canvas = mainCanvas;
            whiteCanvas1 = new myCanvas();
            whiteCanvas2 = new myCanvas();
            blackCanvas1 = new myCanvas();
            blackCanvas2 = new myCanvas();
            whiteCanvas1.HorizontalAlignment = whiteCanvas2.HorizontalAlignment = blackCanvas1.HorizontalAlignment = blackCanvas2.HorizontalAlignment = HorizontalAlignment.Stretch;
            whiteCanvas1.VerticalAlignment = whiteCanvas2.VerticalAlignment = blackCanvas1.VerticalAlignment = blackCanvas2.VerticalAlignment = VerticalAlignment.Stretch;
            whiteCanvas1.EditingMode = blackCanvas1.EditingMode = blackCanvas2.EditingMode = whiteCanvas2.EditingMode = canvas.EditingMode;
            whiteCanvas1.Cursor = blackCanvas1.Cursor = blackCanvas2.Cursor = whiteCanvas2.Cursor = canvas.Cursor;
            whiteCanvas1.Background = whiteCanvas2.Background = new SolidColorBrush(Colors.White);
            blackCanvas1.Background = blackCanvas2.Background = new SolidColorBrush(Colors.Black);

            cp.cp.MainColor = Colors.Red;

            cp.cp.secoundColor = Colors.Transparent;
            Selector.SetIsSelected(cp, true);

            cp.rectBtn.Command = myCommands.Rectangle;
            string invalidKeys=null;
            try
            {
                NHotkey.Wpf.HotkeyManager.Current.AddOrReplace("Transfer", Key.F3, ModifierKeys.None, OnTransfer);
            }
            catch { invalidKeys += "F3 - "; }
            try
            {
                NHotkey.Wpf.HotkeyManager.Current.AddOrReplace("ClearShape", Key.Escape, ModifierKeys.None, OnClearShape);
            }
            catch { invalidKeys += "Esc - "; }
            try
            {
                NHotkey.Wpf.HotkeyManager.Current.AddOrReplace("Record", Key.F7, ModifierKeys.None, OnRecord);
            }
            catch { invalidKeys += "F7 - "; }
            try
            {
                NHotkey.Wpf.HotkeyManager.Current.AddOrReplace("Pause", Key.F8, ModifierKeys.None, OnPause);
            }
            catch { invalidKeys += "F8"; }
            if(invalidKeys!=null)
            {
                MessageBox.Show("برخی کلیدها بخاطر آنکه قبل از این به یک برنامه در حال اجرا اختصاص داده شده اند در این برنامه اجرا نمی شوند: "+invalidKeys," اکت",MessageBoxButton.OK,MessageBoxImage.Warning,0,MessageBoxOptions.RightAlign|MessageBoxOptions.RtlReading);
            }
            cp.brushBtn.Command = myCommands.Brush;
            cp.arrowBtn.Command = myCommands.Arrow;
            cp.freeFormBtn.Command = myCommands.FreeForm;
            cp.LineBtn.Command = myCommands.Line;
            cp.EllipseBtn.Command = myCommands.Ellipse;
            cp.HideBtn.Command = myCommands.HideControlPanel;
            cp.PointerBtn.Command = myCommands.Move;
            cp.ScreenShotBtn.Command = myCommands.ScreenShot;
            cp.EraserBtn.Command = myCommands.Delete;
            cp.Recordbtn.Command = myCommands.Record;
            cp.Close.Click += Close_Click;
            cp.TransferBtn.Click += TransferBtn_Click;
            cp.WhiteItem1.Selected += WhiteItem_Selected;
            cp.TransItem.Selected += TransItem_Selected;
            cp.StaticItem.Selected += StaticItem_Selected;
            cp.WhiteItem2.Selected += WhiteItem2_Selected;
            cp.blackItem.Selected += BlackItem_Selected;
            cp.blackItem2.Selected += BlackItem2_Selected;
            cp.TransferItem.Selected += TransferItem_Selected;

            cp.Info.Click += Info_Click;
            //minPan.ShowBtn.Click += ShowBtn_Click;
            cp.ClearAllBtn.Command = myCommands.ClearAll;
            cp.TextBtn.Command = myCommands.Text;
            canvas.ActionChanged += Canvas_ActionChanged;
            _ = new BrushAction(canvas, cp.cp, cp.strokeSetyings);
            _ = new BrushAction(whiteCanvas1, cp.cp, cp.strokeSetyings);
            _ = new BrushAction(whiteCanvas2, cp.cp, cp.strokeSetyings);
            _ = new BrushAction(blackCanvas1, cp.cp, cp.strokeSetyings);

        }

        private void OnPause(object sender, HotkeyEventArgs e)
        {
            if (_rec != null)
            {
                if (_rec.Status == RecorderStatus.Recording)
                {
                    _rec.Pause();
                    cp.PauseBtn.Content = resumeContent;
                    cp.PauseBtn.ToolTip = "ادامه ضبط (F8)";
                    pauseMsg.Visibility = Visibility.Visible;
                }
                else if (_rec.Status == RecorderStatus.Paused)
                {
                    _rec.Resume();
                    pauseMsg.Visibility = Visibility.Hidden;
                    cp.PauseBtn.Content = pauseContent;
                    cp.PauseBtn.ToolTip = "توقف موقت ضبط (F8)";
                }
            }
   
        }

        private void OnRecord(object sender, HotkeyEventArgs e)
        {
            Record();
        }

        private void PauseBtn_Click(object sender, RoutedEventArgs e)
        {
            OnPause(null, null);
        }

        //private void ShowBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!minPan.Dragged)
        //    {
        //        if (this.Background.Opacity == 0)
        //            TransferBtn_Click(null, null);
        //        else

        //            cp.Visibility = Visibility.Visible;
        //        minPan.Visibility = Visibility.Hidden;
        //    }
        //}

        private void changeCanvas(myCanvas canv)
        {
            container.Content = canv;
            canvas = canv;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                var sa = canvas.CurrentAction as ShapeAction;
                if (sa != null)
                    sa.Cancel();
            }
        }

        private void Canvas_ActionChanged(WpfPaint.Codes.Controls.myCanvas canvas, PaintAction previousAction)
        {
            cp.DescLbl.Content = canvas.CurrentAction.Description;
        }

        private void OnClearShape(object sender, HotkeyEventArgs e)
        {
            var sa = canvas.CurrentAction as ShapeAction;
            if (sa != null)
                sa.Cancel();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            Act.About about = new Act.About();
            about.ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TransferBtn_Click(object sender, RoutedEventArgs e)
        {
            //OnTransfer(sender, new HotkeyEventArgs("Minimize"));
            cp.TransferItem.IsSelected = true;
        }

        private void StaticItem_Selected(object sender, RoutedEventArgs e)
        {
            changeCanvas(mainCanvas);
            WindowState = WindowState.Minimized;
            this.Background = new ImageBrush(Codes.Utilities.CopyScreen());
            WindowState = WindowState.Maximized;
            //minPan.Visibility = Visibility.Hidden;
            type = backType.Static;
        }

        private void TransItem_Selected(object sender, RoutedEventArgs e)
        {

            //this.Background = new SolidColorBrush(Colors.White);
            //this.Background.Opacity = 0.002;
            changeCanvas(mainCanvas);
            this.Background.Opacity = 0.002;
            type = backType.Trans;
            //minPan.Visibility = Visibility.Hidden;
        }

        private void WhiteItem_Selected(object sender, RoutedEventArgs e)
        {
            changeCanvas(whiteCanvas1);
            //minPan.Visibility = Visibility.Hidden;
            type = backType.Solid;

        }
        private void WhiteItem2_Selected(object sender, RoutedEventArgs e)
        {
            changeCanvas(whiteCanvas2);
            //minPan.Visibility = Visibility.Hidden;
            type = backType.Solid;

        }
        private void BlackItem_Selected(object sender, RoutedEventArgs e)
        {
            changeCanvas(blackCanvas1);
            //minPan.Visibility = Visibility.Hidden;
            type = backType.Solid;

        }
        private void BlackItem2_Selected(object sender, RoutedEventArgs e)
        {
            changeCanvas(blackCanvas2);
            //minPan.Visibility = Visibility.Hidden;
            type = backType.Solid;

        }
        private void TransferItem_Selected(object sender, RoutedEventArgs e)
        {
            this.Background.Opacity = 0;
            NHotkey.Wpf.HotkeyManager.Current.Remove("ClearShape");
            isInWorkSpace = true;
            //cp.Visibility = Visibility.Hidden;
            cp.Hide();
            if (type == backType.Solid)
                container.Content = null;

            //OnTransfer(null, null);


        }
        private void OnTransfer(object sender, HotkeyEventArgs e)
        {
            if (!isInWorkSpace)
            {
                
                //minPan.Visibility = Visibility.Visible;
                cp.TransferItem.IsSelected = true;
            }
            else
            {
                NHotkey.Wpf.HotkeyManager.Current.AddOrReplace("ClearShape", Key.Escape, ModifierKeys.None, OnClearShape);
                isInWorkSpace = false;

                //if (WindowState != WindowState.Minimized)
                //canvas.Visibility = Visibility.Visible;
                //if (type == backType.Static)
                //{
                //    WindowState = WindowState.Minimized;
                //    this.Background = new ImageBrush(Codes.Utilities.CopyScreen());
                //    WindowState = WindowState.Maximized;
                //}
                //else 
                if (type == backType.Solid)

                {
                    
                    if (canvas == blackCanvas1)
                        cp.blackItem.IsSelected = true;
                    else if (canvas == whiteCanvas1)
                        cp.WhiteItem1.IsSelected = true;
                    else if (canvas == blackCanvas2)
                        cp.blackItem2.IsSelected = true;
                    else
                        cp.WhiteItem2.IsSelected = true;
                    
                    //container.Content = canvas;
                    this.Background.Opacity = 1;
                }
                else
                {
                    cp.TransItem.IsSelected = true;
                    //this.Background.Opacity = 0.002;
                }


                //cp.Visibility = Visibility.Visible;
                cp.Show();
                //minPan.Visibility = Visibility.Hidden;
               
            }

        }

        private void Record_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Record();
            //  if (recordJob == null)
            //  { 
            //      recordJob = new ScreenCaptureJob();
            //      recordJob.CaptureRectangle = new System.Drawing.Rectangle(0, 0, (int)(SystemParameters.WorkArea.Width - (SystemParameters.WorkArea.Width % 4)), (int)(SystemParameters.WorkArea.Height - (SystemParameters.WorkArea.Height % 4)));
            //      recordJob.ShowCountdown = true;
            //      recordJob.OutputPath =System.AppDomain.CurrentDomain.BaseDirectory+ @"out";
            //      recordJob.OutputScreenCaptureFileName = System.AppDomain.CurrentDomain.BaseDirectory + @"out\" + DateTime.Now.ToString().Replace("/","").Replace(":"," ")+".x" ;
            //      EncoderDevice ed;
            //      var devices = EncoderDevices.FindDevices(EncoderDeviceType.Audio);
            //      if (devices.Count > 0)
            //      {
            //          ed = devices[0];
            //          recordJob.AddAudioDeviceSource(ed);
            //      }
            //  }
            //      if (recordJob.Status != RecordStatus.Running)
            //      {
            //      recordJob.Start();
            //      }
            //      else
            //      {
            //          recordJob.Stop();
            //      Job converterJob = new Job();
            //      MediaItem media = new MediaItem(recordJob.OutputScreenCaptureFileName);
            //      media.OutputFormat.VideoProfile.Bitrate = media.SourceVideoProfile.Bitrate;
            //      media.OutputFormat.VideoProfile.FrameRate = media.SourceVideoProfile.FrameRate;
            //      media.OutputFormat.AudioProfile = media.SourceAudioProfile;
            //      media.OutputFormat.VideoProfile.Size = media.SourceVideoProfile.Size;
            //      media.ResizeQuality = ResizeQuality.Bilinear;
            //  //    media.OverlayFileName = recordJob.OutputPath+ @"\copy.mp4";
            //  //     media.OverlayLayoutMode = OverlayLayoutMode.Custom;
            //  //media.OverlayRect = recordJob.CaptureRectangle;
            //      converterJob.MediaItems.Add(media);
            //      converterJob.OutputDirectory = recordJob.OutputPath;

            //      converterJob.Encode();

            ////      Process.Start(Path.ChangeExtension(recordJob.OutputScreenCaptureFileName,".mp4"));
            //      }



        }
        private void RectangleCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _ = new ShapeAction(canvas, new PRectangle(canvas), cp.cp, cp.strokeSetyings);
        }

        private void EllipseCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _ = new ShapeAction(canvas, new PEllipse(canvas), cp.cp, cp.strokeSetyings);
        }

        private void ClearShapeCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var sa = canvas.CurrentAction as ShapeAction;
            if (sa != null)
                sa.Cancel();
        }

        private void LineCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _ = new ShapeAction(canvas, new PLine(canvas), cp.cp, cp.strokeSetyings);
        }

        private void FreeFormCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _ = new ShapeAction(canvas, new PFreeForm(canvas), cp.cp, cp.strokeSetyings);
        }

        private void BrushCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _ = new BrushAction(canvas, cp.cp, cp.strokeSetyings);
        }

        private void ArrowCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _ = new ShapeAction(canvas, new PArrow(canvas), cp.cp, cp.strokeSetyings);
        }

        private void DeleteCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _ = new EraserAction(canvas);
        }

        private void MinimizeCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void HideCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (cp.IsVisible)
            {
                //cp.Visibility = Visibility.Hidden;
                cp.Hide();
                cpVisisbie = false;
                //minPan.Visibility = Visibility.Visible;
            }
            else
            {
                //cp.Visibility = Visibility.Visible;
                cp.Show();
                cpVisisbie = true;

            }
        }

        private void MoveCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _ = new MoveAction(canvas);

        }

        private void ScreenShotCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            bool v = cp.Visibility == Visibility.Visible;
            cp.Visibility = Visibility.Collapsed;
            this.UpdateLayout();

            var timer = new DispatcherTimer();

            timer.Interval = System.TimeSpan.FromMilliseconds(500);
            timer.Tick += (snd, te) =>
            {
                timer.Stop();
                var s = Codes.Utilities.CopyScreen();
                var sfd = new SaveFileDialog();
                sfd.Filter = "bmp(*.bmp)|*.bmp|Jpg(*.jpg)|*.jpg";
                sfd.Title = "انتخاب محل ذخیره";
                if (v)
                {

                    { cp.Visibility = Visibility.Visible; }

                }

                if (sfd.ShowDialog() == true)
                {


                    using (var fileStream = new System.IO.FileStream(sfd.FileName, System.IO.FileMode.Create))
                    {
                        BitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(s));
                        encoder.Save(fileStream);
                    }

                }


            };
            timer.Start();
        }


        //private void TransparentCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    if (Opacity != 0)
        //        this.Background.Opacity = 0;
        //    else if (type != backType.Solid)
        //    {
        //        this.Background.Opacity = 0.02;
        //    }
        //    else
        //    {

        //        WhiteItem_Selected(sender, e);
        //    }
        //}

        private void ClearAll_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            canvas.Children.Clear();
            canvas.Strokes.Clear();
        }

        private void Text_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            _ = new TextAction(canvas, cp.fontSettings, cp.cp);
            

        }

        ///////////////////////////
        ///recorder
        ///


        Recorder _rec;
        private bool isInWorkSpace;

        void Record()
        {

        //    if (_rec == null)
            //{

                List<string> inputDevices = Recorder.GetSystemAudioDevices(AudioDeviceSource.InputDevices);
                List<string> outputDevices = Recorder.GetSystemAudioDevices(AudioDeviceSource.OutputDevices);
                string selectedOutputDevice = outputDevices[0];//select one of the devices.. Passing empty string or null uses system default playback device.
                string selectedInputDevice = inputDevices[0]; //select one of the devices.. Passing empty string or null uses system default recording device.
                var AudioOptions = new AudioOptions
                {
                    IsAudioEnabled = true,
                    IsOutputDeviceEnabled = true,
                    IsInputDeviceEnabled = true,
                    AudioOutputDevice = selectedOutputDevice,
                    AudioInputDevice = selectedInputDevice
                };

                //Record to a file
                RecorderOptions options = new RecorderOptions
                {
                    RecorderMode = RecorderMode.Video,
                    //If throttling is disabled, out of memory exceptions may eventually crash the program,
                    //depending on encoder settings and system specifications.
                    IsThrottlingDisabled = false,
                    //Hardware encoding is enabled by default.
                    IsHardwareEncodingEnabled = true,
                    //Low latency mode provides faster encoding, but can reduce quality.
                    IsLowLatencyEnabled = false,
                    //Fast start writes the mp4 header at the beginning of the file, to facilitate streaming.
                    IsMp4FastStartEnabled = true,
                    AudioOptions = AudioOptions,
                   
                    VideoOptions = new VideoOptions
                    {
                        BitrateMode = BitrateControlMode.Quality,
                        Bitrate = 8000 * 1000,
                        Framerate = 60,
                        IsFixedFramerate = true
                        ,
                        EncoderProfile = H264Profile.High
                        ,Quality=100
                    },
                    MouseOptions = new MouseOptions
                    {
                        //Displays a colored dot under the mouse cursor when the left mouse button is pressed.	
                        IsMouseClicksDetected = true,
                        MouseClickDetectionColor = "#FFFF00",
                        MouseRightClickDetectionColor = "#FFFF00",
                        MouseClickDetectionRadius = 30,
                        MouseClickDetectionDuration = 100,

                        IsMousePointerEnabled = true,
                        /* Polling checks every millisecond if a mouse button is pressed.
                           Hook works better with programmatically generated mouse clicks, but may affect
                           mouse performance and interferes with debugging.*/
                        MouseClickDetectionMode = MouseDetectionMode.Hook
                    }
                };
               
            //}
            if (_rec==null||_rec.Status == RecorderStatus.Idle || _rec.Status == RecorderStatus.Finishing)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "پرونده mp4|*.mp4";

                if (sfd.ShowDialog().Value)
                {
                    SetQualityWin qw = new SetQualityWin();
                    qw.ShowDialog();
                    options.VideoOptions.Quality = qw.Quality;
                    _rec = Recorder.CreateRecorder(options);

                    _rec.OnRecordingComplete += Rec_OnRecordingComplete;
                    _rec.OnRecordingFailed += Rec_OnRecordingFailed;
                    _rec.OnStatusChanged += Rec_OnStatusChanged;
                    string videoPath = Path.Combine(sfd.FileName);
                    if (File.Exists(sfd.FileName))
                        File.Delete(sfd.FileName);
                    
                    _rec.Record(videoPath);
                    cp.PauseBtn.Content = pauseContent;
                    cp.PauseBtn.ToolTip = "توقف موقت ضبط (F8)";
                    cp.PauseBtn.Visibility = Visibility.Visible;
                    pauseMsg.Visibility = Visibility.Hidden;

                    Application.Current.Resources["tgShadowColor"] = Colors.Red;
                    
                }
            }
            else
                EndRecording();
            //..Or to a stream
            //_outStream = new MemoryStream();
            //_rec.Record(_outStream);
        }
        void EndRecording()
        {
            _rec.Stop();
            cp.PauseBtn.Visibility = Visibility.Hidden;
            pauseMsg.Visibility = Visibility.Hidden;


        }
        private void Rec_OnRecordingComplete(object sender, RecordingCompleteEventArgs e)
        {
            Application.Current.Resources["tgShadowColor"] = Colors.Black;
            //Get the file path if recorded to a file
            string path = e.FilePath;
            //or do something with your stream
            //... something ...

        }
        private void Rec_OnRecordingFailed(object sender, RecordingFailedEventArgs e)
        {
            string error = e.Error;
            MessageBox.Show("خطایی در نماگرفت (کپچر) به وجود آمده است" + error.ToString(), "خطا", MessageBoxButton.OK, MessageBoxImage.Error); ;
            Application.Current.Resources["tgShadowColor"] = Colors.Black;

        }
        private void Rec_OnStatusChanged(object sender, RecordingStatusEventArgs e)
        {
            RecorderStatus status = e.Status;
            
           
        }
        /////////////////////////////////////////////////////////////////////////////////////
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            if (_rec != null && (_rec.Status == RecorderStatus.Recording || _rec.Status==RecorderStatus.Paused))
                _rec.Stop();
            e.Cancel = false;

        }
       
    }
}

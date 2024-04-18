using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace dastyar.Codes.Controls
{
    /// <summary>
    /// Interaction logic for FontSettings.xaml
    /// </summary>
    public partial class FontSettings : UserControl
    {

        public FontSettings()
        {
            InitializeComponent();
            FontNameCB.Text = "Tahoma";
            FontSizeCB.Text = "12";
            Font_Family = new FontFamily("Tahoma");
            Font_Size = 12;
            //rtlBtn.IsChecked = true;
            ///    tbm.Add(centerBtn, leftBtn, rightBtn/*,justifyBtn*/);
            // rightBtn.IsChecked = true;

        }
        ToggleButtonManager tbm = new ToggleButtonManager();
        public delegate void FontChangedEH(FontSettings settingsPanel);
        public event FontChangedEH FontChanged;
        public bool PreventFontChangedRasing { get; set; }
        public bool IsBold { get; private set; }
        public bool IsItalic
        { get; private set; }
        public double Font_Size { get; private set; }
        public FontFamily Font_Family { private set; get; }
        public enum Direction { ltr, rtl }
        public FlowDirection TextDirection { get; set; }
        public TextRange TextRange { get; set; }
        public void Set(TextRange TextRange)
        {
            PreventFontChangedRasing = true;
            BoldBtn.IsChecked = (TextRange.GetPropertyValue(TextElement.FontWeightProperty).Equals(FontWeights.Bold));
            ItalicBtn.IsChecked = TextRange.GetPropertyValue(TextElement.FontStyleProperty).Equals(FontStyles.Italic);
            FontSizeCB.Text = TextRange.GetPropertyValue(TextElement.FontSizeProperty).ToString();
            FontNameCB.SelectedItem = TextRange.GetPropertyValue(TextElement.FontFamilyProperty);
            UnderlineBtn.IsChecked = TextRange.GetPropertyValue(Inline.TextDecorationsProperty).Equals(TextDecorations.Underline);
            PreventFontChangedRasing = false;

        }
        public void Set4Paragraph(Paragraph par)
        {
            if (par == null) return;
            //rtlBtn.IsChecked = par.FlowDirection == FlowDirection.RightToLeft;
            //ltrBtn.IsChecked = !rtlBtn.IsChecked;
            //leftBtn.IsChecked = par.TextAlignment.Equals(TextAlignment.Left);
            //centerBtn.IsChecked = par.TextAlignment.Equals(TextAlignment.Center);
            //rightBtn.IsChecked = par.TextAlignment.Equals(TextAlignment.Right);
            //justifyBtn.IsChecked = par.TextAlignment.Equals(TextAlignment.Justify);



        }
        public void SetInputProperties(TextRange TextRange, Paragraph paragraph)
        {
            if (IsBold)
                TextRange.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            else
                TextRange.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            if (IsItalic)
                TextRange.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            else
                TextRange.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);

            TextRange.ApplyPropertyValue(TextElement.FontSizeProperty, Font_Size);
            TextRange.ApplyPropertyValue(TextElement.FontFamilyProperty, Font_Family);
            TextRange.ApplyPropertyValue(Paragraph.TextDecorationsProperty, UnderlineBtn.IsChecked.Value ? TextDecorations.Underline : new TextDecorationCollection());
            if (paragraph != null)
            {
                //paragraph.FlowDirection = TextDirection;

                //if (centerBtn.IsChecked.Value)
                //    paragraph.TextAlignment = TextAlignment.Center;
                //else if (leftBtn.IsChecked.Value)
                //    paragraph.TextAlignment = TextDirection==FlowDirection.RightToLeft? TextAlignment.Left:TextAlignment.Right;
                //else if (rightBtn.IsChecked.Value)
                //    paragraph.TextAlignment = TextDirection == FlowDirection.RightToLeft ? TextAlignment.Right:TextAlignment.Left;
                //else if (justifyBtn.IsChecked.Value)
                //    paragraph.TextAlignment = TextAlignment.Justify;
            }
            //else if (TextDirection == FlowDirection.RightToLeft)
            //{
            //    TextRange.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Right);
            //    rig
            //}
            //else
            //    TextRange.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Left);






        }
        private void BoldBtn_Checked(object sender, RoutedEventArgs e)
        {

            IsBold = true;
            if (FontChanged != null && !PreventFontChangedRasing)
                FontChanged(this);
        }

        private void BoldBtn_Unchecked(object sender, RoutedEventArgs e)
        {

            IsBold = false;
            if (FontChanged != null && !PreventFontChangedRasing)
                FontChanged(this);
        }

        private void ItalicBtn_Checked(object sender, RoutedEventArgs e)
        {

            IsItalic = true;
            if (FontChanged != null && !PreventFontChangedRasing)
                FontChanged(this);
        }

        private void ItalicBtn_Unchecked(object sender, RoutedEventArgs e)
        {

            IsItalic = false;
            if (FontChanged != null && !PreventFontChangedRasing)
                FontChanged(this);
        }


        private void FontSizeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;
            Font_Size = (double.Parse((e.AddedItems[0] as ComboBoxItem).Content.ToString()));
            if (FontChanged != null && !PreventFontChangedRasing)
                FontChanged(this);
        }

        private void FontNameCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Font_Family = (FontFamily)FontNameCB.SelectedItem;
            if (FontChanged != null && !PreventFontChangedRasing)
                FontChanged(this);
        }

        private void rtlBtn_Checked(object sender, RoutedEventArgs e)
        {
            TextDirection = FlowDirection.LeftToRight; // why? i dont know
            //ltrBtn.IsChecked = false;
            //rightBtn.IsChecked = true;
            if (FontChanged != null && !PreventFontChangedRasing)
                FontChanged(this);
        }

        private void ltrBtn_Checked(object sender, RoutedEventArgs e)
        {
            TextDirection = FlowDirection.RightToLeft; //why

            //rtlBtn.IsChecked = false;
            //leftBtn.IsChecked = true;

            if (FontChanged != null && !PreventFontChangedRasing)
                FontChanged(this);
        }

        private void UnderlineBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (FontChanged != null && !PreventFontChangedRasing)
                FontChanged(this);
        }

        private void UnderlineBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            if (FontChanged != null && !PreventFontChangedRasing)
                FontChanged(this);
        }

        //private void centerBtn_Checked(object sender, RoutedEventArgs e)
        //{

        //    if (FontChanged != null && !PreventFontChangedRasing)
        //        FontChanged(this);
        //}

        private void alignment_changed(object sender, RoutedEventArgs e)
        {
            if (FontChanged != null && !PreventFontChangedRasing)
                FontChanged(this);
            //if(!centerBtn.IsChecked.Value&&!leftBtn.IsChecked.Value&&!rightBtn.IsChecked.Value/*&&!justifyBtn.IsChecked.Value*/)
            //{
            //    if (TextDirection == FlowDirection.RightToLeft)
            //        rightBtn.IsChecked = true;
            //    else
            //        leftBtn.IsChecked = true;
            //}

        }

    }
}

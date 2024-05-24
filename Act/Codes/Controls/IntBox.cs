using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace Act.Codes.Controls
{
    public class IntBox : TextBox
    {

        public IntBox()
        {
            Text = "0";
        }

        public delegate void ValueChangedEH(IntBox sender);
        public event ValueChangedEH ValueChanged;

        int _min = -5, _max = 100;
        public int Min
        {
            get { return _min; }
            set { if (value > _max) return; if (value > Value) Text = value.ToString(); _min = value; }
        }
        public int Max
        {
            get { return _max; }
            set { if (value < _min) return; _max = value; }
        }

        [DefaultValue(0)]
        public int Value
        {
            get { return int.Parse(Text); }
            set { if (value < _min) Text = _min.ToString(); else if (value > _max) Text = _max.ToString(); else Text = value.ToString(); }
        }
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);
            Regex r = new Regex("^[^0-9\b]$");
            if (r.IsMatch(e.Text) && !(_min < 0 && e.Text == "-" && SelectionStart == 0))
                e.Handled = true;
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            int tmp;

            if (!int.TryParse(Text, out tmp) && !(_min < 0 && Text == "-"))
            {
                Text = _min.ToString();
            }
            else if (tmp > _max)
            {
                Text = _max.ToString();
            }

            else if (tmp < _min)
            {
                Text = _min.ToString();
            }
            if (ValueChanged != null)
            {
                ValueChanged(this);
            }
        }

    }
}

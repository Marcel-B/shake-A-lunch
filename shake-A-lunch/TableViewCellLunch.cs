using System;
using UIKit;

namespace shakeAlunch
{
    public partial class TableViewCellLunch : UITableViewCell
    {
        public string Lunch
        {
            get => LabelLunch.Text;
            set => LabelLunch.Text = value;
        }
        public TableViewCellLunch(IntPtr handle) : base(handle) { }
    }
}
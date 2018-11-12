using System;
using UIKit;
using shake_A_lunch.std;

namespace shakeAlunch
{
    public partial class MainViewController : UIViewController
    {
        public MainViewController(IntPtr handle) : base(handle)
        {
        }

        public override void MotionEnded(UIEventSubtype motion, UIEvent evt)
        {
            base.MotionEnded(motion, evt);
            var store = AppStore.Instance;
            var cnt = store.Locations.Count;
            if (cnt == 0)
            {
                LabelLunch.Text = "First enter some Locations, dude!";
                return;
            }
            var idx = store.Rnd.Next(cnt);
            LabelLunch.Text = store.Locations[idx];
        }
    }
}
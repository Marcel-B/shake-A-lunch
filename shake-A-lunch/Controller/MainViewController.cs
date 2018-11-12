using System;
using UIKit;
using shake_A_lunch.std;
using Xamarin.Essentials;

namespace shakeAlunch
{
    public partial class MainViewController : UIViewController
    {
        public MainViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ButtonAccept.SetTitle("", UIControlState.Normal);
            ButtonAccept.Hidden = true;
            ButtonAccept.TouchUpInside += (object sender, EventArgs e) =>
            {
                if (sender is UIButton btn)
                {
                    var idx = btn.Tag;
                    AppStore.Instance.Locations[(int)idx].Accepted++;
                    btn.Hidden = true;
                    LabelLunch.Hidden = false;
                    btn.SetTitle("", UIControlState.Normal);
                    LabelLunch.Text = $">>{AppStore.Instance.Locations[(int)idx].Name}<<";
                    AppStore.Instance.Locations.Sort();
                }
            };
        }

        public override void MotionEnded(UIEventSubtype motion, UIEvent evt)
        {
            base.MotionEnded(motion, evt);

            var store = AppStore.Instance;
            var cnt = store.Locations.Count;
            Vibration.Vibrate();
            // Or use specified time
            var duration = TimeSpan.FromSeconds(1);
            Vibration.Vibrate(duration);
            if (cnt == 0)
            {
                LabelLunch.Text = "First enter some Locations, dude!";
                return;
            }
            LabelLunch.Hidden = true;
            ButtonAccept.Hidden = false;
            var idx = store.Rnd.Next(cnt);
            ButtonAccept.SetTitle(store.Locations[idx].Name, UIControlState.Normal);
            ButtonAccept.Tag = idx;
            store.Locations[idx].Count++;
        }
    }
}
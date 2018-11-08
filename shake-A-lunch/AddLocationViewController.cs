using System;
using UIKit;
using shake_A_lunch.std;

namespace shakeAlunch
{
    public partial class AddLocationViewController : UIViewController
    {
        public AddLocationViewController(IntPtr handle) : base(handle){}
        partial void Add(UIButton sender)
        {
            AppStore.Instance.Locations.Add(TextFieldLunch.Text);
            NavigationController.PopToRootViewController(true);
        }
    }
}
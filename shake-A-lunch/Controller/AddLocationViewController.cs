using System;
using UIKit;
using shake_A_lunch.std;
using shake_A_lunch.std.Models;

namespace shakeAlunch
{
    public partial class AddLocationViewController : UIViewController
    {
        public AddLocationViewController(IntPtr handle) : base(handle){}
        partial void Add(UIButton sender)
        {
            var location = new Location
            {
                Name = TextFieldLunch.Text,
                Created = DateTimeOffset.Now,
                Count = 0,
                Accepted = 0,
                Active = true
            };
            AppStore.Instance.Locations.Add(location);
            NavigationController.PopToRootViewController(true);
        }
    }
}
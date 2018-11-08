// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace shakeAlunch
{
    [Register ("AddLocationViewController")]
    partial class AddLocationViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonSave { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TextFieldLunch { get; set; }

        [Action ("Add:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Add (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (ButtonSave != null) {
                ButtonSave.Dispose ();
                ButtonSave = null;
            }

            if (TextFieldLunch != null) {
                TextFieldLunch.Dispose ();
                TextFieldLunch = null;
            }
        }
    }
}
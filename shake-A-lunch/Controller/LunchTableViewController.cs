using System;
using Foundation;
using shake_A_lunch.std;
using UIKit;
using System.Collections.ObjectModel;
using System.Linq;
using CoreGraphics;
using System.Drawing;
using shake_A_lunch.std.Models;

namespace shakeAlunch
{
    public partial class LunchTableViewController : UITableViewController
    {
        public ObservableCollection<Location> Lunches { get; set; }
        public LunchTableViewController(IntPtr handle) : base(handle)
        {
            Lunches = new ObservableCollection<Location>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            TableView.RowHeight = 100;
            Lunches.Clear();
            foreach (var location in AppStore.Instance.Locations)
                Lunches.Add(location);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            AppStore.Instance.Locations = Lunches.ToList();
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("LocationCell") as TableViewCellLunch;
            cell.Lunch = Lunches[indexPath.Row].Name;
            cell.Counter = $"{Lunches[indexPath.Row].Accepted} / {Lunches[indexPath.Row].Count}";
            return cell;
        }

        //public override UISwipeActionsConfiguration GetLeadingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
        //{
        //    //var definitionAction = ContextualDefinitionAction(indexPath.Row);
        //    var flagAction = ContextualFlagAction(indexPath, tableView);
        //    var leadingSwipe = UISwipeActionsConfiguration.FromActions(new UIContextualAction[] { flagAction });
        //    leadingSwipe.PerformsFirstActionWithFullSwipe = true;
        //    return leadingSwipe;
        //}

        public UIContextualAction ContextualDefinitionAction(int row)
        {
            string word = Lunches[row].Name;
            var action = UIContextualAction.FromContextualActionStyle(UIContextualActionStyle.Normal,
                        "Definition",
                        (ReadLaterAction, view, success) =>
                        {
                            var def = new UIReferenceLibraryViewController(word);

                            var alertController = UIAlertController.Create("No Dictionary Installed", "To install a Dictionary, Select Definition again, click `Manage` on the next screen and select a dictionary to download", UIAlertControllerStyle.Alert);
                            alertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                            if (UIReferenceLibraryViewController.DictionaryHasDefinitionForTerm(word))
                            {
                                PresentViewController(def, true, null);
                                success(true);
                            }
                            else
                            {
                                PresentViewController(alertController, true, null);
                                success(false);
                            }
                        });
            action.BackgroundColor = UIColor.Orange;
            return action;
        }

        public UIContextualAction ContextualFlagAction(NSIndexPath indexPath, UITableView tableView)
        {
            var action = UIContextualAction.FromContextualActionStyle(UIContextualActionStyle.Normal,
              "",
              (FlagAction, view, success) =>
              {
                  Lunches.RemoveAt(indexPath.Row);
                  tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                  success(true);
              });
            action.Image = UIImage.FromBundle("Trash");
            action.BackgroundColor = UIColor.Red;
            return action;
        }

        //public override UITableViewRowAction[] EditActionsForRow(UITableView tableView, NSIndexPath indexPath)
        //{

        //    var someAction = UITableViewRowAction.Create(
        //        UITableViewRowActionStyle.Destructive, "Delete", (arg1, arg2) =>
        //        {
        //            // remove the item from the underlying data source
        //            Lunches.RemoveAt(arg2.Row);
        //            // delete the row from the table
        //            tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
        //        });
        //    var image = UIImage.FromBundle("Bin", NSBundle.MainBundle, null);
        //    var img = image.Scale(new CGSize(80, 100));
        //    someAction.BackgroundColor = UIColor.FromPatternImage(img);
        //    return new[] { someAction };
        //}

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, Foundation.NSIndexPath indexPath)
        {
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    // remove the item from the underlying data source
                    Lunches.RemoveAt(indexPath.Row);
                    // delete the row from the table
                    tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                    break;
                case UITableViewCellEditingStyle.None:
                    Console.WriteLine("CommitEditingStyle:None called");
                    break;
            }
        }
        private UIImage GetColoredImage(string imageName, UIColor color)
        {
            UIImage image = UIImage.FromBundle(imageName);
            UIImage coloredImage = null;

            UIGraphics.BeginImageContext(image.Size);
            using (CGContext context = UIGraphics.GetCurrentContext())
            {

                context.TranslateCTM(0, image.Size.Height);
                context.ScaleCTM(1.0f, -1.0f);

                var rect = new RectangleF(0, 0, (float)image.Size.Width, (float)image.Size.Height);

                // draw image, (to get transparancy mask)
                context.SetBlendMode(CGBlendMode.Normal);
                context.DrawImage(rect, image.CGImage);

                // draw the color using the sourcein blend mode so its only draw on the non-transparent pixels
                context.SetBlendMode(CGBlendMode.SourceIn);
                context.SetFillColor(color.CGColor);
                context.FillRect(rect);

                coloredImage = UIGraphics.GetImageFromCurrentImageContext();
                UIGraphics.EndImageContext();
            }
            return coloredImage;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            => tableView.DeselectRow(indexPath, true);

        public override nint RowsInSection(UITableView tableView, nint section)
            => Lunches.Count;
    }
}
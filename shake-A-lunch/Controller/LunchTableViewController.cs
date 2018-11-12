using System;
using Foundation;
using shake_A_lunch.std;
using UIKit;
using System.Collections.ObjectModel;
using System.Linq;

namespace shakeAlunch
{
    public partial class LunchTableViewController : UITableViewController
    {
        public ObservableCollection<string> Lunches { get; set; }

        public LunchTableViewController(IntPtr handle) : base(handle)
        {
            Lunches = new ObservableCollection<string>();
            Lunches.CollectionChanged += Lunches_CollectionChanged;
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
            Lunches.CollectionChanged -= Lunches_CollectionChanged;
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            AppStore.Instance.Locations = Lunches.ToList();
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("LocationCell") as TableViewCellLunch;
            cell.Lunch = Lunches[indexPath.Row];
            return cell;
        }

        void Lunches_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
            => TableView.ReloadData();

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
            => Lunches.RemoveAt(indexPath.Row);

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            => tableView.DeselectRow(indexPath, true);

        public override nint RowsInSection(UITableView tableView, nint section)
            => Lunches.Count;
    }
}
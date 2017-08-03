// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using System.Collections.Generic;
using Microsoft.ProjectOxford.Vision.Contract;
using Plugin.TextToSpeech;
using System.Linq;

namespace CogTourist
{
	public partial class LanguageController : UITableViewController, IUITableViewDataSource, IUITableViewDelegate
	{
        int currentlySelectedRow = 0;

		public LanguageController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            TableView.DataSource = this;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell tc = null;

            tc = this.TableView.DequeueReusableCell("id");

            if (tc == null)
                tc = new UITableViewCell(UITableViewCellStyle.Default, "id");

            tc.TextLabel.Text = SupportedLanguages.Languages[indexPath.Row].DisplayName;
            tc.Accessory = UITableViewCellAccessory.None;
            tc.SelectionStyle = UITableViewCellSelectionStyle.None;

            if (SupportedLanguages.Languages[indexPath.Row].LanguageCode == AppDelegate.CurrentLanguage.LanguageCode)
            {
                currentlySelectedRow = indexPath.Row;
                tc.Accessory = UITableViewCellAccessory.Checkmark;
            }

            return tc;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return SupportedLanguages.Languages.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.CellAt(NSIndexPath.FromRowSection(currentlySelectedRow, 0)).Accessory = UITableViewCellAccessory.None;
            tableView.CellAt(indexPath).Accessory = UITableViewCellAccessory.Checkmark;
            currentlySelectedRow = indexPath.Row;

            AppDelegate.CurrentLanguage = SupportedLanguages.Languages[indexPath.Row];
        }
	}
}

// This file has been autogenerated from a class added in the UI designer.
using System;

using Foundation;
using UIKit;

using CogTourist.Core;
using Microsoft.ProjectOxford.Vision;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;

namespace CogTourist
{
    public partial class LandmarkViewController : UIViewController
    {
        UIAlertController alert = null;
        PhotoService ps = null;
        VisionService vs = null;
        LoadingView loading;

        public LandmarkViewController(IntPtr handle) : base(handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Landmarks";
            selectedPhoto.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 70);
            descriptionLabel.Text = "";

            alert = UIAlertController.Create("Vision API", "Pick or take photos", UIAlertControllerStyle.ActionSheet);
            BuildAlertActions();

            vs = new VisionService();
            ps = new PhotoService();

            selectedPhoto.ContentMode = UIViewContentMode.ScaleAspectFit;

            photoButton.TouchUpInside += (sender, e) =>
            {
                PresentViewController(alert, true, null);
            };
        }

        void BuildAlertActions()
        {
            var pickAction = UIAlertAction.Create("Pick Photo", UIAlertActionStyle.Default, async (obj) => await TakeOrPickPhoto(false));
            var takeAction = UIAlertAction.Create("Take Photo", UIAlertActionStyle.Default, async (obj) => await TakeOrPickPhoto(true));
            var cancelAction = UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null);

            alert.AddAction(pickAction);
            alert.AddAction(takeAction);
            alert.AddAction(cancelAction);
        }

        async Task TakeOrPickPhoto(bool takePhoto)
        {
            selectedPhoto.Image = null;
            loading = new LoadingView(UIScreen.MainScreen.Bounds);
            View.Add(loading);

            MediaFile returnedPhoto = null;

            if (takePhoto)
                returnedPhoto = await ps.TakePhoto();
            else
                returnedPhoto = await ps.PickPhoto();

            if (returnedPhoto == null)
            {
                loading.Hide();
                return;
            }

            using (var photoStream = returnedPhoto.GetStream())
            {
                selectedPhoto.Image = new UIImage(NSData.FromStream(photoStream));

                photoStream.Position = 0;

                var landmarks = await vs.DescribePhoto(photoStream);

                descriptionLabel.Text = landmarks?.Landmarks?.FirstOrDefault()?.Name ?? "that's not a landmark!";
            }

            loading.Hide();
        }



    }
}
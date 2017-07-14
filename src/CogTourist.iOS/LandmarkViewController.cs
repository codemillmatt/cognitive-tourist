// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

using CogTourist.Core;
using Microsoft.ProjectOxford.Vision;
using System.IO;

namespace CogTourist
{
    public partial class LandmarkViewController : UIViewController
    {
        public LandmarkViewController(IntPtr handle) : base(handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            selectedPhoto.ContentMode = UIViewContentMode.ScaleAspectFit;

            photoButton.TouchUpInside += (sender, e) =>
            {
                var alert = UIAlertController.Create("Vision", "Pick or take photos", UIAlertControllerStyle.ActionSheet);

                var photoAction = UIAlertAction.Create("Pick photo", UIAlertActionStyle.Default, async (obj) =>
                {
                    descriptionLabel.Text = "please wait...";

                    var ps = new PhotoService();
                    var photo = await ps.PickPhoto();

                    if (photo == null)
                        return;

                    selectedPhoto.Image = new UIImage(NSData.FromStream(photo));

                    // Need to reset the stream position before sending it on
                    photo.Position = 0;
                    var vs = new VisionService();
                    descriptionLabel.Text = await vs.DescribePhoto(photo);
                });

                alert.AddAction(photoAction);

                var cameraAction = UIAlertAction.Create("Take photo", UIAlertActionStyle.Default, async (obj) =>
                {
                    descriptionLabel.Text = "please wait...";

                    var ps = new PhotoService();
                    var photo = await ps.TakePhoto();

                    var photoStream = photo?.GetStream();
                    if (photoStream == null)
                        return;

                    selectedPhoto.Image = new UIImage(NSData.FromStream(photoStream));

                    photoStream.Position = 0;
                    var vs = new VisionService();
                    // descriptionLabel.Text = await vs.DescribePhoto(photoStream);
                    descriptionLabel.Text = await vs.OCRPhoto(photoStream);
                });
                alert.AddAction(cameraAction);

                PresentViewController(alert, true, null);
            };
        }

    }
}

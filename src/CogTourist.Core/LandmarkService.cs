using System;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Media;
using System.Threading;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using Plugin.Media.Abstractions;

namespace CogTourist.Core
{
    public class LandmarkService
    {
        static readonly string apiKey = "fb6e847547c84b2baf20dc43ae0681f8";
        static readonly string url = "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0";

        public async Task<LandmarkIdentity> PickPhotoForIdentification()
        {
            try
            {
                if (!await CheckPhotoLibraryPermissions())
                    return null;

                if (!CrossMedia.Current.IsPickPhotoSupported)
                    return null;

                var photo = await CrossMedia.Current.PickPhotoAsync();
                var photoStream = photo.GetStream();

                var vc = new VisionServiceClient(apiKey, url);
                var desc = await vc.DescribeAsync(photoStream);

                return new LandmarkIdentity
                {
                    Description = desc.Description.Captions[0].Text,
                    Photo = photo.GetStream()
                };
            }
            catch (Exception ex)
            {
                var s = ex.ToString();
                return null;
            }
        }

        public async Task<LandmarkIdentity> TakePhotoForIdentification()
        {
            try
            {
                if (!await CheckCameraPermissions())
                    return null;

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    return null;

                var photoOptions = new StoreCameraMediaOptions
                {
                    SaveToAlbum = true
                };

                var photo = await CrossMedia.Current.TakePhotoAsync(photoOptions);
                var photoStream = photo.GetStream();

                var vc = new VisionServiceClient(apiKey, url);
                var desc = await vc.DescribeAsync(photo.GetStream());

                return new LandmarkIdentity
                {
                    Description = desc?.Description?.Captions[0]?.Text,
                    Photo = photoStream
                };
            }
            catch (Exception ex)
            {
                var s = ex.ToString();
                return null;
            }
        }

        async Task<bool> CheckCameraPermissions()
        {
            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);

            if (cameraStatus != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                {
                    // TODO: Show a dialog
                }

                var grantedPermissions = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);

                if (grantedPermissions.ContainsKey(Permission.Camera))
                    cameraStatus = grantedPermissions[Permission.Camera];
            }

            return cameraStatus == PermissionStatus.Granted;
        }

        async Task<bool> CheckPhotoLibraryPermissions()
        {
            var photoStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Photos);

            if (photoStatus != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Photos))
                {
                    // TODO: show a dialog
                }

                var grantedPermissions = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Photos);

                if (grantedPermissions.ContainsKey(Permission.Photos))
                    photoStatus = grantedPermissions[Permission.Photos];
            }

            return photoStatus == PermissionStatus.Granted;
        }
    }
}

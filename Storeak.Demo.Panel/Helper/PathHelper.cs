using StoreakApiService.Core.Projects;

namespace Storeak.Demo.Panel.Helper
{
    public static class PathHelper
    {
        public static string defaultPicturePath = ApiProjectSettings.AdminAssetPath + "emptypic.jpg";
        public static string defaultUserPicturePath = ApiProjectSettings.AdminAssetPath + "keentheme/media/users/default.jpg";

        public static string GetImagePath(string ImagePath)
        {
            return string.IsNullOrEmpty(ImagePath) ? defaultPicturePath :  ImagePath;
        }
        public static string GetImageBase64Path(string ImageBase64, string DefaultImage = "emptypic.jpg")
        {
            if (DefaultImage == "emptypic.jpg")
            {
                return string.IsNullOrEmpty(ImageBase64) ? defaultPicturePath : ImageBase64;
            }
            else
            {
                if (string.IsNullOrEmpty(DefaultImage))
                {
                    return string.IsNullOrEmpty(ImageBase64) ? defaultPicturePath : ImageBase64;
                }
                else
                {
                    return string.IsNullOrEmpty(ImageBase64) ? DefaultImage : ImageBase64;
                }
            }
        }
        public static string GetUserImagePath(string ImagePath)
        {
            return string.IsNullOrEmpty(ImagePath) ? defaultUserPicturePath :  ImagePath;
        }
        public static string GetUserImageBase64Path(string ImageBase64, string DefaultImage = "default.jpg")
        {
            if (DefaultImage == "default.jpg")
            {
                return string.IsNullOrEmpty(ImageBase64) ? defaultUserPicturePath : ImageBase64;
            }
            else
            {
                if (string.IsNullOrEmpty(DefaultImage))
                {
                    return string.IsNullOrEmpty(ImageBase64) ? defaultUserPicturePath : ImageBase64;
                }
                else
                {
                    return string.IsNullOrEmpty(ImageBase64) ?  DefaultImage : ImageBase64;
                }
            }
        }
    }
}

using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static Xamarin.Essentials.Permissions;

namespace PerboyreApp.Helpers
{
    public static class PermissionHelper
    {
        public static async Task<PermissionStatus> CheckAndRequestPermission(BasePlatformPermission permission)
        {
            var status = await permission.CheckStatusAsync();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Prompt the user to turn on in settings
                // On iOS once a permission has been denied it may not be requested again from the application
                return status;
            }

            status = await Permissions.RequestAsync<Permissions.LocationAlways>();

            return status;
        }
    }
}

using System;
using Android.Content;

namespace PerboyreApp.Droid.Utils
{
    public class ResourceUtil
    {
        public static int GetDrawableIdByFileName(string fileName, Context context)
        {
            return context.Resources.GetIdentifier(fileName, "drawable", context.PackageName);
        }
    }
}

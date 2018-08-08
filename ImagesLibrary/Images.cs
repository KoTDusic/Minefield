using System;

namespace ImagesLibrary
{
    public class Images
    {
        public static string LoaderKey = "pack://application:,,,/ImagesLibrary;component/Images/loader.gif";
        public static Uri LoaderImage = new Uri(LoaderKey, UriKind.Absolute);

        public static string MineKey = "pack://application:,,,/ImagesLibrary;component/Images/mine.png";
        public static Uri MineImage = new Uri(MineKey, UriKind.Absolute);
    }
}

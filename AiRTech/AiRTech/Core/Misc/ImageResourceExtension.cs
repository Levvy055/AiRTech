using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using AiRTech.Core.DataHandling;
using AiRTech.Core.Web;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AiRTech.Core.Misc
{
    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
        private static ImageSource _defaultEmptyImage;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }
            var assembly = typeof(ImageResourceExtension).GetTypeInfo().Assembly;
            var imageSource = ImageSource.FromResource(Source, assembly);

            return imageSource;
        }

        public static ImageSource GetEmbeddedImage(string img)
        {
            var assembly = typeof(ImageResourceExtension).GetTypeInfo().Assembly;
            var imageSource = ImageSource.FromResource(img, assembly);
            return imageSource;
        }

        public static async Task<ImageSource> GetImageFromUri(string pathToImg)
        {
            Uri imgUri;
            if (!Uri.TryCreate(pathToImg, UriKind.RelativeOrAbsolute, out imgUri))
            {
                return DefaultEmptyImage;
            }
            var app = Application.Current as App;
            try
            {
                if (!app.FileHandler.Exists(imgUri) || app.FileHandler.IsEmpty(imgUri))
                {
                    var s = false;
                    using (var fs = app.FileHandler.GetFileStream(pathToImg))
                    {
                        if (await app.Web.GetImage(pathToImg, fs).ConfigureAwait(false))
                        {
                            s = true;
                        }
                    }
                    if (!s)
                    {
                        Debug.WriteLine("Image File " + pathToImg + " not found! ");
                        app.FileHandler.RemoveFile(imgUri);
                        return DefaultEmptyImage;
                    }
                }
                Uri rUri;
                if (!Uri.TryCreate(new Uri(app.FileHandler.RootAppPath() + "\\"), imgUri, out rUri))
                {
                    Debug.WriteLine("Image File " + pathToImg + " not found! ");
                    return DefaultEmptyImage;
                }
                var imageSource = ImageSource.FromFile(FileHelper.GetAboslutePath(rUri));
                return imageSource;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Image File " + pathToImg + " not found! " + e);
                app.FileHandler.RemoveFile(imgUri);
                return DefaultEmptyImage;
            }
        }

        public static ImageSource GetImageFromFile(string pathToImg)
        {
            Uri imgUri;
            var imageSource = Uri.TryCreate(pathToImg, UriKind.Absolute, out imgUri)
                ? ImageSource.FromFile(pathToImg) : _defaultEmptyImage;
            return imageSource;
        }

        public string Source { get; set; }

        public static ImageSource DefaultEmptyImage =>
            _defaultEmptyImage ??
            (_defaultEmptyImage = GetEmbeddedImage("AiRTech.Resources.no-image.png"));
    }
}

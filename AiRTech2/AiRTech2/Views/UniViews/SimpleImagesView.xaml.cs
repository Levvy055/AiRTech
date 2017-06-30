using System.Collections.Generic;
using AiRTech2.Misc;
using AiRTech2.ViewModels.Subjects;
using AiRTech2.ViewModels.Subjects.BasicSignalParams;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AiRTech2.Views.UniViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimpleImagesView : ContentView
    {
        private readonly SubjectViewModel _viewModel;

        public SimpleImagesView(params string[] images)
        {
            InitializeComponent();
            //LoadImages(images);
        }

        private void LoadImages(IReadOnlyCollection<string> images)
        {
            if (images == null || images.Count == 0)
            {
                return;
            }
            foreach (var imgS in images)
            {
                var img = ImageResourceExtension.GetEmbeddedImage(imgS);
                if (img != ImageResourceExtension.DefaultEmptyImage)
                {
                    ImageContainer.Children.Add(new Image { Source = img });
                }
            }
        }
    }
}
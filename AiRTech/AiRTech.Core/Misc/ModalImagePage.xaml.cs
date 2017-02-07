using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AiRTech.Core.Misc
{
    public partial class ModalImagePage : ContentPage
    {
        public ModalImagePage(ImageSource iS)
        {
            Content = new Grid
            {
                Padding = new Thickness(20),
                Children = {
                    new PanContainer {
                        Content = new PinchToZoomContainer {
                                Content = new Image {
                                    Source = iS
                                }
                        }
                    }
                }
            };
        }
    }
}

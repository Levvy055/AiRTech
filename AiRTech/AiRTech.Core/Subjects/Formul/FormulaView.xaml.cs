using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AiRTech.Core.Misc;
using AiRTech.Core.Net;
using Xamarin.Forms;

namespace AiRTech.Core.Subjects.Formul
{
    public partial class FormulaView : ContentView
    {
        private readonly Subject _subject;
        private readonly Formula _fml;

        public FormulaView(Formula fml, Subject subject)
        {
            _subject = subject;
            _fml = fml;
            Title = fml.Title;
            BindingContext = fml;
            InitializeComponent();
            if (fml.Synonyms != null && fml.Synonyms.Length > 0)
            {
                var txt = string.Join(", ", fml.Synonyms);
                var sLab = new Label
                {
                    FormattedText = new FormattedString
                    {
                        Spans =
                            {
                                new Span {Text = "Inaczej: ", FontAttributes = FontAttributes.Bold},
                                new Span {Text = txt} }
                    }
                };
                Sl.Children.Add(sLab);
            }
            var img = new Image
            {
                Source = fml.ImageSource,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Aspect = Aspect.AspectFill
            };
            var tgr = new TapGestureRecognizer()
            {
                Command = new Command(OnImageTap),
                CommandParameter = _fml.ImageSource
            };
            img.GestureRecognizers.Add(tgr);
            Sl.Children.Add(img);
            if (fml.InEqs != null && fml.InEqs.Length > 0)
            {
                CreateInner();
            }
        }

        private async void CreateInner()
        {
            foreach (var eq in _fml.InEqs.Where(id => id != null))
            {
                try
                {
                    Sl.Children.Add(await CreateViewForInnerFormulaComp(eq));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    CoreManager.Current.App.DialogManager.ShowWarningDialog("Błąd w pobranej zawartości",
                        "Nie można utworzyć elementu definicji " + _fml.Title);
                }
            }
        }

        private async Task<StackLayout> CreateViewForInnerFormulaComp(InEq id)
        {
            var stackLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
            stackLayout.Children.Add(new Label
            {
                FormattedText = new FormattedString
                {
                    Spans = { new Span { Text = id.Sign,
                    FontAttributes = FontAttributes.Bold } }
                }
            });
            stackLayout.Children.Add(new Label
            {
                FormattedText = new FormattedString
                {
                    Spans = { new Span
                    {
                        Text = " - "
                    }, new Span
                    {
                        Text = id.Desc
                    } }
                }
            });
            if (!string.IsNullOrWhiteSpace(id.Img))
            {
                var path = Path.Combine(WebCore.FnFmlsDir, WebCore.FnImgDir, id.Img);
                var s = await ImageResourceExtension.GetImageFromUri(path);
                var image = new Image
                {
                    Source = s
                };
                var tgr = new TapGestureRecognizer()
                {
                    Command = new Command(OnImageTap),
                    CommandParameter = s
                };
                image.GestureRecognizers.Add(tgr);
                stackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Children = { stackLayout }
                };
                stackLayout.Children.Add(image);
            }
            return stackLayout;
        }

        private void OnImageTap(object obj)
        {
            if (obj is ImageSource imageSource)
            {
                var mip = new ModalImagePage(imageSource);
                CoreManager.Current.App.NavigateToModal(mip);
            }
        }

        public string Title { get; private set; }
    }
}

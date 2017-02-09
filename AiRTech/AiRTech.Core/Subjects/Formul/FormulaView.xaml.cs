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
            Title = _fml.Title;
            BindingContext = _fml;
            InitializeComponent();
            if (_fml.Synonyms != null && _fml.Synonyms.Length > 0)
            {
                var txt = string.Join(", ", _fml.Synonyms);
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
            if (!string.IsNullOrWhiteSpace(_fml.EqFile))
            {
                var iS = _fml.ImageSource;
                var img = new Image
                {
                    Source = iS,
                    VerticalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Aspect = Aspect.AspectFill
                };
                var tgr = new TapGestureRecognizer()
                {
                    Command = new Command(OnImageTap),
                    CommandParameter = iS
                };
                img.GestureRecognizers.Add(tgr);
                Sl.Children.Add(img);
            }
            if (_fml.InEqs != null && _fml.InEqs.Length > 0)
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
                    var sl = await CreateViewForInnerFormulaComp(eq);
                    Sl.Children.Add(sl);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    CoreManager.Current.App.DialogManager.ShowWarningDialog("Błąd w pobranej zawartości",
                        "Nie można utworzyć elementu wzoru " + _fml.Title);
                }
            }
        }

        private async Task<StackLayout> CreateViewForInnerFormulaComp(InEq id)
        {
            var stackLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
            if (!string.IsNullOrWhiteSpace(id.Sign))
            {
                stackLayout.Children.Add(new Label
                {
                    FormattedText = new FormattedString
                    {
                        Spans = { new Span { Text = id.Sign,
                    FontAttributes = FontAttributes.Bold } }
                    }
                });
            }
            if (!string.IsNullOrWhiteSpace(id.Desc))
            {
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
            }
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

        private static void OnImageTap(object obj)
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

using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace AiRTech.Core.Subjects.Formul
{
    public partial class FormulaView : ContentView
    {
        private readonly Subject _subject;

        public FormulaView(Formula fml, Subject subject)
        {
            _subject = subject;
            Title = fml.Title;
            BindingContext = fml;
            InitializeComponent();
            var img = new Image
            {
                Source = fml.ImageSource,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Aspect = Aspect.AspectFill
            };
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
            Sl.Children.Add(img);
            if (fml.InEqs != null && fml.InEqs.Length > 0)
            {
                foreach (var eq in fml.InEqs.Where(id => id != null))
                {
                    try
                    {
                        Sl.Children.Add(CreateViewForInnerFormulaComp(eq));
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                        CoreManager.Current.App.DialogManager.ShowWarningDialog("Błąd w pobranej zawartości", "Nie można utworzyć elementu definicji " + fml.Title);
                    }
                }
            }
        }

        public string Title { get; private set; }

        private View CreateViewForInnerFormulaComp(InEq id)
        {
            var v = new StackLayout { Orientation = StackOrientation.Horizontal };
            v.Children.Add(new Label
            {
                FormattedText = new FormattedString
                {
                    Spans = { new Span { Text = id.Sign,
                FontAttributes = FontAttributes.Bold } }
                }
            });
            v.Children.Add(new Label
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
            return v;
        }
    }
}

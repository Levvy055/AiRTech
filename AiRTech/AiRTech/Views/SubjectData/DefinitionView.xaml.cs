using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using AiRTech.Core.Subjects;
using AiRTech.Core.Subjects.Def;
using AiRTech.Views.Other;
using AiRTech.Views.ViewComponents;
using Xamarin.Forms;

namespace AiRTech.Views.SubjectData
{
    public partial class DefinitionView : ContentView
    {
        private readonly Subject _subject;

        public DefinitionView(Definition def, Subject subject)
        {
            _subject = subject;
            BindingContext = def;
            InitializeComponent();
            if (def.Inner != null && def.Inner.Length > 0)
            {
                foreach (var id in def.Inner)
                {
                    if (id != null)
                    {
                        try
                        {
                            Sl.Children.Add(CreateViewForInnerDefComp(id));
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e);
                            DialogManager.ShowWarningDialog("Błąd w pobranej zawartości", "Nie można utworzyć elementu definicji " + def.Title);
                        }
                    }
                }
            }
            if (def.Solvers.Count > 0)
            {
                Sl.Children.Add(new Label
                {
                    Text = "Powiązane Solvery",
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold

                });
                foreach (var s in def.Solvers.Where(s => s != null))
                {
                    Sl.Children.Add(CreateButton(s));
                }
            }
        }

        private View CreateViewForInnerDefComp(InDef id)
        {
            var v = new StackLayout();
            if (id.Layout == InDefLayout.TextOverImage && id.Image == null)
            {
                if (!string.IsNullOrWhiteSpace(id.Header))
                {
                    id.Layout = !string.IsNullOrWhiteSpace(id.List) ? InDefLayout.List : InDefLayout.HeaderAndText;
                }
            }
            switch (id.Layout)
            {
                case InDefLayout.TextUnderImage:
                    v.Orientation = StackOrientation.Vertical;
                    v.Children.Add(CreateImage(id));
                    v.Children.Add(CreateText(id, true));
                    break;
                case InDefLayout.TextOverImage:
                    v.Orientation = StackOrientation.Vertical;
                    v.Children.Add(CreateText(id, true));
                    v.Children.Add(CreateImage(id));
                    break;
                case InDefLayout.List:
                    v.Orientation = StackOrientation.Vertical;
                    if (!string.IsNullOrWhiteSpace(id.Header))
                    {
                        v.Children.Add(CreateHeader(id));
                    }
                    v.Children.Add(CreateList(id));
                    break;
                case InDefLayout.HeaderAndText:

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return v;
        }

        private View CreateHeader(InDef id)
        {
            var fs = new FormattedString
            {
                Spans = { new Span { Text = id.Header, FontSize = 16, FontAttributes = FontAttributes.Bold } }
            };
            return new Label { FormattedText = fs };
        }

        private static Label CreateText(InDef id, bool italic = false)
        {
            if (id.Text == null)
            {
                return new Label();
            }
            var fa = italic ? FontAttributes.Italic : FontAttributes.None;
            var ft = new FormattedString
            {
                Spans = { new Span { Text = id.Text, FontAttributes = fa } }
            };
            return new Label
            {
                FormattedText = ft,
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center
            };
        }

        private View CreateList(InDef id)
        {
            var fs = new FormattedString();
            foreach (var str in id.List.Split('|'))
            {
                fs.Spans.Add(new Span { Text = "-> ", FontAttributes = FontAttributes.Bold });
                fs.Spans.Add(new Span { Text = str + Environment.NewLine });
            }
            return new Label { FormattedText = fs };
        }

        private static Image CreateImage(InDef id)
        {
            return new Image
            {
                Source = id.ImageSource,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Aspect = Aspect.AspectFill
            };
        }

        private View CreateButton(SolverView solverView)
        {
            var b = new Button
            {
                Text = solverView.Title,
                Command = SolverButton_Click(solverView)
            };
            return b;
        }

        private ICommand SolverButton_Click(SolverView solverView)
        {
            var c = new Command(() =>
            {
                var app = App.Current as App;
                var np = app.GetPage(typeof(SolverPage), "Podstawy Teorii Sygnałów", _subject) as SolverPage;
                np?.NavigateToTab(solverView);
            });
            return c;
        }
    }
}

﻿using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace AiRTech.Core.Subjects.Def
{
    public partial class DefinitionView : ContentView
    {
        private readonly Subject _subject;
        private readonly Definition _def;

        public DefinitionView(Definition def, Subject subject)
        {
            _def = def;
            _subject = subject;
            Title = def.Title;
            BindingContext = def;
            InitializeComponent();
            if (def.Inner != null && def.Inner.Length > 0)
            {
                foreach (var id in def.Inner)
                {
                    if (id != null)
                    {
                        var v = CreateViewForInnerDefComp(id);
                        if (v != null)
                        {
                            Sl.Children.Add(v);
                        }
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(def.SolverNames))
            {
                var sn = def.SolverNames.Split('|');
                for (var i = 0; i < sn.Length; i++)
                {
                    sn[i] = sn[i].Trim();
                }
                Sl.Children.Add(new Label
                {
                    Text = "Powiązane Solvery",
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.End,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold

                });
                foreach (var s in sn)
                {
                    Sl.Children.Add(CreateButton(s));
                }
            }
        }

        public string Title { get; private set; }

        private View CreateViewForInnerDefComp(InDef id)
        {
            try
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
            catch (Exception e)
            {
                Debug.WriteLine(e);
                CoreManager.Current.App.DialogManager.ShowWarningDialog("Błąd w pobranej zawartości", "Nie można utworzyć elementu definicji " + _def.Title);
            }
            return null;
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

        private View CreateButton(string solverName)
        {
            var b = new Button
            {
                Text = solverName,
                Command = SolverButton_Click(solverName)
            };
            return b;
        }

        private ICommand SolverButton_Click(string solverName)
        {
            var c = new Command(() =>
            {
                CoreManager.Current.App.NavigateToSolverList(_subject, solverName);
            });
            return c;
        }
    }
}
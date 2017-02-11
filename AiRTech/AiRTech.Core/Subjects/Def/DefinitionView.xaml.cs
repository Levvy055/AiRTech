using AiRTech.Core.Misc;
using System;
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
                CreateNavigationToSolvers();
            }
            if (!string.IsNullOrWhiteSpace(def.FormulaNames))
            {
                CreateNavigationToFormulas();
            }
        }

        private View CreateViewForInnerDefComp(InDef id)
        {
            try
            {
                var v = new StackLayout();
                if (id.Layout == InDefLayout.TextOverImage && id.Image == null)
                {
                    if (id.List != null)
                    {
                        id.Layout = InDefLayout.List;
                    }
                    else if (id.OList != null)
                    {
                        id.Layout = InDefLayout.OList;
                    }
                    else if (!string.IsNullOrWhiteSpace(id.Header))
                    {
                        id.Layout = InDefLayout.HeaderAndText;
                    }
                    else
                    {
                        id.Layout = InDefLayout.OnlyText;
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
                        v.Children.Add(CreateHeader(id));
                        v.Children.Add(CreateText(id));
                        break;
                    case InDefLayout.OList:
                        v.Orientation = StackOrientation.Vertical;
                        if (!string.IsNullOrWhiteSpace(id.Header))
                        {
                            v.Children.Add(CreateHeader(id));
                        }
                        v.Children.Add(CreateOList(id));
                        break;
                    case InDefLayout.OnlyText:
                        v.Children.Add(CreateText(id));
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

        private void CreateNavigationToSolvers()
        {
            var sn = _def.SolverNames.Split('|');
            for (var i = 0; i < sn.Length; i++)
            {
                sn[i] = sn[i].Trim();
            }
            Sl.Children.Add(new Label
            {
                Text = "Solvery do definicji",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.End,
                FontSize = 18,
                FontAttributes = FontAttributes.Bold
            });
            foreach (var s in sn)
            {
                Sl.Children.Add(CreateSolverButton(s));
            }
        }

        private void CreateNavigationToFormulas()
        {
            var sn = _def.FormulaNames.Split('|');
            for (var i = 0; i < sn.Length; i++)
            {
                sn[i] = sn[i].Trim();
            }
            Sl.Children.Add(new Label
            {
                Text = "Wzory do definicji",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.End,
                FontSize = 18,
                FontAttributes = FontAttributes.Bold
            });
            foreach (var s in sn)
            {
                Sl.Children.Add(CreateFormulaButton(s));
            }
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
            foreach (var str in id.List)
            {
                fs.Spans.Add(new Span { Text = "-> ", FontAttributes = FontAttributes.Bold });
                fs.Spans.Add(new Span { Text = str + Environment.NewLine });
            }
            return new Label { FormattedText = fs };
        }

        private View CreateOList(InDef id)
        {
            var fs = new FormattedString();
            foreach (var p in id.OList)
            {
                fs.Spans.Add(new Span { Text = p.Key + ": ", FontAttributes = FontAttributes.Bold });
                fs.Spans.Add(new Span { Text = p.Value + Environment.NewLine });
            }
            return new Label { FormattedText = fs };
        }

        private static Image CreateImage(InDef id)
        {
            var iS = id.ImageSource;
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
            return img;
        }

        private static void OnImageTap(object obj)
        {
            if (obj is ImageSource imageSource)
            {
                var mip = new ModalImagePage(imageSource);
                CoreManager.Current.App.NavigateToModal(mip);
            }
        }

        private View CreateSolverButton(string solverName)
        {
            var b = new Button
            {
                Text = solverName,
                Command = SolverButton_Click(solverName)
            };
            return b;
        }

        private View CreateFormulaButton(string formulaName)
        {
            var b = new Button
            {
                Text = formulaName,
                Command = FormulaButton_Click(formulaName)
            };
            return b;
        }

        private ICommand SolverButton_Click(string solverName)
        {
            var c = new Command(() =>
            {
                CoreManager.Current.App.NavigateToSolver(solverName, _subject);
            });
            return c;
        }

        private ICommand FormulaButton_Click(string formulaName)
        {
            var c = new Command(() =>
            {
                CoreManager.Current.App.NavigateToFormula(formulaName, _subject);
            });
            return c;
        }

        public string Title { get; private set; }
    }
}

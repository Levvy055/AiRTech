using System;
using System.Windows.Input;
using AiRTech.Core.Subjects;
using AiRTech.Core.Subjects.Def;
using AiRTech.Views.ViewComponents;
using Xamarin.Forms;

namespace AiRTech.Views.SubjectData
{
    public partial class SDefinition : ContentView
    {
        private readonly Subject _subject;

        public SDefinition(Definition def, Subject subject)
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
                        Sl.Children.Add(CreateViewForInnerDefComp(id));
                    }
                }
            }
            if (def.Solvers.Count > 0)
            {
                foreach (var s in def.Solvers)
                {
                    if (s != null)
                    {
                        Sl.Children.Add(CreateButton(s));
                    }
                }
            }
        }

        private View CreateViewForInnerDefComp(InDef id)
        {
            var v = new StackLayout();
            switch (id.Layout)
            {
                case InDefLayout.TextUnderImage:
                    v.Orientation = StackOrientation.Vertical;
                    v.Children.Add(CreateImage(id));
                    v.Children.Add(CreateLabel(id));
                    break;
                case InDefLayout.TextOverImage:
                    v.Orientation = StackOrientation.Vertical;
                    v.Children.Add(CreateLabel(id));
                    v.Children.Add(CreateImage(id));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return v;
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

        private static Label CreateLabel(InDef id)
        {
            return new Label
            {
                Text = id.Text,
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center
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

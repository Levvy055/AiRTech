using System;
using Xamarin.Forms;

namespace AiRTech.Core.Subjects.Solv.Solvers.Components
{
    public class SvRow : ViewComponent
    {
        private ViewComponent[] _components;
        private double[] _columnsRatio;

        public SvRow(params ViewComponent[] components) : this(null, components)
        {
        }

        public SvRow(string title, params ViewComponent[] components) : base(ViewComponentType.Row)
        {
            var g = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            HasTitle = !string.IsNullOrWhiteSpace(title);
            if (HasTitle)
            {
                g.RowDefinitions.Add(new RowDefinition());
                g.RowDefinitions.Add(new RowDefinition());
                g.Children.Add(new Label { Text = title }, 0, 0);
            }
            Source = g;
            Components = components;
        }

        public bool HasTitle { get; }

        private Grid MGrid => Source as Grid;

        public ViewComponent[] Components
        {
            get
            {
                return _components;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("there was null on input for " + CompType);
                }
                _components = value;
                for (var i = 0; i < _components.Length; i++)
                {
                    MGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    var vc = _components[i];
                    if (vc?.Source != null)
                    {
                        MGrid.Children.Add(vc.Source, i, HasTitle ? 1 : 0);
                    }
                }
            }
        }

        public double[] ColumnsRatio
        {
            get { return _columnsRatio; }
            set
            {
                _columnsRatio = value;
                for (var i = 0; i < MGrid.ColumnDefinitions.Count; i++)
                {
                    var c = MGrid.ColumnDefinitions[i];
                    c.Width = i < _columnsRatio.Length ? new GridLength(_columnsRatio[i], GridUnitType.Star) : GridLength.Auto;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AiRTech.Core.Math.Solvers.Components
{
    public class SvRow : ViewComponent
    {
        private ViewComponent[] _components;


        public SvRow(params ViewComponent[] components) : this(null, components)
        {
        }

        public SvRow(string title, params ViewComponent[] components) : base(ViewComponentType.Row)
        {
            var g = new Grid();
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
                for (var i = _components.Length - 1; i >= 0; i--)
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
    }
}

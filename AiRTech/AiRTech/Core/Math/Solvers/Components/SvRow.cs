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

        public SvRow(params ViewComponent[] components) : base(ViewComponentType.Row)
        {
            Source = new Grid();
            Components = components;
        }

        private Grid MGrid => Source as Grid;

        public ViewComponent[] Components
        {
            get
            {
                return _components;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("there was null on input for " + CompType);
                }
                _components = value;
                MGrid.Children.Clear();
                MGrid.ColumnDefinitions.Clear();
                for (var i = _components.Length - 1; i >= 0; i--)
                {
                    MGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    var vc = _components[i];
                    if (vc != null)
                    {
                        MGrid.Children.Add(vc.Source, i, 0);
                    }
                }
            }
        }
    }
}

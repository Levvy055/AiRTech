using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.Math.Solvers.Components;
using Xamarin.Forms;

namespace AiRTech.Views.ViewComponents
{
    public delegate void ChangedEventHandler(object sender, EventArgs e);

    public partial class SolverView : ContentView
    {
        private ViewComponent[,] _contento;
        private Dictionary<View, Delegate> _listeners;
        public event ChangedEventHandler Changed;

        public SolverView()
        {
            InitializeComponent();
            _listeners = new Dictionary<View, Delegate>();
            MGrid.VerticalOptions = LayoutOptions.Start;
            MGrid.HorizontalOptions = LayoutOptions.FillAndExpand;
        }

        protected virtual void OnChanged(EventArgs e)
        {
            Changed?.Invoke(this, e);
        }

        public ViewComponent[,] Contento
        {
            get { return _contento; }
            set
            {
                if (value == null)
                {
                    throw new InvalidOperationException("Solver Content cannot be assigned to null!");
                }
                MGrid.Children.Clear();
                _contento = value;
                for (var y = 0; y < _contento.GetLength(0); y++)
                {
                    MGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    for (var x = 0; x < _contento.GetLength(1); x++)
                    {
                        if (MGrid.ColumnDefinitions.Count <= x)
                        {
                            MGrid.ColumnDefinitions.Add(new ColumnDefinition());
                        }
                        var v = _contento[y, x];
                        if (v != null)
                        {
                            AddToGrid(v, x, y);
                        }
                    }
                }
                UpdateChildrenLayout();
            }
        }

        private void AddToGrid(ViewComponent v, int x, int y)
        {
            var s = v.Source;
            if (s != null)
            {
                MGrid.Children.Add(s, x, y);
            }
            else
            {
                throw new ArgumentNullException("Null solver component: "+v);
            }
        }

        public delegate void OnEntryChange();
    }
}

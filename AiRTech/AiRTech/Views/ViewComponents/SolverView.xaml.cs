using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AiRTech.Views.ViewComponents
{
    public delegate void ChangedEventHandler(object sender, EventArgs e);

    public partial class SolverView : ContentView
    {
        private View[,] _contento;
        private Dictionary<View, Delegate> _listeners;
        public event ChangedEventHandler Changed;

        public SolverView()
        {
            InitializeComponent();
            _listeners = new Dictionary<View, Delegate>();
        }

        protected virtual void OnChanged(EventArgs e)
        {
            Changed?.Invoke(this, e);
        }

        public View[,] Contento
        {
            get { return _contento; }
            set
            {
                if (value == null)
                {
                    return;
                }
                MGrid.Children.Clear();
                _contento = value;
                for (var x = 0; x < _contento.GetLength(0); x += 1)
                {
                    MGrid.RowDefinitions.Add(new RowDefinition());
                    for (var y = 0; y < _contento.GetLength(1); y += 1)
                    {
                        if (MGrid.ColumnDefinitions.Count <= y)
                        {
                            MGrid.ColumnDefinitions.Add(new ColumnDefinition());
                        }
                        var v = _contento[x, y];
                        if (v == null) { continue; }
                        MGrid.Children.Add(v);
                        Grid.SetColumn(v, y);
                        Grid.SetRow(v, x);
                    }
                }
            }
        }

        public delegate void OnEntryChange();
    }
}

using System;
using AiRTech.Core.Subjects.Solv.Components;
using Xamarin.Forms;

namespace AiRTech.Core.Subjects.Solv
{
    public delegate void ChangedEventHandler(object sender, EventArgs e);

    public partial class SolverView : ContentView
    {
        private ViewComponent[,] _contento;
        private double[] _rowsRatio;
        public event ChangedEventHandler Changed;

        public SolverView(ViewComponent[,] contents)
        {
            InitializeComponent();
            MGrid.VerticalOptions = LayoutOptions.Start;
            MGrid.HorizontalOptions = LayoutOptions.FillAndExpand;
            Contento = contents;
        }

        protected virtual void OnChanged(EventArgs e)
        {
            Changed?.Invoke(this, e);
        }

        private void AddToGrid(ViewComponent v, int x, int y)
        {
            if (v == null)
            {
                return;
            }
            var s = v.Source;
            if (s != null)
            {
                MGrid.Children.Add(s, x, y);
            }
            else
            {
                throw new ArgumentNullException("Null solver component: " + v);
            }
        }

        public string Title { get; set; }
        public ViewComponent[,] Contento
        {
            get { return _contento; }
            private set
            {
                MGrid.Children.Clear();
                if (value == null)
                {
                    _contento = new ViewComponent[,]
                    {
                        {new SvLabel("Not yet implemented!")}
                    };
                }
                else
                {
                    _contento = value;
                }
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
                        AddToGrid(v, x, y);
                    }
                }
                UpdateChildrenLayout();
            }
        }

        public double[] RowsRatio
        {
            get { return _rowsRatio; }
            set
            {
                _rowsRatio = value;
                for (var i = 0; i < MGrid.RowDefinitions.Count; i++)
                {
                    var c = MGrid.RowDefinitions[i];
                    c.Height = i < _rowsRatio.Length ? new GridLength(_rowsRatio[i], GridUnitType.Star) : GridLength.Auto;
                }
            }
        }

        public delegate void OnEntryChange();
    }
}

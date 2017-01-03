using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AiRTech.Views.Other
{
    public class HistResultModal
    {
        private readonly List<double> _cHeaders = new List<double>();
        private readonly List<double> _rHeaders = new List<double>();
        private static readonly Color HEADERS_BG_COLOR = Color.Blue;
        private static readonly Color HEADERS_TXT_COLOR = Color.White;
        private static readonly Color NORMAL_BG_COLOR = Color.Gray;

        public HistResultModal(List<KeyValuePair<Tuple<double, double>, double>> hist)
        {
            Hist = hist;
            MGrid = new Grid();
            MGrid.RowDefinitions.Add(new RowDefinition());
            MGrid.ColumnDefinitions.Add(new ColumnDefinition());
            CalibrateRowsAndColumns(hist.Count);
            ParseHeaders();
            MakeHeaderRow();
            MakeTable();
            Page = new ContentPage
            {
                Content = MGrid,
                BackgroundColor = Color.Black
            };
        }

        public void Show()
        {
            var app = Application.Current as App;
            app?.NavigateToModal(Page);
        }

        private void CalibrateRowsAndColumns(int count)
        {
            while (MGrid.RowDefinitions.Count <= count)
            {
                MGrid.RowDefinitions.Add(new RowDefinition());
            }
            while (MGrid.ColumnDefinitions.Count <= count)
            {
                MGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        private void ParseHeaders()
        {
            foreach (var t in Hist)
            {
                if (!_cHeaders.Contains(t.Key.Item1))
                {
                    _cHeaders.Add(t.Key.Item1);
                }
                if (!_rHeaders.Contains(t.Key.Item2))
                {
                    _rHeaders.Add(t.Key.Item2);
                }
            }
        }

        private void MakeHeaderRow()
        {
            for (var i = 0; i < _cHeaders.Count; i++)
            {
                var l = CreateHeaderCell(_cHeaders[i].ToString());
                MGrid.Children.Add(l, i + 1, 0);
            }
        }

        private void MakeTable()
        {
            for (var i = 1; i <= _rHeaders.Count; i++)
            {
                var l = CreateHeaderCell(_rHeaders[i - 1].ToString());
                MGrid.Children.Add(l, 0, i);
                for (var j = 0; j < _cHeaders.Count; j++)
                {
                    var v = Hist.Find(pair => pair.Key.Item1.Equals(_cHeaders[j])
                                    && pair.Key.Item2.Equals(_rHeaders[i - 1])).Value;
                    var lb = CreateNormalCell(v);
                    MGrid.Children.Add(lb, j + 1, i);
                }
            }
        }

        private static Label CreateHeaderCell(string txt)
        {
            var l = new Label
            {
                Text = txt,
                BackgroundColor = HEADERS_BG_COLOR,
                TextColor = HEADERS_TXT_COLOR,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            return l;
        }

        private static Label CreateNormalCell(double v)
        {
            var lb = new Label
            {
                Text = v.ToString(),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                BackgroundColor = NORMAL_BG_COLOR
            };
            return lb;
        }

        private List<KeyValuePair<Tuple<double, double>, double>> Hist { get; }
        private ContentPage Page { get; }
        private Grid MGrid { get; }
    }
}

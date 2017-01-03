﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AiRTech.Core.Math.Solvers.Components
{
    public class SvGrid : ViewComponent
    {
        public SvGrid(string name = null, IDictionary<string, ViewComponent> uc = null) : base(ViewComponentType.Grid, name)
        {
            var g = new Grid
            {
                MinimumWidthRequest = 30,
                MinimumHeightRequest = 30
            };
            Source = g;
            uc?.Add(name, this);
        }

        public void SetGridComponents(View[,] cmps, bool reset = false)
        {
            if (reset)
            {
                MGrid.Children.Clear();
            }
            if (cmps == null)
            {
                return;
            }
            var sx = cmps.GetLength(0);
            var sy = cmps.GetLength(1);
            for (var i = 0; i < sx; i++)
            {
                for (var j = 0; j < sy; j++)
                {
                    MGrid.Children.Add(cmps[i, j], i, j);
                }
            }
        }

        public void ResetGridToSqEntry(int size)
        {
            Entries = new Entry[size, size];
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    Entries[i, j] = new Entry { Text = "0" };
                }
            }
            SetGridComponents(Entries, true);
        }

        public Entry[,] Entries { get; private set; }

        public Grid MGrid => (Grid)Source;
    }
}

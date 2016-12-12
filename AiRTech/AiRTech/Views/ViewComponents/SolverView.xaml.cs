using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AiRTech.Views.ViewComponents
{
    public partial class SolverView : ContentView
    {
        public SolverView()
        {
            InitializeComponent();
            var f= new Grid();
            Content = f;
        }
    }
}

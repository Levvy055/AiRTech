using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.Subjects;
using Xamarin.Forms;

namespace AiRTech.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            listView.ItemsSource = Subject.Subjects.Values;
        }
    }
}

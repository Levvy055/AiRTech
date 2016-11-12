using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.Subjects;
using Xamarin.Forms;

namespace AiRTech.Views
{
    public partial class SubjectsPage : ContentPage
    {
        public SubjectsPage()
        {
            InitializeComponent();
            listView.ItemsSource = Subject.Subjects.Values;
        }
    }
}

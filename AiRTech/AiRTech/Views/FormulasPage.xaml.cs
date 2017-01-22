using System.Collections.Generic;
using AiRTech.Core.Subjects;
using AiRTech.Views.ViewModels;
using Xamarin.Forms;

namespace AiRTech.Views
{
    public partial class FormulasPage : ContentPage
    {

        public FormulasPage(Subject subject)
        {
            Subject = subject;
            BindingContext = new FurmulasViewModel(this);
            InitializeComponent();
        }

        public Subject Subject { get; set; }
        public ListView FmlListView => MList;
    }
}

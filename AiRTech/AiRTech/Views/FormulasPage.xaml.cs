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
            var fmlsVm= new FurmulasViewModel(this);
            BindingContext = fmlsVm;
            InitializeComponent();
            Subject.Base.Sort();
            FmlListView.ItemSelected += fmlsVm.MlistOnItemSelected;
        }

        public Subject Subject { get; set; }
        public Dictionary<string, ContentPage> FmlViews { get; } = new Dictionary<string, ContentPage>();
        public ListView FmlListView => Mlist;
        public View NoFmlsView => NoFmlsLabel;
    }
}

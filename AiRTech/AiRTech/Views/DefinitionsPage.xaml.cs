using System.Collections.Generic;
using AiRTech.Core.Subjects;
using AiRTech.Views.ViewModels;
using Xamarin.Forms;

namespace AiRTech.Views
{
    public partial class DefinitionsPage : ContentPage
    {

        public DefinitionsPage(Subject subject)
        {
            Subject = subject;
            var defVm = new DefinitionsViewModel(this);
            BindingContext = defVm;
            InitializeComponent();
            Subject.Base.Sort();
            DefListView.ItemSelected += defVm.MlistOnItemSelected;
        }

        public Subject Subject { get; set; }
        public Dictionary<string, ContentPage> DefViews { get; } = new Dictionary<string, ContentPage>();
        public ListView DefListView => Mlist;
        public View NoDefsView => NoDefsLabel;
    }
}

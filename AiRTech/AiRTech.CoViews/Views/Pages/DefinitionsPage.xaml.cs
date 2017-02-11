using System.Collections.Generic;
using AiRTech.Core;
using AiRTech.Core.Misc;
using AiRTech.Core.Subjects;
using AiRTech.Core.Subjects.Def;
using AiRTech.Core.Subjects.Formul;
using AiRTech.Views.ViewModels;
using Xamarin.Forms;

namespace AiRTech.Views.Pages
{
    public partial class DefinitionsPage : ContentPage
    {

        public DefinitionsPage(Subject subject)
        {
            Subject = subject;
            ViewModel = new DefinitionsViewModel(this);
            BindingContext = ViewModel;
            InitializeComponent();
            Subject.Base.Sort();
            DefListView.ItemSelected += ViewModel.MlistOnItemSelected;
        }

        public void NavigateToDefinition(string name)
        {
            Subject.Base.LoadDefinitions();
            ViewModel.Update();
            foreach (var dnc in DefListView.ItemsSource)
            {
                var d = dnc as Definition;
                if (d != null && d.Title == name)
                {
                    DefListView.SelectedItem = d;
                    return;
                }
            }
            CoreManager.Current.App.DialogManager.ShowWarningDialog("Brak definicji!", "Brak podanej definicji: " + name);
        }

        public Subject Subject { get; set; }
        public Dictionary<string, ContentPage> DefViews { get; } = new Dictionary<string, ContentPage>();
        public ListView DefListView => Mlist;
        public View NoDefsView => NoDefsLabel;

        public DefinitionsViewModel ViewModel { get; set; }
    }
}

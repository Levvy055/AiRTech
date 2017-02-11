using System.Collections.Generic;
using AiRTech.Core.Subjects;
using AiRTech.Core.Subjects.Formul;
using AiRTech.Views.ViewModels;
using Xamarin.Forms;
using AiRTech.Views.Other;
using AiRTech.Core;

namespace AiRTech.Views.Pages
{
    public partial class FormulasPage : ContentPage
    {

        public FormulasPage(Subject subject)
        {
            Subject = subject;
            ViewModel = new FormulasViewModel(this);
            BindingContext = ViewModel;
            InitializeComponent();
            Subject.Base.Sort();
            FmlListView.ItemSelected += ViewModel.MlistOnItemSelected;
        }

        public void NavigateToFormula(string name)
        {
            Subject.Base.LoadFormulas();
            ViewModel.Update();
            foreach (var fnc in FmlListView.ItemsSource)
            {
                var f = fnc as Formula;
                if (f != null && f.Title == name)
                {
                    FmlListView.SelectedItem = f;
                    return;
                }
            }
            CoreManager.Current.App.DialogManager.ShowWarningDialog("Brak wzoru!", "Brak podanego wzoru: " + name);
        }

        public Subject Subject { get; set; }
        public Dictionary<string, ContentPage> FmlViews { get; } = new Dictionary<string, ContentPage>();
        public ListView FmlListView => Mlist;
        public FormulasViewModel ViewModel { get; set; }
        public View NoFmlsView => NoFmlsLabel;
    }
}

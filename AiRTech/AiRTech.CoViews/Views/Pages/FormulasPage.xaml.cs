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
            var fmlsVm = new FurmulasViewModel(this);
            BindingContext = fmlsVm;
            InitializeComponent();
            Subject.Base.Sort();
            FmlListView.ItemSelected += fmlsVm.MlistOnItemSelected;
        }

        public async void NavigateToFormula(string name)
        {
            await Subject.Base.LoadFormulas();
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
        public View NoFmlsView => NoFmlsLabel;
    }
}

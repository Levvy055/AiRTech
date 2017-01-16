using System;
using System.Collections.Generic;
using AiRTech.Core.Subjects;
using AiRTech.Core.Subjects.Def;
using AiRTech.Views.SubjectData;
using AiRTech.Views.ViewModels;
using Xamarin.Forms;

namespace AiRTech.Views
{
    public partial class DefinitionsPage : ContentPage
    {

        public DefinitionsPage(Subject subject)
        {
            Subject = subject;
            BindingContext = new DefinitionsViewModel(this);
            InitializeComponent();
            var defs = subject.Base.Definitions;
            if (defs != null && defs.Count > 0)
            {
                Mlist.ItemSelected += MlistOnItemSelected;
                foreach (var def in defs)
                {
                    var sd = new SDefinition(def, subject);
                    var sdp = new ContentPage {Title = def.Title, Content = sd};
                    DefViews.Add(def.Title, sdp);
                }
            }
            else
            {
                Mlist.IsVisible = false;
            }
        }

        private void MlistOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var d = Mlist.SelectedItem as Definition;
            if (d == null) { return; }
            var app = Application.Current as App;
            app.NavigateToModal(DefViews[d.Title]);
        }

        public Subject Subject { get; set; }
        public Dictionary<string, ContentPage> DefViews { get; } = new Dictionary<string, ContentPage>();
    }
}

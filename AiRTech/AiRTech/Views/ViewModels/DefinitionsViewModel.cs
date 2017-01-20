using System.Collections.Generic;
using System.ComponentModel;
using AiRTech.Core.Subjects.Def;
using AiRTech.Views.SubjectData;
using Xamarin.Forms;

namespace AiRTech.Views.ViewModels
{
    public class DefinitionsViewModel : ViewModelBase
    {
        public DefinitionsViewModel(DefinitionsPage page) : base(page)
        {
            Title = "Definicje";
            Subject.Base.PropertyChanged += (sender, args) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    SubjectOnPropertyChanged(sender, args);
                });
            };
        }

        private void SubjectOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var p = Page as DefinitionsPage;
            if (args.PropertyName == nameof(Definitions) && p?.DefListView != null)
            {
                var defs = Definitions.ToArray();
                p.DefListView.ItemsSource = defs;
                if (defs != null && defs.Length > 0)
                {
                    foreach (var def in defs)
                    {
                        if (!p.DefViews.ContainsKey(def.Title))
                        {
                            var sd = new SDefinition(def, Subject);
                            var sdp = new ContentPage { Title = def.Title, Content = sd };
                            p.DefViews.Add(def.Title, sdp);
                        }
                    }
                }
            }
        }

        public void MlistOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var p = Page as DefinitionsPage;
            var d = p.DefListView.SelectedItem as Definition;
            if (d == null) { return; }
            var app = Application.Current as App;
            app.NavigateToModal(p.DefViews[d.Title]);
            p.DefListView.SelectedItem = -1;
        }

        public List<Definition> Definitions => Subject.Base.Definitions;
    }
}

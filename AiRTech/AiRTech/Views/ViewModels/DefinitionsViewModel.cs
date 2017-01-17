using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            page.Subject.Base.PropertyChanged += SubjectOnPropertyChanged;
        }

        private void SubjectOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var p = Page as DefinitionsPage;
            if (args.PropertyName == nameof(Definitions) && p?.DefListView != null)
            {
                p.DefListView.ItemsSource = Definitions;
                if (Definitions != null && Definitions.Count > 0)
                {
                    p.DefListView.ItemSelected += MlistOnItemSelected;
                    foreach (var def in Definitions)
                    {
                        if (!p.DefViews.ContainsKey(def.Title))
                        {
                            var sd = new SDefinition(def, Subject);
                            var sdp = new ContentPage {Title = def.Title, Content = sd};
                            p.DefViews.Add(def.Title, sdp);
                        }
                    }
                }
            }
        }

        private void MlistOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var p = Page as DefinitionsPage;
            var d = p.DefListView.SelectedItem as Definition;
            if (d == null) { return; }
            var app = Application.Current as App;
            app.NavigateToModal(p.DefViews[d.Title]);
            p.DefListView.SelectedItem = -1;
        }

        public ObservableCollection<Definition> Definitions => Subject.Base.Definitions;
    }
}

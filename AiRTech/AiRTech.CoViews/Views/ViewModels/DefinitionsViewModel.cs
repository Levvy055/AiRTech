using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using AiRTech.Core;
using AiRTech.Core.Subjects.Def;
using AiRTech.Views.Pages;
using Xamarin.Forms;

namespace AiRTech.Views.ViewModels
{
    public class DefinitionsViewModel : ViewModelBase
    {
        private View _noDefsView;

        public DefinitionsViewModel(DefinitionsPage page) : base(page)
        {
            IsBusy = true;
            Title = "Definicje";
            NoDefs = "Brak definicji";
            Subject.Base.PropertyChanged += (sender, args) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsBusy = true;
                    SubjectOnPropertyChanged(sender, args);
                    IsBusy = false;
                });
            };
            IsBusy = false;
        }

        private void SubjectOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            IsBusy = true;
            if (args.PropertyName == nameof(Definitions) && DefPage?.DefListView != null)
            {
                Update();
            }
            IsBusy = false;
        }

        public void Update()
        {
            var defs = Definitions.ToArray();
            DefPage.DefListView.ItemsSource = defs;
            if (defs != null && defs.Length > 0)
            {
                DefPage.DefListView.IsVisible = true;
                DefPage.NoDefsView.IsVisible = false;
                foreach (var def in defs)
                {
                    if (!DefPage.DefViews.ContainsKey(def.Title))
                    {
                        var sd = new DefinitionView(def, Subject);
                        var sdp = new ContentPage {Title = def.Title, Content = sd};
                        DefPage.DefViews.Add(def.Title, sdp);
                    }
                }
            }
            else
            {
                DefPage.DefListView.IsVisible = false;
                DefPage.NoDefsView.IsVisible = true;
            }
        }

        public void MlistOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            IsBusy = true;
            var d = DefPage.DefListView.SelectedItem as Definition;
            if (d == null)
            {
                IsBusy = false;
                return;
            }
            try
            {
                var view = DefPage.DefViews[d.Title];
                CoreManager.Current.App.NavigateToModal(view);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Err: " + e);
            }
            DefPage.DefListView.SelectedItem = -1;
            IsBusy = false;
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(o =>
                {
                    if (!IsBusy)
                    {
                        IsBusy = true;
                        DefPage.DefViews.Clear();
                        Subject.Base.LoadDefinitionsFromServerAndSave();
                        IsBusy = false;
                        CoreManager.Current.App.NavigateBack();
                        CoreManager.Current.App.NavigateToDefinitionList(DefPage.Subject);
                    }
                });
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new Command(o =>
                {
                    Subject.Base.SearchDefinition();
                });
            }
        }

        public List<Definition> Definitions => Subject.Base.Definitions;
        public string NoDefs { get; set; }
        public DefinitionsPage DefPage => Page as DefinitionsPage;
    }
}

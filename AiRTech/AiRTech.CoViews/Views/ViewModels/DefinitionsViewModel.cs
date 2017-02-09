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
            Page.IsBusy = true;
            Title = "Definicje";
            NoDefs = "Brak definicji";
            Subject.Base.PropertyChanged += (sender, args) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    SubjectOnPropertyChanged(sender, args);
                });
            };
            Page.IsBusy = false;
        }

        private void SubjectOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            Page.IsBusy = true;
            if (args.PropertyName == nameof(Definitions) && DefPage?.DefListView != null)
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
                            var sdp = new ContentPage { Title = def.Title, Content = sd };
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
            Page.IsBusy = false;
        }

        public void MlistOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            Page.IsBusy = true;
            var d = DefPage.DefListView.SelectedItem as Definition;
            if (d == null)
            {
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
            Page.IsBusy = false;
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(o =>
                {
                    if (!Page.IsBusy)
                    {
                        Page.IsBusy = true;
                        DefPage.DefViews.Clear();
                        Subject.Base.LoadDefinitionsFromServerAndSave();
                        Page.IsBusy = false;
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

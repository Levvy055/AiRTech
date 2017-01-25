﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using AiRTech.Core.Subjects.Def;
using Xamarin.Forms;

namespace AiRTech.Views.ViewModels
{
    public class DefinitionsViewModel : ViewModelBase
    {
        private View _noDefsView;

        public DefinitionsViewModel(Pages.DefinitionsPage page) : base(page)
        {
            Title = "Definicje";
            NoDefs = "Brak definicji";
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
            var p = Page as Pages.DefinitionsPage;
            if (args.PropertyName == nameof(Definitions) && p?.DefListView != null)
            {
                var defs = Definitions.ToArray();
                p.DefListView.ItemsSource = defs;
                if (defs != null && defs.Length > 0)
                {
                    p.DefListView.IsVisible = true;
                    p.NoDefsView.IsVisible = false;
                    foreach (var def in defs)
                    {
                        if (!p.DefViews.ContainsKey(def.Title))
                        {
                            var sd = new DefinitionView(def, Subject);
                            var sdp = new ContentPage { Title = def.Title, Content = sd };
                            p.DefViews.Add(def.Title, sdp);
                        }
                    }
                }
                else
                {
                    p.DefListView.IsVisible = false;
                    p.NoDefsView.IsVisible = true;
                }
            }
        }

        public void MlistOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var p = Page as Pages.DefinitionsPage;
            var d = p.DefListView.SelectedItem as Definition;
            if (d == null)
            {
                return;
            }
            try
            {
                var app = Application.Current as App;
                var view = p.DefViews[d.Title];
                app.NavigateToModal(view);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Err: " + e);
            }
            p.DefListView.SelectedItem = -1;
        }

        public List<Definition> Definitions => Subject.Base.Definitions;
        public string NoDefs { get; set; }
    }
}

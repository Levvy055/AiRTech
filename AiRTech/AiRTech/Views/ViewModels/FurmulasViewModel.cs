using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.Subjects.Formul;
using Xamarin.Forms;

namespace AiRTech.Views.ViewModels
{
    public class FurmulasViewModel : ViewModelBase
    {
        public FurmulasViewModel(Pages.FormulasPage page) : base(page)
        {
            Title = "Wzory";
            NoFormula = "Brak wzorów";
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
            var p = Page as Pages.FormulasPage;
            if (args.PropertyName == nameof(Formulas) && p?.FmlListView != null)
            {
                var fmls = Formulas.ToArray();
                p.FmlListView.ItemsSource = fmls;
                if (fmls != null && fmls.Length > 0)
                {
                    p.FmlListView.IsVisible = true;
                    p.NoFmlsView.IsVisible = false;
                    foreach (var def in fmls)
                    {
                        if (!p.FmlViews.ContainsKey(def.Title))
                        {
                            var sd = new FormulaView(def, Subject);
                            var sdp = new ContentPage { Title = def.Title, Content = sd };
                            p.FmlViews.Add(def.Title, sdp);
                        }
                    }
                }
                else
                {
                    p.FmlListView.IsVisible = false;
                    p.NoFmlsView.IsVisible = true;
                }
            }
        }

        public string NoFormula { get; set; }
        public List<Formula> Formulas => Subject.Base.Formulas;

        public void MlistOnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var p = Page as Pages.FormulasPage;
            var d = p.FmlListView.SelectedItem as Formula;
            if (d == null)
            {
                return;
            }
            try
            {
                var app = Application.Current as App;
                var view = p.FmlViews[d.Title];
                app.NavigateToModal(view);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Err: " + e);
            }
            p.FmlListView.SelectedItem = -1;
        }
    }
}

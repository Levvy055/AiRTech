using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AiRTech.Core;
using AiRTech.Core.Subjects.Formul;
using AiRTech.Views.Pages;
using Xamarin.Forms;

namespace AiRTech.Views.ViewModels
{
    public class FormulasViewModel : ViewModelBase
    {
        public FormulasViewModel(FormulasPage page) : base(page)
        {
            IsBusy = true;
            Title = "Wzory";
            NoFormula = "Brak wzorów";
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
            if (args.PropertyName == nameof(Formulas) && FmlPage?.FmlListView != null)
            {
                Update();
            }
        }

        public  void Update()
        {
            var fmls = Formulas.ToArray();
            FmlPage.FmlListView.ItemsSource = fmls;
            if (fmls != null && fmls.Length > 0)
            {
                FmlPage.FmlListView.IsVisible = true;
                FmlPage.NoFmlsView.IsVisible = false;
                foreach (var def in fmls)
                {
                    if (!FmlPage.FmlViews.ContainsKey(def.Title))
                    {
                        var sd = new FormulaView(def, Subject);
                        var sdp = new ContentPage {Title = def.Title, Content = sd};
                        FmlPage.FmlViews.Add(def.Title, sdp);
                    }
                }
            }
            else
            {
                FmlPage.FmlListView.IsVisible = false;
                FmlPage.NoFmlsView.IsVisible = true;
            }
        }

        public void MlistOnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            IsBusy = true;
            var d = FmlPage.FmlListView.SelectedItem as Formula;
            if (d == null)
            {
                IsBusy = false;
                return;
            }
            if (FmlPage.FmlViews.ContainsKey(d.Title))
            {
                var view = FmlPage.FmlViews[d.Title];
                CoreManager.Current.App.NavigateToModal(view);
            }
            FmlPage.FmlListView.SelectedItem = -1;
            IsBusy = false;
        }

        public ICommand SearchCommand
        {
            get
            {
                return new Command(o =>
                {
                    Subject.Base.SearchFormula();

                });
            }
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
                        FmlPage.FmlViews.Clear();
                        Subject.Base.LoadFormulasFromServerAndSave();
                        IsBusy = false;
                    }
                });
            }
        }
        public string NoFormula { get; set; }
        public List<Formula> Formulas => Subject.Base.Formulas;
        public FormulasPage FmlPage => Page as FormulasPage;
    }
}

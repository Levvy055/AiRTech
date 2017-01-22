using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.Subjects.Formula;
using Xamarin.Forms;

namespace AiRTech.Views.ViewModels
{
    public class FurmulasViewModel : ViewModelBase
    {
        public FurmulasViewModel(FormulasPage page) : base(page)
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
            var p = Page as FormulasPage;
            if (args.PropertyName == nameof(Formulas) && p?.FmlListView != null)
            {

            }
            throw new NotImplementedException();
        }

        public string NoFormula { get; set; }
        public List<Formula> Formulas => Subject.Base.Formulas;
    }
}

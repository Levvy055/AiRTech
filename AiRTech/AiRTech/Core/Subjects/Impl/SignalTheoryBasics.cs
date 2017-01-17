using System.Threading.Tasks;
using AiRTech.Core.Subjects.Def;
using AiRTech.Core.Subjects.Solv.Solvers;
using AiRTech.Core.Web;
using AiRTech.Views.SubjectData;
using Xamarin.Forms;

namespace AiRTech.Core.Subjects.Impl
{
    public class SignalTheoryBasics : SubjectBase
    {
        private Definition _hist;

        public SignalTheoryBasics() : base(SubjectType.PODSTAWY_TEORII_SYGNALOW)
        {
            GetDefinitions();
        }

        private async void GetDefinitions()
        {
            var app = Application.Current as App;
            var defList = await app.Web.GetDefinitionList(SubjectType);
            if (defList != null)
            {
                foreach (var def in defList)
                {
                    Definitions.Add(def);
                }
            }
        }

        protected override void UpdateDependencies()
        {

        }

        public Definition Histogram
        {
            get
            {
                var solver = Solver as SignalTheoryBasicsSolver;
                return _hist ?? (_hist = new Definition
                {
                    Title = "Histogram",
                    Desc = "Zestawienie danych statystycznych w postaci wykresu powierzchniowego " +
                           "złożonego z przylegających do siebie słupków (prostokątów), " +
                           "których wysokość ilustruje liczebność występowania badanej cechy " +
                           "w populacji lub jej próbie.",
                    Inner =
                    {
                        new InDef {Image = "hist_s1.png", Text = "Przykład histogramu"},
                        new InDef {Image = "hist_s2.png", Text = "Histogram 2D"}
                    },
                    Solvers = { solver.HistogramView }
                });
            }
        }
    }
}
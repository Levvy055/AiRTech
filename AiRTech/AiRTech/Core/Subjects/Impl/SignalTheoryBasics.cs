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

        public SignalTheoryBasics() : base(SubjectType.PODSTAWY_TEORII_SYGNALOW)
        {
            UpdateDependencies();
            GetDefinitions();
        }

        private async void GetDefinitions()
        {
            var app = Application.Current as App;
            var newDefList = await app.Web.GetDefinitionList(SubjectType);
            var defList = app.Database.UpdateDefinitions(newDefList);
            foreach (var def in defList)
            {
                Definitions.Add(def);
            }
        }

        protected sealed override void UpdateDependencies()
        {
            var app = Application.Current as App;
            var defList = app.Database.GetAllDefinitions();
            if (defList != null)
            {
                foreach (var def in defList)
                {
                    Definitions.Add(def);
                }
            }
        }
    }
}
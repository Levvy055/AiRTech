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
            LoadDefinitionsFromFile().ContinueWith(task =>
            {
                LoadDefinitionsFromServer();
            });
        }

        protected sealed override void UpdateDependencies()
        {

        }
    }
}
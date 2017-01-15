using AiRTech.Core.Subjects.Def;

namespace AiRTech.Core.Subjects.Impl
{
    public class SignalTheoryBasics : SubjectBase
    {
        private readonly Definition _t = new Definition { Title = "Test", Desc = "more less" };

        public SignalTheoryBasics() : base(SubjectType.PODSTAWY_TEORII_SYGNALOW)
        {
            Definitions.Add(_t);
        }

        protected override void UpdateDependencies()
        {
        }
    }
}
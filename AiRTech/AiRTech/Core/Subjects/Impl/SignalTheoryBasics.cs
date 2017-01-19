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
using System.Collections.Generic;
using AiRTech.Core.SubjectData;

namespace AiRTech.Core.DataHandling
{
    public interface IDbHandler
    {
        void Init();
        bool Exists(SDefinition definition);
        bool Exists(SFormula formula);
        bool Add(SDefinition definition);
        bool Add(SFormula formula);
        bool Update(SDefinition definition);
        bool Update(SFormula formula);
        bool RemoveAllExcept(List<SDefinition> definitions);
        bool RemoveAllExcept(List<SFormula> formulas);
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.SubjectData;
using SQLite;
using Xamarin.Forms;

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

        SQLiteConnection DbConnection();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.SubjectData;
using SQLite;
using Xamarin.Forms;

namespace AiRTech.Core.DataHandling
{
    public class DbHandler : IDbHandler
    {
        public void Init()
        {
            using (var db = DbConnection())
            {
                db.CreateTable<SDefinition>();
                db.CreateTable<SFormula>();
            }
        }

        public bool Exists(SDefinition definition)
        {
            throw new NotImplementedException();
        }

        public bool Exists(SFormula formula)
        {
            throw new NotImplementedException();
        }

        public bool Add(SDefinition definition)
        {
            throw new NotImplementedException();
        }

        public bool Add(SFormula formula)
        {
            throw new NotImplementedException();
        }

        public bool Update(SDefinition definition)
        {
            throw new NotImplementedException();
        }

        public bool Update(SFormula formula)
        {
            throw new NotImplementedException();
        }

        public bool RemoveAllExcept(List<SDefinition> definitions)
        {
            throw new NotImplementedException();
        }

        public bool RemoveAllExcept(List<SFormula> formulas)
        {
            throw new NotImplementedException();
        }

        public SQLiteConnection DbConnection()
        {
            return DependencyService.Get<IFileHandler>().GetDatabaseConnection();
        }
    }
}

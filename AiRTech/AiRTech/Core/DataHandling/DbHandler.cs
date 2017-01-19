using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AiRTech.Core.Subjects;
using AiRTech.Core.Subjects.Def;
using AiRTech.Views.SubjectData;
using SQLite;
using Xamarin.Forms;

namespace AiRTech.Core.DataHandling
{
    public class DbHandler
    {
        private string _path;

        public DbHandler()
        {
            using (var db = Connection)
            {
                db.CreateTable<InDef>();
                db.CreateTable<Definition>();
                //db.CreateTable<Formula>();
            }
        }

        public bool Exists(Definition def)
        {
            using (var db = Connection)
            {
                var i = (from dbdef in db.Table<Definition>() where dbdef.Equals(def, false) select dbdef).FirstOrDefault();
                return i != null;
            }
        }

        private bool Exists(InDef idef)
        {
            using (var db = Connection)
            {
                var l = db.Table<InDef>().ToList();
                var i = db.Table<InDef>().Where(dbidef => idef.Equals(dbidef, false)).FirstOrDefault();
                return i != null;
            }
        }

        public bool Exists(SFormula formula)
        {
            throw new NotImplementedException();
        }

        private Definition GetDefinition(int id)
        {
            using (var db = Connection)
            {
                var definition = db.Table<Definition>().First(def => def.ID == id);
                SubjectType st;
                if (definition == null || !Enum.TryParse(definition.SubjectName, out st))
                {
                    return null;
                }
                definition.LinkDeserializedComponents(st);
                return definition;
            }
        }

        public bool Add(Definition definition)
        {
            using (var db = Connection)
            {
                return db.Insert(definition) > 0;
            }
        }

        private bool Add(InDef idef)
        {
            using (var db = Connection)
            {
                return db.Insert(idef) > 0;
            }
        }

        public bool Add(SFormula formula)
        {
            throw new NotImplementedException();
        }

        public void Update(Definition definition)
        {
            var oldDef = GetDefinition(definition.ID);
            definition.ID = oldDef.ID;

            using (var db = Connection)
            {
                db.Update(definition);
            }
        }

        private void Update(InDef idef)
        {
            var oldDef = GetDefinition(idef.ID);
            idef.ID = oldDef.ID;

            using (var db = Connection)
            {
                db.Update(idef);
            }
        }

        public void Update(SFormula formula)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Definition> UpdateDefinitions(IEnumerable<Definition> definitions)
        {
            foreach (var def in definitions.Where(def => def != null))
            {
                if (def.Inner != null && def.Inner.Length > 0)
                {
                    foreach (var idef in def.Inner)
                    {
                        if (!Exists(idef))
                        {
                            Add(idef);
                        }
                        else
                        {
                            Update(idef);
                        }
                    }
                }
                if (!Exists(def))
                {
                    Add(def);
                }
                else
                {
                    Update(def);
                }
            }
            RemoveAllExcept(definitions);
            return GetAllDefinitions();
        }

        public IEnumerable<Definition> GetAllDefinitions()
        {
            using (var db = Connection)
            {
                var defs = db.Table<Definition>().ToList();
                foreach (var def in defs)
                {
                    var idefList = (from idef in db.Table<InDef>() where idef.DefinitionId == def.ID select idef).ToArray();
                    if (idefList != null && idefList.Length > 0)
                    {
                        def.Inner = idefList;
                    }
                    SubjectType st;
                    if(Enum.TryParse(def.SubjectName, out st))
                    {
                        def.LinkDeserializedComponents(st);
                    }
                }
                return defs;
            }
        }
        
        public void RemoveAllExcept(IEnumerable<Definition> exceptDefinitions)
        {
            var lAll = GetAllDefinitions();
            foreach (var definition in lAll)
            {
                if (!exceptDefinitions.Contains(definition))
                {
                    Remove(definition);
                }
            }
        }

        private void Remove(Definition definition)
        {
            using (var db = Connection)
            {
                db.Table<Definition>().Delete(def => def.ID == definition.ID);
            }
        }

        public bool RemoveAllExcept(IEnumerable<SFormula> formulas)
        {
            throw new NotImplementedException();
        }

        public SQLiteConnection Connection
        {
            get
            {
                if (_path == null)
                {
                    _path = DependencyService.Get<IFileHandler>().GetDatabaseFilePath();
                }
                return new SQLiteConnection(_path);
            }
        }
    }
}

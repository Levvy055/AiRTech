using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AiRTech.Core.Subjects.Def;
using AiRTech.Core.Subjects.Formul;
using AiRTech.Core.Subjects.Solv;
using AiRTech.Properties;
using Xamarin.Forms;

namespace AiRTech.Core.Subjects
{
    public abstract class SubjectBase : INotifyPropertyChanged
    {
        protected SubjectType SubjectType;
        protected SubjectBase(SubjectType subjectType)
        {
            SubjectType = subjectType;
            Definitions = new List<Definition>();
            Formulas = new List<Formula>();
            Solver = Solver.GetSolverFor(SubjectType);
            PropertyChanged += (sender, args) => UpdateDependencies();
        }

        public void LoadDefinitions()
        {
            LoadDefinitionsFromFile().ContinueWith(task =>
            {
                LoadDefinitionsFromServerAndSave();
            });
        }

        public void LoadFormulas()
        {
            LoadFormulasFromFile().ContinueWith(task =>
            {
                LoadFormulasFromServerAndSave();
            });
        }

        protected abstract void UpdateDependencies();

        protected async Task LoadDefinitionsFromFile()
        {
            try
            {
                var app = Application.Current as App;
                var defList = await app.FileHandler.GetDefinitions(SubjectType);
                if (defList != null)
                {
                    Definitions.Clear();
                    foreach (var def in defList)
                    {
                        Definitions.Add(def);
                    }
                    OnPropertyChanged(nameof(Definitions));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        protected async void LoadDefinitionsFromServerAndSave()
        {
            var app = Application.Current as App;
            try
            {
                var newDefList = await app.Web.GetDefinitionList(SubjectType);
                if (newDefList == null)
                {
                    return;
                }
                app.FileHandler.UpdateDefinitions(newDefList, SubjectType);
                await LoadDefinitionsFromFile();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        protected async Task LoadFormulasFromFile()
        {
            try
            {
                var app = Application.Current as App;
                var fmlList = await app.FileHandler.GetFormulas(SubjectType);
                if (fmlList != null)
                {
                    Formulas.Clear();
                    foreach (var f in fmlList)
                    {
                        Formulas.Add(f);
                    }
                    OnPropertyChanged(nameof(Formulas));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        protected async void LoadFormulasFromServerAndSave()
        {
            var app = Application.Current as App;
            try
            {
                var newDefList = await app.Web.GetFormulaList(SubjectType);
                if (newDefList == null)
                {
                    return;
                }
                app.FileHandler.UpdateFormulas(newDefList, SubjectType);
                await LoadFormulasFromFile();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public void Sort()
        {
            Definitions.Sort();
            Formulas.Sort();
            OnPropertyChanged(nameof(Definitions));
            OnPropertyChanged(nameof(Formulas));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Solver Solver { get; private set; }
        public List<Definition> Definitions { get; protected set; }
        public List<Formula> Formulas { get; protected set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
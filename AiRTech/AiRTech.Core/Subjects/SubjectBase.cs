using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AiRTech.Core.Subjects.Def;
using AiRTech.Core.Subjects.Formul;
using AiRTech.Core.Subjects.Solv;
using Xamarin.Forms;
using AiRTech.Core.Properties;

namespace AiRTech.Core.Subjects
{
    public abstract class SubjectBase : INotifyPropertyChanged
    {
        protected SubjectBase(SubjectType subjectType, Subject subject)
        {
            SubjectType = subjectType;
            Subject = subject;
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

        public async Task LoadFormulas()
        {
            await LoadFormulasFromFile().ContinueWith(task =>
            {
                LoadFormulasFromServerAndSave();
            });
        }

        protected abstract void UpdateDependencies();

        protected async Task LoadDefinitionsFromFile()
        {
            try
            {
                var defList = await CoreManager.Current.FileHandler.GetDefinitions(SubjectType);
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

        public async void LoadDefinitionsFromServerAndSave()
        {
            try
            {
                var newDefList = await CoreManager.Current.Web.GetDefinitionList(SubjectType);
                if (newDefList == null)
                {
                    return;
                }
                CoreManager.Current.App.ClearDefinitions(Subject);
                CoreManager.Current.FileHandler.UpdateDefinitions(newDefList, SubjectType);
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
                var fmlList = await CoreManager.Current.FileHandler.GetFormulas(SubjectType);
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

        public async void LoadFormulasFromServerAndSave()
        {
            try
            {
                var newDefList = await CoreManager.Current.Web.GetFormulaList(SubjectType);
                if (newDefList == null)
                {
                    return;
                }
                CoreManager.Current.App.ClearFormulas(Subject);
                CoreManager.Current.FileHandler.UpdateFormulas(newDefList, SubjectType);
                await LoadFormulasFromFile();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public void SearchDefinition()
        {
            CoreManager.Current.App.NavigateToSearchPage(NavPageType.DefinitionsPage, Subject);
        }

        public void SearchFormula()
        {
            CoreManager.Current.App.NavigateToSearchPage(NavPageType.FormulasPage, Subject);
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

        public SubjectType SubjectType { get; }
        public Solver Solver { get; private set; }
        public List<Definition> Definitions { get; protected set; }
        public List<Formula> Formulas { get; protected set; }
        public Subject Subject { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
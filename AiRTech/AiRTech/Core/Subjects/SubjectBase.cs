using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AiRTech.Annotations;
using AiRTech.Core.Subjects.Def;
using AiRTech.Core.Subjects.Solv;
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
            Solver = Solver.GetSolverFor(SubjectType);
            PropertyChanged += (sender, args) => UpdateDependencies();
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
                    foreach (var def in defList)
                    {
                        if (!Definitions.Contains(def))
                        {
                            Definitions.Add(def);
                        }
                    }
                    OnPropertyChanged(nameof(Definitions));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        protected async void LoadDefinitionsFromServer()
        {
            var app = Application.Current as App;
            try
            {
                var newDefList = await app.Web.GetDefinitionList(SubjectType);
                app.FileHandler.UpdateDefinitions(newDefList, SubjectType);
                await LoadDefinitionsFromFile();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Solver Solver { get; private set; }
        public List<Definition> Definitions { get; protected set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AiRTech.Annotations;
using AiRTech.Core.Subjects.Def;
using AiRTech.Core.Subjects.Solv;

namespace AiRTech.Core.Subjects
{
    public abstract class SubjectBase : INotifyPropertyChanged
    {
        protected SubjectType SubjectType;
        protected SubjectBase(SubjectType subjectType)
        {
            SubjectType = subjectType;
            Definitions = new ObservableCollection<Definition>();
            Definitions.CollectionChanged += UpdateDependencies;
            Solver = Solver.GetSolverFor(SubjectType);
            PropertyChanged += UpdateDependencies;
        }

        private void UpdateDependencies(object sender, PropertyChangedEventArgs e)
        {
            UpdateDependencies();
        }

        private void UpdateDependencies(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateDependencies();
            OnPropertyChanged(nameof(Definitions));
        }

        protected abstract void UpdateDependencies();

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Solver Solver { get; private set; }
        public ObservableCollection<Definition> Definitions { get; protected set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
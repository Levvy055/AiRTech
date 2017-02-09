using AiRTech.Core;
using AiRTech.Core.Misc;
using AiRTech.Core.Subjects;
using AiRTech.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AiRTech.Views.ViewModels
{
    public class SearchPageViewModel : INotifyPropertyChanged
    {
        private Subject _subject;
        private IEnumerable<Item> _list;
        private bool _active;

        public SearchPageViewModel(Subject subject)
        {
            _subject = subject;
            Header = "Wyniki:";
            Footer = "Koniec";
            RefreshData();
        }

        public async void RefreshData()
        {
            if (SearchPage.SearchFilter == NavPageType.DefinitionsPage)
            {
                _list = await CoreManager.Current.FileHandler.GetDefinitions(_subject.Base.SubjectType);
            }
            else
            {
                _list = await CoreManager.Current.FileHandler.GetFormulas(_subject.Base.SubjectType);
            }
            if (_list != null && _list.Any())
            {
                foreach (var item in _list)
                {
                    Items.Add(item);
                }
                OnPropertyChanged(nameof(Items));
                Active = true;
            }
            else
            {
                Active = false;
            }
        }

        private void Clear()
        {
            Items.Clear();
        }

        public void ShowAll()
        {
            Clear();
            if (_list != null && _list.Any())
            {
                foreach (var item in _list)
                {
                    Items.Add(item);
                }
                OnPropertyChanged(nameof(Items));
            }
        }

        public void SearchFor(string txt)
        {
            Clear();
            if (_list != null)
            {
                foreach (var item in _list)
                {
                    if (item.Title.ToLower().Contains(txt))
                    {
                        Items.Add(item);
                    }
                }
            }
        }

        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ObservableCollection<Item> Items { get; } = new ObservableCollection<Item>();
        public string Header { get; }
        public string Footer { get; }
        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                OnPropertyChanged(nameof(Active));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

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
            foreach (var item in _list)
            {
                Items.Add(item);
            }
            OnPropertyChanged(nameof(Items));
        }

        public void Clear()
        {
            Items.Clear();
        }

        public void SearchFor(string txt)
        {
            Clear();
            foreach (var item in _list)
            {
                if (item.Title.ToLower().Contains(txt))
                {
                    Items.Add(item);
                }
            }
        }

        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ObservableCollection<Item> Items { get; } = new ObservableCollection<Item>();
        public string Header { get; }
        public string Footer { get; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

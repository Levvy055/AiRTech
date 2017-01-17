using System;
using System.Collections.Generic;
using AiRTech.Core.Subjects;
using AiRTech.Core.Subjects.Def;
using AiRTech.Views.SubjectData;
using AiRTech.Views.ViewModels;
using Xamarin.Forms;

namespace AiRTech.Views
{
    public partial class DefinitionsPage : ContentPage
    {

        public DefinitionsPage(Subject subject)
        {
            Subject = subject;
            BindingContext = new DefinitionsViewModel(this);
            InitializeComponent();
        }

        public Subject Subject { get; set; }
        public Dictionary<string, ContentPage> DefViews { get; } = new Dictionary<string, ContentPage>();
        public ListView DefListView => Mlist;
    }
}

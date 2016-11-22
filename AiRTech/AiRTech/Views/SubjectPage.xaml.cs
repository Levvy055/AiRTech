﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.Subjects;
using AiRTech.Views.ViewModels;
using Xamarin.Forms;

namespace AiRTech.Views
{
    public partial class SubjectPage : ContentPage
    {
        public SubjectPage(Subject subject)
        {
            InitializeComponent();
            BindingContext = new SubjectViewModel(this, subject);
        }
    }
}
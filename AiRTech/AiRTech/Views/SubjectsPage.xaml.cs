﻿using AiRTech.Views.ViewModels;
using Xamarin.Forms;

namespace AiRTech.Views
{
    public partial class SubjectsPage : ContentPage
    {
        public SubjectsPage()
        {
            BindingContext = new SubjectsViewModel(this);
            InitializeComponent();
        }

    }
}

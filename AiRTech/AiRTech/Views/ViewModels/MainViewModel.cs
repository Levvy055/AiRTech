﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AiRTech.Views.ViewModels
{
    public class MainViewModel :ViewModelBase
    {
        public MainViewModel(Page page) : base(page)
        {
            Title = "AiR Tech";
        }
    }
}

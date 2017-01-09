﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AiRTech.Core.Math.Solvers.Components
{
    public class SvSwitch : ViewComponent
    {
        private readonly Switch _switch;

        public SvSwitch(string name, IDictionary<string, ViewComponent> uc = null, string txt = null, Action<object, EventArgs> action = null, bool initValue = false, bool editable=true) : base(ViewComponentType.Switch, name)
        {
            var lbl = new Label
            {
                Text = txt
            };
            _switch = new Switch
            {
                IsToggled = initValue,
                InputTransparent = !editable,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            if (action != null)
            {
                _switch.Toggled += (sender, args) => { action.Invoke(sender, args); };
            }
            var sp = new StackLayout
            {
                Children = { lbl, _switch }
            };
            Source = sp;
            uc?.Add(name, this);
        }

        public bool Selected => _switch.IsToggled;
    }
}
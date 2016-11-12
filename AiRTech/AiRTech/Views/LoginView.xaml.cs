using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.Subjects;
using Xamarin.Forms;

namespace AiRTech.Views
{
    public partial class LoginView : ContentView
    {
        public LoginView()
        {
            InitializeComponent();
            LoginBtn.Clicked += (sender, e) =>
            {
                Debug.WriteLine("Btn Login Clicked.");

            };

        }
    }
}

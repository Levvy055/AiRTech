using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiRTech.Core
{
    public interface IDialogManager
    {
        void ShowWarningDialog(string header, string message);
    }
}

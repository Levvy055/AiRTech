using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.SecureStorage;

namespace AiRTech.Droid
{
    public static class LinkerPreserve
    {
        static LinkerPreserve()
        {
            throw new Exception(typeof(SecureStorageImplementation).FullName);
        }
    }

    public class PreserveAttribute : Attribute
    {
    }
}
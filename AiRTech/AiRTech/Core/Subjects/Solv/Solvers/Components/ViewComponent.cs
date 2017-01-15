using System;
using Xamarin.Forms;

namespace AiRTech.Core.Subjects.Solv.Solvers.Components
{
    public class ViewComponent
    {
        private static int _id;

        protected ViewComponent(ViewComponentType compType, string name = null)
        {
            _id++;
            CompType = compType;
            if (string.IsNullOrWhiteSpace(name))
            {
                name = _id.ToString();
            }
            Name = compType + "_" + name;
        }

        public ViewComponentType CompType { get; private set; }
        public View Source { get; protected set; }
        public string Name { get; private set; }

        public T GetSourceAs<T>()
        {
            if (Source == null)
            {
                throw new InvalidCastException("Source is null!");
            }
            try
            {
                return (T)Convert.ChangeType(Source, typeof(T));
            }
            catch (InvalidCastException)
            {
                return default(T);
            }
        }
    }
}

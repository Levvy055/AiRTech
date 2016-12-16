using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.Commands;
using AiRTech.Core.Math.Solvers;
using Xamarin.Forms;

namespace AiRTech.Core.Subjects
{
    public class Subject
    {
        public static Dictionary<SubjectType, Subject> Subjects { get; } = new Dictionary<SubjectType, Subject>()
        {
            {SubjectType.PODSTAWY_ELEKTRONIKI,
                new Subject("Podstawy Elektroniki",
                    typeof (ElectronicBasics),
                    SubjectType.PODSTAWY_ELEKTRONIKI,
                    "AiRTech.Resources.pe.png")},
            {SubjectType.PODSTAWY_TEORII_SYGNALOW,
                new Subject("Podstawy Teorii Sygnałów",
                    typeof(SignalTheoryBasics),
                   SubjectType.PODSTAWY_TEORII_SYGNALOW,
                    "AiRTech.Resources.pts.png")}
        };

        public static ICollection<Subject> SubjectAsValues => Subjects.Values;

        private Subject(string name, Type type, SubjectType subjectType, string img)
        {
            if (type == null)
            {
                throw new ArgumentException("Type of subject class is null!");
            }
            Id = subjectType.GetHashCode();
            Name = name;
            Details = "Przejdź";
            Img = img;
            Base = (SubjectBase) Activator.CreateInstance(type, subjectType);
        }

        public int Id { get; }
        public string Name { get; }
        public string Details { get; set; }
        public string Img { get; }
        public ImageSource ImgProperty => ImageSource.FromResource(Img);
        public SubjectBase Base { get; private set; }
    }
}

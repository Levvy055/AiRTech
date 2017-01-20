using System;
using System.Collections.Generic;
using AiRTech.Core.Misc;
using AiRTech.Core.Subjects.Impl;
using Xamarin.Forms;

namespace AiRTech.Core.Subjects
{
    public class Subject
    {
        private static Dictionary<SubjectType, Subject> _subjects;

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
            Base = (SubjectBase)Activator.CreateInstance(type);
            Subjects.Add(subjectType, this);
        }

        public static Dictionary<SubjectType, Subject> Subjects
        {
            get
            {
                if (_subjects != null)
                {
                    return _subjects;
                }
                _subjects = new Dictionary<SubjectType, Subject>();
                //new Subject("Podstawy Elektroniki", typeof(ElectronicBasics),
                //    SubjectType.PODSTAWY_ELEKTRONIKI, "AiRTech.Resources.pe.png");
                new Subject("Podstawy Teorii Sygnałów", typeof(SignalTheoryBasics),
                    SubjectType.PODSTAWY_TEORII_SYGNALOW, "AiRTech.Resources.pts.png");
                return _subjects;
            }
        }
        public static ICollection<Subject> SubjectAsValues => Subjects.Values;

        public int Id { get; }
        public string Name { get; }
        public string Details { get; set; }
        public string Img { get; }
        public ImageSource ImgProperty => ImageResourceExtension.GetEmbeddedImage(Img);
        public SubjectBase Base { get; private set; }
    }
}

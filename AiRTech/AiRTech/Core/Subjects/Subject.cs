﻿using System;
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
                    typeof(ElectronicBasicsSolver),
                    SubjectType.PODSTAWY_ELEKTRONIKI,
                    "AiRTech.Resources.pe.png")},
            {SubjectType.PODSTAWY_TEORII_SYGNALOW,
                new Subject("Podstawy Teorii Sygnałów",
                    typeof(SignalTheoryBasics),
                    typeof(SignalTheoryBasicsSolver),
                    SubjectType.PODSTAWY_TEORII_SYGNALOW,
                    "AiRTech.Resources.pts.png")}
        };

        public static ICollection<Subject> SubjectAsValues => Subjects.Values;

        private Subject(string name, Type type, Type solverType, SubjectType subjectType, string img)
        {
            if (type == null)
            {
                throw new ArgumentException("Type of subject class is null!");
            }
            Id = subjectType.GetHashCode();
            Name = name;
            Details = "Przejdź";
            Img = img;
            Obj = Activator.CreateInstance(type);
            SolverType = solverType;
        }

        public int Id { get; }
        public string Name { get; }
        public string Details { get; set; }
        public string Img { get; }
        public ImageSource ImgProperty => ImageSource.FromResource(Img);
        public object Obj { get; private set; }
        public Type SolverType { get; }
    }

    public class SubjectBase
    {

    }

    public enum SubjectType
    {
        PODSTAWY_ELEKTRONIKI, PODSTAWY_TEORII_SYGNALOW, MECHANIKA, PODSTAWY_AUTOMATYKI, METODY_NUMERYCZNE, ANGIELSKI, ELEMENTY_OPTYKI_I_AKUSTYKI
    }
}

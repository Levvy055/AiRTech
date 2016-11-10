using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiRTech.Core.Subjects
{
    public class Subject
    {
        public static Dictionary<SubjectType, Subject> Subjects { get; } = new Dictionary<SubjectType, Subject>()
        {
            {SubjectType.PODSTAWY_ELEKTRONIKI, new Subject("Podstawy Elektroniki", typeof (ElectronicBasics), SubjectType.PODSTAWY_ELEKTRONIKI, "pe.png")},
            {SubjectType.PODSTAWY_TEORII_SYGNALOW, new Subject("Podstawy Teorii Sygnałów", typeof(SignalTheoryBasics),SubjectType.PODSTAWY_TEORII_SYGNALOW, "pts.png")}
        };


        public Subject(string name, Type type, SubjectType subjectType, string img)
        {
            Id = subjectType.GetHashCode();
            Name = name;
            Img = img;
            Obj = Activator.CreateInstance(type);
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Img { get; private set; }
        public object Obj { get; private set; }
    }

    public enum SubjectType
    {
        PODSTAWY_ELEKTRONIKI, PODSTAWY_TEORII_SYGNALOW, MECHANIKA, PODSTAWY_AUTOMATYKI, METODY_NUMERYCZNE, ANGIELSKI, ELEMENTY_OPTYKI_I_AKUSTYKI
    }
}

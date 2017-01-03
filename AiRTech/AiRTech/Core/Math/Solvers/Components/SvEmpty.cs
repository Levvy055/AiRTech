namespace AiRTech.Core.Math.Solvers.Components
{
    public class SvEmpty:ViewComponent
    {
        public SvEmpty() : base(ViewComponentType.Empty)
        {
        }

        public static SvEmpty Elem { get; } = new SvEmpty();
    }
}

namespace Sketcher.Models.Constraints
{
    public interface IConstraint
    {
        string ErrorMessage { get; }
        bool CanApply();
        void Apply(bool forward);
        bool Validate();
    }
}

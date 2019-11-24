using TauCode.Domain.Identities;

namespace TauCode.Cqrs.Queries
{
    public interface ICodedEntityQuery : IQuery
    {
        IdBase GetId();
        string GetCode();
        string GetCodePropertyName();
    }
}

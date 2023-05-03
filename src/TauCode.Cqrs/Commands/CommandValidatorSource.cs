using System.Reflection;

namespace TauCode.Cqrs.Commands;

public class CommandValidatorSource : ICommandValidatorSource
{
    private readonly ValidatorSource _validatorSource;

    public CommandValidatorSource(params Assembly[] assemblies)
    {
        _validatorSource = new ValidatorSource(typeof(ICommand), assemblies);
    }

    public Type? GetCommandValidatorType(Type commandType)
    {
        return _validatorSource.GetValidatorType(commandType);
    }
}
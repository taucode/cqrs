namespace TauCode.Cqrs.Commands;

public interface ICommandValidatorSource
{
    Type? GetCommandValidatorType(Type commandType);
}
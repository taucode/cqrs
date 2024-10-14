namespace TauCode.Cqrs.Commands;

public static class CommandValidatorSourceExtensions
{
    public static object? CreateCommandValidator(
        this ICommandValidatorSource commandValidatorSource,
        IServiceProvider serviceProvider,
        Type commandType)
    {
        var commandValidatorType = commandValidatorSource.GetCommandValidatorType(commandType);
        if (commandValidatorType == null)
        {
            return null;
        }

        var validator = serviceProvider.GetService(commandValidatorType);
        return validator;
    }
}
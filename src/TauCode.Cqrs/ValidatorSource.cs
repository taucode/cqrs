using FluentValidation;
using System.Reflection;

namespace TauCode.Cqrs;

internal class ValidatorSource
{
    private class ValidatorRecord
    {
        public ValidatorRecord(Type targetType, Type validatorType)
        {
            this.TargetType = targetType;
            this.ValidatorType = validatorType;
        }

        internal Type TargetType { get; }
        internal Type ValidatorType { get; }
    }

    private readonly Type _targetInterface;
    private readonly Dictionary<Type, ValidatorRecord> _records;

    /// <summary>
    /// Loads validators from assemblies
    /// </summary>
    /// <param name="targetInterface">"Core" interface of object to validate. E.g. ICommand or IQuery</param>
    /// <param name="assemblies">Assemblies to scan for validators in</param>
    internal ValidatorSource(Type targetInterface, params Assembly[] assemblies)
    {
        _targetInterface = targetInterface; // todo: check it is really an interface?
        _records = assemblies
            .SelectMany(x => x.GetTypes())
            .Select(CreateValidatorRecord)
            .Where(x => x != null)
            .ToDictionary(x => x!.TargetType, x => x!);
    }

    internal Type? GetValidatorType(Type targetType)
    {
        return _records.GetValueOrDefault(targetType)?.ValidatorType;
    }

    private ValidatorRecord? CreateValidatorRecord(Type potentialValidatorType)
    {
        if (potentialValidatorType.IsAbstract || potentialValidatorType.IsValueType)
        {
            return null;
        }

        var interfaces = potentialValidatorType.GetInterfaces();
        if (!interfaces.Contains(typeof(IValidator)))
        {
            return null;
        }

        var genericInterfaces =
            interfaces
                .Where(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(IValidator<>))
                .ToList();

        if (genericInterfaces.Count != 1)
        {
            return null;
        }

        var genericInterface = genericInterfaces.Single();

        var validatorTarget = genericInterface.GetGenericArguments().Single();

        if (!validatorTarget.GetInterfaces().Contains(_targetInterface))
        {
            return null;
        }

        var record = new ValidatorRecord(validatorTarget, potentialValidatorType);
        return record;
    }
}
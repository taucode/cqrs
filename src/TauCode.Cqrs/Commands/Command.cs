namespace TauCode.Cqrs.Commands;

public abstract class Command<TResult> : ICommand<TResult>
{
    private TResult _result = default!;

    public void SetResult(TResult result)
    {
        _result = result;
    }

    public TResult GetResult()
    {
        return _result;
    }
}
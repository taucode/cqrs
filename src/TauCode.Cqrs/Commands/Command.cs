namespace TauCode.Cqrs.Commands;

public class Command<TResult> : ICommand<TResult>
{
    private TResult _result;

    public void SetResult(TResult result)
    {
        _result = result;
    }

    public TResult GetResult()
    {
        return _result;
    }
}
namespace TauCode.Cqrs.Commands
{
    public interface ICommand<TResult> : ICommand
    {
        void SetResult(TResult result);

        TResult GetResult();
    }
}

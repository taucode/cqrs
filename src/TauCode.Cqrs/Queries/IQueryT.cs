namespace TauCode.Cqrs.Queries;

public interface IQuery<TResult> : IQuery
{
    void SetResult(TResult result);

    TResult GetResult();
}
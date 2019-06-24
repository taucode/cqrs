namespace TauCode.Cqrs.Queries
{
    public interface IQueryRunner
    {
        void Run<TQuery>(TQuery query) where TQuery : IQuery;
    }
}

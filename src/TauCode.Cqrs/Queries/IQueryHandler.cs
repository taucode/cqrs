using System.Threading.Tasks;

namespace TauCode.Cqrs.Queries
{
    public interface IQueryHandler<in TQuery> 
        where TQuery : IQuery
    {
        void Execute(TQuery query);
        Task ExecuteAsync(TQuery query);
    }
}

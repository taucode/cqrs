using System.Threading;
using System.Threading.Tasks;

namespace TauCode.Cqrs.Queries
{
    public class QueryHandlerBase<TQuery> : IQueryHandler<TQuery> where TQuery : IQuery
    {
        public void Execute(TQuery query)
        {
            throw new FeatureNotImplementedException();
        }

        public Task ExecuteAsync(TQuery query, CancellationToken cancellationToken)
        {
            throw new FeatureNotImplementedException();
        }
    }
}

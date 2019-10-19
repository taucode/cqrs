using System;
using System.Threading;
using System.Threading.Tasks;

namespace TauCode.Cqrs.Queries
{
    public class QueryRunner : IQueryRunner
    {
        #region Fields

        protected readonly IQueryHandlerFactory QueryHandlerFactory;

        #endregion

        #region Constructor

        public QueryRunner(IQueryHandlerFactory queryHandlerFactory)
        {
            QueryHandlerFactory = queryHandlerFactory ?? throw new ArgumentNullException(nameof(queryHandlerFactory));
        }

        #endregion

        #region Virtual

        protected virtual void OnBeforeExecuteHandler<TQuery>(IQueryHandler<TQuery> handler, TQuery query)
            where TQuery : IQuery
        {
            // idle, override in ancestor if needed.
        }

        protected virtual Task OnBeforeExecuteHandlerAsync<TQuery>(IQueryHandler<TQuery> handler, TQuery query, CancellationToken cancellationToken)
            where TQuery : IQuery
        {
            // idle, override in ancestor if needed.
            return Task.CompletedTask;
        }


        #endregion

        #region IQueryRunner Members
        
        public void Run<TQuery>(TQuery query) where TQuery : IQuery
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            IQueryHandler<TQuery> queryHandler;

            try
            {
                queryHandler = QueryHandlerFactory.Create<TQuery>();
            }
            catch (Exception ex)
            {
                throw new CannotCreateQueryHandlerException(ex);
            }

            if (queryHandler == null)
            {
                throw new CannotCreateQueryHandlerException();
            }

            this.OnBeforeExecuteHandler(queryHandler, query);

            queryHandler.Execute(query);
        }

        public async Task RunAsync<TQuery>(TQuery query, CancellationToken cancellationToken) where TQuery : IQuery
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            IQueryHandler<TQuery> queryHandler;

            try
            {
                queryHandler = QueryHandlerFactory.Create<TQuery>();
            }
            catch (Exception ex)
            {
                throw new CannotCreateQueryHandlerException(ex);
            }

            if (queryHandler == null)
            {
                throw new CannotCreateQueryHandlerException();
            }

            await this.OnBeforeExecuteHandlerAsync(queryHandler, query, cancellationToken);
            await queryHandler.ExecuteAsync(query, cancellationToken);
        }

        #endregion
    }
}

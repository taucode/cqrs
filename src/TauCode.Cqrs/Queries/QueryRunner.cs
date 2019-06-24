using System;

namespace TauCode.Cqrs.Queries
{
    public class QueryRunner : IQueryRunner
    {
        protected readonly IQueryHandlerFactory QueryHandlerFactory;

        public QueryRunner(IQueryHandlerFactory queryHandlerFactory)
        {
            QueryHandlerFactory = queryHandlerFactory ?? throw new ArgumentNullException(nameof(queryHandlerFactory));
        }

        protected virtual void OnBeforeExecuteHandler<TQuery>(IQueryHandler<TQuery> handler, TQuery query)
            where TQuery : IQuery
        {
            // idle, override in ancestor if needed.
        }

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
    }
}

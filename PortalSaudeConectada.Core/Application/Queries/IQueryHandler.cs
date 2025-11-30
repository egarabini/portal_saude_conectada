using MediatR;

namespace PortalSaudeConectada.Core.Application.Queries;

/// <summary>
/// Interface base para Query Handlers
/// </summary>
/// <typeparam name="TQuery">Tipo da Query</typeparam>
/// <typeparam name="TResponse">Tipo do resultado retornado</typeparam>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}


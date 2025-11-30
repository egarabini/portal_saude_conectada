using MediatR;

namespace PortalSaudeConectada.Core.Application.Queries;

/// <summary>
/// Interface base para Queries
/// Queries sempre retornam um resultado (nunca void)
/// </summary>
/// <typeparam name="TResponse">Tipo do resultado retornado</typeparam>
public interface IQuery<out TResponse> : IRequest<TResponse>
{
}


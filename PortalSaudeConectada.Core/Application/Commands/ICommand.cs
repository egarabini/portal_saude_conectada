using MediatR;

namespace PortalSaudeConectada.Core.Application.Commands;

/// <summary>
/// Marker interface para Commands que não retornam valor
/// </summary>
public interface ICommand : IRequest
{
}

/// <summary>
/// Interface base para Commands que retornam um resultado
/// </summary>
/// <typeparam name="TResponse">Tipo do resultado retornado</typeparam>
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}


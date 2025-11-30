using MediatR;

namespace PortalSaudeConectada.Core.Application.Commands;

/// <summary>
/// Interface base para Command Handlers que não retornam valor
/// </summary>
/// <typeparam name="TCommand">Tipo do Command</typeparam>
public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{
}

/// <summary>
/// Interface base para Command Handlers que retornam um resultado
/// </summary>
/// <typeparam name="TCommand">Tipo do Command</typeparam>
/// <typeparam name="TResponse">Tipo do resultado retornado</typeparam>
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
}


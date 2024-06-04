using System.Reactive.Disposables;
using TicTacToe.Interfaces;
using TicTacToe.Models;

namespace TicTacToe;

public static class Extensions
{
    public static bool IsAllSymbolsAre(this IEnumerable<Symbol?> line, Symbol symbol)
    {
        return line.All(x => x == symbol);
    }
    
    public static ValueTask<TElement> NextAsync<TElement>(this IObservable<TElement> observable, CancellationToken cancellationToken = default)
    {
        var taskCompletionSource = new TaskCompletionSource<TElement>();

        IDisposable subscription = Disposable.Empty;

        cancellationToken.Register(Cancel);

        subscription = observable.Subscribe(x =>
            {
                subscription.Dispose();
                taskCompletionSource.SetResult(x);
            },
            exception =>
            {
                subscription.Dispose();
                taskCompletionSource.SetException(exception);
            },
            Cancel);

        return new ValueTask<TElement>(taskCompletionSource.Task);

        void Cancel()
        {
            subscription.Dispose();
            taskCompletionSource.TrySetCanceled(cancellationToken);
        }
    }

    public static Symbol Reverse(this Symbol symbol) => symbol is Symbol.X ? Symbol.O : Symbol.X;

    public static XPlayer AsXPlayer(this IStrategy strategy) => new(strategy);

    public static OPlayer AsOPlayer(this IStrategy strategy) => new(strategy);
}
using System.Reactive.Disposables;
using System.Reactive.Linq;
using TicTacToe.Models;

namespace TicTacToe;

public static class GameExtensions
{
    public static IAsyncEnumerable<Move> ToAsyncEnumerable(this Game game)
    {
        ArgumentNullException.ThrowIfNull(game);

        return AsyncEnumerable.Create(ct => GameIterator(game, ct));
    }

    private static async IAsyncEnumerator<Move> GameIterator(Game game, CancellationToken cancellationToken)
    {
        while (await game.NextMove(cancellationToken))
        {
            if(cancellationToken.IsCancellationRequested)
                break;

            yield return game.LastMove;
        }
    }

    public static IObservable<Move> ToObservable(this Game game)
    {
        ArgumentNullException.ThrowIfNull(game);

        return Observable.Create<Move>(observer =>
        {
            var ctd = new CancellationDisposable();

#pragma warning disable CS4014
            GameEvaluator(game, observer, ctd.Token);
#pragma warning restore CS4014

            return ctd;
        });
    }

    private static async Task GameEvaluator(Game game, IObserver<Move> observer, CancellationToken cancellationToken)
    {
        try
        {
            while (await game.NextMove(cancellationToken))
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                observer.OnNext(game.LastMove);
            }
        }
        catch (Exception e)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                observer.OnError(e);
            }
        }
        finally
        {
            observer.OnCompleted();
        }
    }
}
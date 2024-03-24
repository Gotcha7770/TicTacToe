using TicTacToe.Interfaces;

namespace TicTacToe.Models;

public enum GameState
{
    InProgress,
    GameOver,
    Draw
}

// https://www.xchen.tech/mywork/blog-post-one-g54cj-24mzx-czgpd
public class Game
{
    private readonly Field _field;
    private readonly XPlayer _xPlayer;
    private readonly OPlayer _oPlayer;

    public Game(XPlayer xPlayer, OPlayer oPlayer)
    {
        _field = new Field();
        _xPlayer = xPlayer;
        _oPlayer = oPlayer;

        CurrentPlayer = _xPlayer;
    }

    public Move LastMove { get; private set; }
    public IPlayer CurrentPlayer { get; private set; }
    public GameState State { get; private set; }

    public async Task<bool> NextMove(CancellationToken cancellationToken)
    {
        if (State is GameState.InProgress)
        {
            LastMove = await CurrentPlayer.GetNextMove(_field, cancellationToken);
            if (cancellationToken.IsCancellationRequested)
                return false;

            _field.Apply(LastMove);

            if (IsWinner(_field, CurrentPlayer.Symbol))
            {
                State = GameState.GameOver;
            }
            else if (IsDraw(_field))
            {
                State = GameState.Draw;
            }
            else
            {
                SwitchPlayer();
            }

            return true;
        }
        
        return false;
    }

    internal static IEnumerable<Move> GetAvailableMoves(Field field, Symbol symbol)
    {
        return field.GetEmptyCells().Select(x => new Move(x, symbol));
    }

    internal static bool IsDraw(Field field) => field.GetEmptyCells().IsEmpty();

    internal static bool IsWinner(Field field, Symbol symbol) => field.GetAllLines().Any(x => x.IsAllSymbolsAre(symbol));
    
    internal static Symbol? GetWinner(Field field)
    {
        foreach (var line in field.GetAllLines())
        {
            foreach (var player in Enum.GetValues<Symbol>())
            {
                if (line.IsAllSymbolsAre(player))
                    return player;
            }
        }

        return null;
    }

    internal static int Score(Field field, Symbol symbol, byte depth)
    {
        var winner = GetWinner(field);
        if (winner.HasValue)
        {
            return winner == symbol ? 10 - depth : depth - 10;
        }

        return 0;
    }

    private void SwitchPlayer()
    {
        CurrentPlayer = CurrentPlayer == _xPlayer ? _oPlayer : _xPlayer;
    }
}
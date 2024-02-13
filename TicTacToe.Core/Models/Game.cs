using TicTacToe.Interfaces;

namespace TicTacToe.Models;

public enum GameState
{
    InProgress,
    GameOver,
    Draw
}

public class Game
{
    private readonly Field _field;
    private readonly XPlayer _xPlayer;
    private readonly OPlayer _oPlayer;

    public Game(XPlayer one, OPlayer other)
    {
        _field = new Field();
        _xPlayer = one;
        _oPlayer = other;

        CurrentPlayer = _xPlayer;
    }

    public GameState State { get; private set; }
    public IPlayer CurrentPlayer { get; private set; }

    public async Task<bool> NextMove()
    {
        if (State is GameState.InProgress)
        {
            var move = await CurrentPlayer.GetNextMove(_field);
            _field.Apply(move);

            if (IsDraw())
            {
                State = GameState.Draw;
            }
            else if (IsWinner(CurrentPlayer))
            {
                State = GameState.GameOver;
            }
            else
            {
                SwitchPlayer();
            }

            return true;
        }
        
        return false;
    }

    private void SwitchPlayer()
    {
        CurrentPlayer = CurrentPlayer == _xPlayer ? _xPlayer : _oPlayer;
    }

    private bool IsDraw() => _field.GetEmptyCells().IsEmpty();

    private bool IsWinner(IPlayer player) => _field.GetAllLines().Any(x => x.IsAllSymbolsAre(player.Symbol));
}
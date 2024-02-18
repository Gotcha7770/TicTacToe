using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Avalonia.ReactiveUI;
using ReactiveUI;
using TicTacToe.Interfaces;
using TicTacToe.Models;

namespace TicTacToe.Avalonia.ViewModels;

public record BoardSize(int Rows, int Columns);

public class GameViewModel : ReactiveObject, IDisposable
{
    private readonly XPlayer _xPlayer;
    private readonly OPlayer _oPlayer;
    private readonly CellViewModel[] _cells;

    private IDisposable _subscription;
    private GameState _currentState;
    private IPlayer _currentPlayer;

    public BoardSize Size { get; }
    public IReadOnlyCollection<CellViewModel> Cells => _cells;

    public GameViewModel(XPlayer xPlayer, OPlayer oPlayer, BoardSize size)
    {
        _xPlayer = xPlayer;
        _oPlayer = oPlayer;
        Size = size;

        _cells = new CellViewModel[size.Rows * size.Columns];
        _subscription = CreateNewGame();
    }

    public IPlayer CurrentPlayer
    {
        get => _currentPlayer;
        set => this.RaiseAndSetIfChanged(ref _currentPlayer, value);
    }

    public GameState State
    {
        get => _currentState;
        set => this.RaiseAndSetIfChanged(ref _currentState, value);
    }

    public void Restart() => CreateNewGame();

    public void Dispose() 
    {
        _subscription?.Dispose();
    }

    private IDisposable CreateNewGame()
    {
        _subscription?.Dispose();
        ResetCells();

        var game = new Game(_xPlayer, _oPlayer);
        return game.ToObservable()
            .SubscribeOn(AvaloniaScheduler.Instance)
            .Subscribe(x => ApplyMove(x, game), () => State = game.State);
    }

    private void ResetCells()
    {
        _cells.ForEach(x => x.Symbol = null);
    }

    private void ApplyMove(Move move, Game game)
    {
        var cell = _cells[ToIndex(move.Cell)];
        cell.Symbol = move.Symbol;
        CurrentPlayer = game.CurrentPlayer;
    }

    private int ToIndex(Cell cell) => cell.Row * Size.Columns + cell.Column;
}
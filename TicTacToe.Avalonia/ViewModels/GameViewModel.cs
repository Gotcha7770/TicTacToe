using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.ReactiveUI; //TODO: убрать
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

    private readonly IDisposable _fullCleanup;
    private GameState _currentState;
    private IPlayer _currentPlayer;
    private BoardSize _size;
    private int _xPlayerScore;
    private int _oPlayerScore;
    private IDisposable _gameCleanup;

    public IReadOnlyCollection<CellViewModel> Cells => _cells;

    public GameViewModel(BoardSize size)
    {
        Size = size;
        _cells = new CellViewModel[size.Rows * size.Columns];
        var userMove = InitializeCells(size);

        _xPlayer = new HumanStrategy(userMove).AsXPlayer();
        _oPlayer = new HumanStrategy(userMove).AsOPlayer();

        _gameCleanup = CreateNewGame();
        _fullCleanup = new CompositeDisposable
        {
            Disposable.Create(_gameCleanup.Dispose),
        };
    }

    public GameViewModel(IAIStrategy strategy, Symbol aiPlayer, BoardSize size)
    {
        Size = size;
        _cells = new CellViewModel[size.Rows * size.Columns];
        var userMove = InitializeCells(size);

        if (aiPlayer is Symbol.X)
        {
            _xPlayer = strategy.AsXPlayer();
            _oPlayer = new HumanStrategy(userMove).AsOPlayer();
        }
        else
        {
            _xPlayer = new HumanStrategy(userMove).AsXPlayer();
            _oPlayer = strategy.AsOPlayer();
        }

        _gameCleanup = CreateNewGame();
        _fullCleanup = new CompositeDisposable
        {
            Disposable.Create(() => _gameCleanup?.Dispose()),
            strategy as IDisposable ?? Disposable.Empty
        };
    }

    public BoardSize Size
    {
        get => _size;
        set => this.RaiseAndSetIfChanged(ref _size, value);
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

    public int XPlayerScore
    {
        get => _xPlayerScore;
        set => this.RaiseAndSetIfChanged(ref _xPlayerScore, value);
    }

    public int OPlayerScore
    {
        get => _oPlayerScore;
        set => this.RaiseAndSetIfChanged(ref _oPlayerScore, value);
    }

    public void Restart() => _gameCleanup = CreateNewGame();

    public void Dispose() 
    {
        _fullCleanup?.Dispose();
    }

    private IObservable<Move> InitializeCells(BoardSize size)
    {
        for (byte i = 0; i < size.Rows; i++)
        {
            for (byte j = 0; j < size.Columns; j++)
            {
                _cells[i * size.Columns + j] = new CellViewModel(new Cell(i, j));
            }
        }
        
        return _cells.Select(x => x.Move.Select(cell => new Move(cell, CurrentPlayer.Symbol))).Merge();
    }

    private IDisposable CreateNewGame()
    {
        _gameCleanup?.Dispose();
        ResetCells();

        var game = new Game(_xPlayer, _oPlayer);
        CurrentPlayer = game.CurrentPlayer;

        return game.ToObservable()
            .SubscribeOn(AvaloniaScheduler.Instance)
            .Subscribe(x => ApplyMove(x, game), () => Finish(game));
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
    
    private void Finish(Game game)
    {
        if (game.State is GameState.GameOver)
        {
            if (CurrentPlayer == _xPlayer)
                XPlayerScore++;
            else if (CurrentPlayer == _oPlayer)
                OPlayerScore++;
        }
        State = game.State;
    }

    private int ToIndex(Cell cell) => cell.Row * Size.Columns + cell.Column;
}
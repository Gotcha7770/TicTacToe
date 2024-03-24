using System;
using System.Reactive.Linq;
using ReactiveUI;
using TicTacToe.Models;

namespace TicTacToe.Avalonia.ViewModels;

public class CellViewModel : ReactiveObject
{
    private Symbol? _symbol;

    public CellViewModel(Cell cell)
    {
        PressedCommand = ReactiveCommand.Create<Symbol, Symbol>(x => x, this.WhenAnyValue(x => x.Symbol).Select(x => x is null));
        Move = PressedCommand.Select(x => new Move(cell, x));
    }

    public ReactiveCommand<Symbol, Symbol> PressedCommand { get; }

    public IObservable<Move> Move { get; }

    public Symbol? Symbol
    {
        get => _symbol;
        set => this.RaiseAndSetIfChanged(ref _symbol, value);
    }
}
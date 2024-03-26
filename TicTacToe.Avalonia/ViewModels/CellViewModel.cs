using System;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using TicTacToe.Models;

namespace TicTacToe.Avalonia.ViewModels;

public class CellViewModel : ReactiveObject
{
    private Symbol? _symbol;

    public CellViewModel(Cell cell)
    {
        PressedCommand = ReactiveCommand.Create(() => { }, this.WhenAnyValue(x => x.Symbol).Select(x => x is null));
        Move = PressedCommand.Select(_ => cell);
    }

    public ReactiveCommand<Unit, Unit> PressedCommand { get; }

    public IObservable<Cell> Move { get; }

    public Symbol? Symbol
    {
        get => _symbol;
        set => this.RaiseAndSetIfChanged(ref _symbol, value);
    }
}
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using TicTacToe.Models;

namespace TicTacToe.Avalonia.ViewModels;

public class CellViewModel : ReactiveObject
{
    private Symbol? _symbol;

    public CellViewModel()
    {
        PressedCommand = ReactiveCommand.Create(() => { }, this.WhenAnyValue(x => x.Symbol).Select(x => x is null));
    }

    public ReactiveCommand<Unit,Unit> PressedCommand { get; set; }

    public Symbol? Symbol
    {
        get => _symbol;
        set => this.RaiseAndSetIfChanged(ref _symbol, value);
    }
}
using ReactiveUI;
using TicTacToe.Models;

namespace TicTacToe.Avalonia.ViewModels;

public class CellViewModel : ReactiveObject
{
    private Symbol? _symbol;

    public Symbol? Symbol
    {
        get => _symbol;
        set => this.RaiseAndSetIfChanged(ref _symbol, value);
    }
}
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using TicTacToe.Models;

namespace TicTacToe.Avalonia.Controls;

public class Cell : TemplatedControl
{
    public static readonly StyledProperty<Symbol?> SymbolProperty = AvaloniaProperty.Register<Cell, Symbol?>(nameof(Symbol));

    public Symbol? Symbol
    {
        get => GetValue(SymbolProperty);
        set => SetValue(SymbolProperty, value);
    }

    public static readonly StyledProperty<ICommand> CommandProperty = AvaloniaProperty.Register<Cell, ICommand>("Command");

    public ICommand Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
}
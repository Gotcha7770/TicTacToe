using Avalonia.ReactiveUI;
using TicTacToe.Avalonia.ViewModels;

namespace TicTacToe.Avalonia.Views;

public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
    }
}
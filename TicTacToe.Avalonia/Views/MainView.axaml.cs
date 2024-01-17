using Avalonia.Controls;
using ReactiveUI;
using TicTacToe.Avalonia.ViewModels;

namespace TicTacToe.Avalonia.Views;

public partial class MainView : UserControl, IViewFor<MainViewModel>
{
    public MainView()
    {
        InitializeComponent();
    }

    object IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = value as MainViewModel;
    }

    public MainViewModel ViewModel { get; set; }
}
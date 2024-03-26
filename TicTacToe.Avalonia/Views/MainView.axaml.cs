using System;
using System.Reactive.Disposables;
using Avalonia.Controls;
using ReactiveUI;
using TicTacToe.Avalonia.ViewModels;
using TicTacToe.Models;

namespace TicTacToe.Avalonia.Views;

public partial class MainView : UserControl, IViewFor<MainViewModel>
{
    public MainView()
    {
        InitializeComponent();
        GameModeSelector.ItemsSource = Enum.GetValues<GameMode>();

        this.WhenActivated(disposable =>
        {
            this.Bind(ViewModel,
                    viewModel => viewModel.SelectedPlayer,
                    view => view.XPlayerButton.IsChecked,
                    x => x is Symbol.X,
                    x => x is true ? Symbol.X : Symbol.O)
                .DisposeWith(disposable);

            this.Bind(ViewModel,
                    viewModel => viewModel.SelectedPlayer,
                    view => view.OPlayerButton.IsChecked,
                    x => x is Symbol.O,
                    x => x is true ? Symbol.O : Symbol.X)
                .DisposeWith(disposable);
        });
    }

    protected override void OnDataContextBeginUpdate()
    {
        ViewModel = DataContext as MainViewModel;
    }

    object IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = value as MainViewModel;
    }

    public MainViewModel ViewModel { get; set; }
}
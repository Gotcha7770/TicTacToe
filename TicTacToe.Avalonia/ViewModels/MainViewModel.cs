using System;
using System.Collections.Generic;
using System.Reactive;
using ReactiveUI;
using TicTacToe.Models;

namespace TicTacToe.Avalonia.ViewModels;

public enum GameMode
{
    Easy,
    Medium,
    Hard,
    HotSeat
}

public class MainViewModel : ReactiveObject
{
    private GameMode _gameMode;
    private Symbol _selectedPlayer;
    
    public MainViewModel()
    {
        RestartCommand = ReactiveCommand.Create(Restart);
        SetPlayerCommand = ReactiveCommand.Create<Symbol>(x => SelectedPlayer = x);
    }
    
    public ReactiveCommand<Unit, Unit> RestartCommand { get; set; }
    public ReactiveCommand<Symbol, Unit> SetPlayerCommand { get; set; }
    public IReadOnlyCollection<GameMode> GameModes { get; } = Enum.GetValues<GameMode>();
    public GameViewModel GameViewModel { get; }

    public GameMode SelectedGameMode
    {
        get => _gameMode;
        set => this.RaiseAndSetIfChanged(ref _gameMode, value);
    }

    public Symbol SelectedPlayer
    {
        get => _selectedPlayer;
        set => this.RaiseAndSetIfChanged(ref _selectedPlayer, value);
    }

    private void Restart()
    {
        
    }
}
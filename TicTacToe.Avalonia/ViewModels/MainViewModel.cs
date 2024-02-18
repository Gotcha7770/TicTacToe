using System;
using System.Collections.Generic;
using System.Reactive;
using ReactiveUI;
using TicTacToe.Models;
using TicTacToe.Models.AI;

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
    private readonly ObservableAsPropertyHelper<GameViewModel> _gameViewModel;

    private GameMode _gameMode;
    private Symbol _selectedPlayer;
    
    public MainViewModel()
    {
        _gameViewModel = this.WhenAnyValue(
            x => x.SelectedGameMode,
            x => x.SelectedPlayer, 
            CreateGame) //TODO: Dispose old game? scoped DI?
            .ToProperty(this, x => x.GameViewModel);

        RestartCommand = ReactiveCommand.Create(Restart);
        SetPlayerCommand = ReactiveCommand.Create<Symbol>(x => SelectedPlayer = x);
    }

    public ReactiveCommand<Unit, Unit> RestartCommand { get; set; }
    public ReactiveCommand<Symbol, Unit> SetPlayerCommand { get; set; }
    public IReadOnlyCollection<GameMode> GameModes { get; } = Enum.GetValues<GameMode>();
    public GameViewModel GameViewModel => _gameViewModel.Value;

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

    private void Restart() => GameViewModel.Restart();

    private GameViewModel CreateGame(GameMode mode, Symbol selectedPlayer)
    {
        var (x, o) = (SimpleAiPlayer.FromX(), SimpleAiPlayer.FromO());
        return new GameViewModel(x, o, new BoardSize(3, 3));
    }
}
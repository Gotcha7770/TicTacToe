using System;
using System.Reactive;
using Microsoft.Extensions.Configuration;
using ReactiveUI;
using Splat;
using TicTacToe.ChatGPT;
using TicTacToe.Models;
using TicTacToe.Models.AI;

namespace TicTacToe.Avalonia.ViewModels;

public enum GameMode
{
    Easy,
    Medium,
    Hard,
    ChatGPT,
    HotSeat
}

public class MainViewModel : ReactiveObject
{
    private readonly IConfiguration _configuration;
    private readonly ObservableAsPropertyHelper<GameViewModel> _gameViewModel;

    private GameMode _gameMode;
    private Symbol _selectedPlayer;
    
    public MainViewModel()
    {
        _configuration = Locator.Current.GetService<IConfiguration>();
        _gameViewModel = this.WhenAnyValue(
            x => x.SelectedGameMode,
            x => x.SelectedPlayer, 
            CreateGame) //TODO: Dispose old game? scoped DI?
            .ToProperty(this, x => x.GameViewModel);

        RestartCommand = ReactiveCommand.Create(Restart);
        SetPlayerCommand = ReactiveCommand.Create<Symbol>(x => SelectedPlayer = x);
        
        SimpleAiStrategy.Timeout = TimeSpan.FromSeconds(0.2);
        MinimaxStrategy.Timeout = TimeSpan.FromSeconds(0.2);
    }

    public ReactiveCommand<Unit, Unit> RestartCommand { get; set; }
    public ReactiveCommand<Symbol, Unit> SetPlayerCommand { get; set; }
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
        return mode switch
        {
            GameMode.Easy => new GameViewModel(new SimpleAiStrategy(), selectedPlayer.Reverse(), new BoardSize(3, 3)),
            //GameMode.Medium => new GameViewModel(CreateOPlayer(mode), new BoardSize(3, 3)),
            GameMode.Hard => new GameViewModel(new MinimaxStrategy(), selectedPlayer.Reverse(), new BoardSize(3, 3)),
            GameMode.ChatGPT => new GameViewModel(new ChatGptStrategy(_configuration), selectedPlayer.Reverse(), new BoardSize(3, 3)),
            GameMode.HotSeat => new GameViewModel(new BoardSize(3, 3)),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };
    }
}
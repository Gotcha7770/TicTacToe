﻿using TicTacToe.Models;

namespace TicTacToe.Core.Tests;

public class Game
{
    private Field _field;

    public Game(IPlayer one, IPlayer other)
    {
        _field = new Field();
        XPlayer = one;
        OPlayer = other;
    }

    public IPlayer XPlayer { get; }
    public IPlayer OPlayer { get; }


    public void Start()
    {
    }
}
module TicTacToe.FS.Tests.TestCasesF

open TicTacToe.FS.Domain
open TicTacToe.FS.Implementation
open Xunit

let E  = CellState.Empty
let X = Played PlayerX
let O = Played PlayerO
    
type RemainingMoves() as this = 
    inherit TheoryData<GameState, CellPosition list>()
    do  this.Add(GameState.Empty, allPositions)
        this.Add(
            [
                X; E; O;
                E; X; E;
                O; E; X
            ] |> GameState.Create,
            [
                (Center, Top)
                (Left, Middle)
                (Right, Middle)
                (Center, Bottom)
            ])
        this.Add(
            [
                X; O; X;
                X; O; O;
                O; X; X
            ] |> GameState.Create,
            [])

type IsGameWonBy() as this =
    inherit TheoryData<Player, GameState, bool>()
    do this.Add(
        PlayerX,
         [
            E; E; E;
            E; E; E;
            E; E; E
         ] |> GameState.Create,
         false)
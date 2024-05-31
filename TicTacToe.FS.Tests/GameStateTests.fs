module GameStateTests

open Xunit
open Swensen.Unquote
open TicTacToe.FS.Tests
open TicTacToe.TestCases
open TicTacToe.FS.Implementation

[<Theory>]
[<ClassData(typeof<RemainingMoves>)>]
let ``test for obtaining remaining moves`` gameState expected =
    test <@ remainingMoves gameState = expected @>
    
[<Theory>]
[<ClassData(typeof<TestCasesF.IsGameWonBy>)>]
let ``test if player won the game`` player gameState expected =
    test <@ isGameWonBy player gameState = expected @>
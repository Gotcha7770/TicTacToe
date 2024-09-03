open System
open Spectre.Console
open TicTacToe.FS.Domain
open TicTacToe.FS.Implementation
open UI

// https://github.com/AurumAS/CmdMvu/blob/main/Program.fs
// https://thomasbandt.com/model-view-update

type Model =
    | StartScreen
    | GameModeSelection
    | PlayerSelection of GameMode
    | GameStart of GameMode * Player
    | Playing of MoveResult

type Message =
    | Next
    | SelectPlayer of GameMode
    | Start of Player
    | NextMove of MoveResult
    | Exit

let view model =
    match model with
    | StartScreen ->         
         AnsiConsole.Write(UI.StartScreen)
         ConsoleKey.Enter |> wait
         Message.Next
    | GameModeSelection ->         
         let mode = AnsiConsole.Prompt(UI.GameModeSelector)
         Message.SelectPlayer mode
    | PlayerSelection _ ->         
         let player = AnsiConsole.Prompt(UI.PlayerSelector)
         Message.Start player
    | GameStart(mode, player) ->
        // match player with        
        //     | PlayerX ->
        //         let cell = AnsiConsole.Prompt(UI.Field)
        //         Message.NextMove            
        //     | PlayerO ->
        //         let player =
        let moveResult = api.NewGame() 
        Message.NextMove moveResult
    | Playing(state, moves) ->
        match moves with
        | GameTied displayInfo ->
            AnsiConsole.Write("GAME OVER - Tie")
            Message.Exit
        | GameWon (displayInfo, player) ->
            AnsiConsole.Write($"GAME WON by {player}")
            Message.Exit
        | PlayerOToMove (displayInfo, nextMoves) ->
        | PlayerXToMove (displayInfo, nextMoves) ->

let update model message =
    match (model, message) with
    | StartScreen, Next -> GameModeSelection |> Some
    | GameModeSelection, SelectPlayer(mode) -> PlayerSelection mode |> Some
    | PlayerSelection(mode), Start(player) -> GameStart(mode, player) |> Some
    | GameStart(mode, player), NextMove(moves) -> Playing(moves) |> Some
    | Playing _, NextMove(moves) -> Playing(moves) |> Some
    | _, Exit -> None
    | _ -> failwith "match wrong case"

[<EntryPoint>]
let main _ =

    let rec mvuLoop model =
        match model |> view |> update model with
        | Some newModel -> newModel |> mvuLoop
        | None -> ()

    mvuLoop StartScreen
    Console.ReadLine() |> ignore
    0
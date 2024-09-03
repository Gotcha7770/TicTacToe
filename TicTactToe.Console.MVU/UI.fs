module UI

open System
open Spectre.Console
open Spectre.Console.Widgets.Field
open TicTacToe.FS.Domain

type GameMode =
    | Easy
    | Medium
    | Hard
    | ChatGPT
    | HotSeat

let rec wait key = 
    let input = Console.ReadKey()
    if input.Key = key then ()
    else wait key

let StartScreen =
    let figlet = FigletText("TicTacToe")
    figlet.Color <- Color.Blue
    
    Layout()
        .SplitRows(
            Layout(Align.Center(figlet, VerticalAlignment.Bottom)),
            Layout(Align.Center(Markup("Press ENTER to continue..."), VerticalAlignment.Top)))
        
        
let GameModeSelector =
    let selection = SelectionPrompt<GameMode>()
    selection.Title <- "Choose Game Mode"
    selection.AddChoices(
            GameMode.Easy,
            GameMode.Medium,
            GameMode.Hard)
    
let PlayerSelector =
    let selection = SelectionPrompt<Player>()
    selection.Title <- "Choose Game Mode"
    selection.AddChoices(
            PlayerX,
            PlayerO)

let EmptyField =
    let field = Field()
    field.Size <- Size(3, 3)
    field.CellSize <- 5
    field.Background <- Color.LightSeaGreen
    field.BorderColor <- Color.Turquoise4
    field

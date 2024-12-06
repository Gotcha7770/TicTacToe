module TicTacToe.FS.Implementation

open TicTacToe.FS.Domain

/// the list of all horizontal positions
let allHorizontalPositions = [Left; HorizontalPosition.Center; Right]

/// the list of all vertical positions
let allVerticalPositions = [Top; VerticalPosition.Middle; Bottom]

/// the list of all positions on board
let allPositions = [
    for v in allVerticalPositions do
        for h in allHorizontalPositions do
            yield (h, v)
    ]

/// private implementation of game state
type GameState = { Cells : Cell list } with
    static member Empty = {
        Cells =
            allPositions
            |> List.map Cell.Empty
    }
    
    static member Create cellStates = {
        Cells = [
            for state in cellStates do
                for position in allPositions do
                    yield { Position = position; State = state }
        ]
    }

/// A type to store the list of cell positions in a line
type Line = Line of CellPosition list

/// a list of the eight lines to check for 3 in a row
let linesToCheck =

    let horizontalLines = [
        for v in allVerticalPositions do
            Line [for h in allHorizontalPositions do (h, v)]            
    ]
    
    let verticalLines = [
        for h in allHorizontalPositions do
            Line [for v in allVerticalPositions do (h, v)]
    ]

    [
        yield! horizontalLines
        yield! verticalLines
        yield Line [Left,Top; Center,Middle; Right,Bottom]
        yield Line [Left,Bottom; Center,Middle; Right,Top]
    ]

/// get the DisplayInfo from the gameState
let getDisplayInfo gameState = {
    DisplayInfo.Cells = gameState.Cells
    }

/// get the cell corresponding to the cell position
let getCell gameState positionToFind = 
    gameState.Cells 
    |> List.find (fun x -> x.Position = positionToFind)

/// Update a particular cell in the GameState
/// and return a new GameState
let internal updateCell newCell gameState =
    let substituteNewCell oldCell =
        if oldCell.Position = newCell.Position then
            newCell
        else
            oldCell

    // get a copy of the cells, with the new cell swapped in
    let newCells = gameState.Cells |> List.map substituteNewCell

    { gameState with Cells = newCells }

/// Return true if the game was won by the specified player
let internal isGameWonBy player gameState = 
    let cellWasPlayedBy playerToCompare cell = 
        match cell.State with
        | Played player -> player = playerToCompare
        | Empty -> false

    let lineIsAllSamePlayer player (Line positions) = 
        positions 
        |> List.map (getCell gameState)
        |> List.forall (cellWasPlayedBy player)

    linesToCheck
    |> List.exists (lineIsAllSamePlayer player)
    
/// Return true if all cells have been played
let internal isGameTied gameState = 
    let cellWasPlayed cell = 
        match cell.State with
        | Played _ -> true
        | Empty -> false

    gameState.Cells
    |> List.forall cellWasPlayed
    
/// determine the remaining moves 
let internal remainingMoves gameState =
    let playableCell cell = 
        match cell.State with
        | Played _ -> None
        | Empty -> Some cell.Position

    gameState.Cells
    |> List.choose playableCell

/// return the other player    
let otherPlayer player = 
    match player with
    | PlayerX -> PlayerO
    | PlayerO -> PlayerX

/// return the move result case for a player 
let moveResultFor player displayInfo nextMoves = 
    match player with
    | PlayerX -> PlayerXToMove (displayInfo, nextMoves)
    | PlayerO -> PlayerOToMove (displayInfo, nextMoves)

/// given a function, a player & a gameState & a position,
/// create a NextMoveInfo with the capability to call the function
let makeNextMoveInfo f player gameState cells =
    // the capability has the player & cellPos & gameState baked in
    let capability() = f player cells gameState 
    { PositionToPlay = cells; Capability = capability }

/// given a function, a player & a gameState & a list of positions,
/// create a list of NextMoveInfos wrapped in a MoveResult
let makeMoveResultWithCapabilities f player gameState cells =
    let displayInfo = getDisplayInfo gameState
    cells
    |> List.map (makeNextMoveInfo f player gameState)
    |> moveResultFor player displayInfo
    
/// player X or O makes a move
let rec playerMove player cellPos gameState  = 
    let newCell = { Position = cellPos; State =  Played player}
    let newGameState = gameState |> updateCell newCell 
    let displayInfo = getDisplayInfo newGameState 

    if newGameState |> isGameWonBy player then
        GameWon (displayInfo, player) 
    elif newGameState |> isGameTied then
        GameTied displayInfo 
    else
        let otherPlayer = otherPlayer player 
        let moveResult = 
            newGameState 
            |> remainingMoves
            |> makeMoveResultWithCapabilities playerMove otherPlayer newGameState
        moveResult 

/// create the state of a new game
let newGame() =
    allPositions 
    |> makeMoveResultWithCapabilities playerMove PlayerX GameState.Empty

/// export the API to the application
let api = {
    NewGame = newGame 
    }
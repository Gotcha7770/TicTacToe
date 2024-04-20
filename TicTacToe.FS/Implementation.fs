module TicTacToe.FS.Implementation

open TicTacTos.FS.Domain

type GameState = {
    cells : Cell list
    }

/// the list of all horizontal positions
let allHorizPositions = [Left; HCenter; Right]

/// the list of all vertical positions
let allVertPositions = [Top; VCenter; Bottom]

/// A type to store the list of cell positions in a line
type Line = Line of CellPosition list

/// a list of the eight lines to check for 3 in a row
let linesToCheck =
    let makeHLine v = Line [for h in allHorizPositions do yield (h,v)]
    let hLines= [for v in allVertPositions do yield makeHLine v]

    let makeVLine h = Line [for v in allVertPositions do yield (h,v)]
    let vLines = [for h in allHorizPositions do yield makeVLine h]

    let diagonalLine1 = Line [Left,Top; HCenter,VCenter; Right,Bottom]
    let diagonalLine2 = Line [Left,Bottom; HCenter,VCenter; Right,Top]

    // return all the lines to check
    [
        yield! hLines
        yield! vLines
        yield diagonalLine1
        yield diagonalLine2
    ]
        
type TicTacToeAPI<'GameState>  =
    {
        newGame : NewGame<'GameState>
        playerXMoves : PlayerXMoves<'GameState>
        playerOMoves : PlayerOMoves<'GameState>
        getCells : GetCells<'GameState>
    }

/// get the cells from the gameState
let getCells gameState =
    gameState.cells
    
/// get the cell corresponding to the cell position
let getCell gameState posToFind =
    gameState.cells
    |> List.find (fun cell -> cell.pos = posToFind)

/// update a particular cell in the GameState
/// and return a new GameState
let private updateCell newCell gameState =

    // create a helper function
    let substituteNewCell oldCell =
        if oldCell.pos = newCell.pos then
            newCell
        else
            oldCell

    // get a copy of the cells, with the new cell swapped in
    let newCells = gameState.cells |> List.map substituteNewCell

    // return a new game state with the new cells
    { gameState with cells = newCells }

/// create the state of a new game
let newGame : NewGame<GameState> =
    // create initial game state with empty everything
    let gameState = { cells=[] }
    let validMoves = []
    gameState, ValidMovesForPlayerX validMoves

let playerXMoves : PlayerXMoves<GameState> =
    let newCell = { pos = cellPos; state = Played PlayerX }
    let newGameState = gameState |> updateCell newCell

// export the functions
let api = {
    newGame = newGame
    playerOMoves = playerOMoves
    playerXMoves = playerXMoves
    getCells = getCells
}
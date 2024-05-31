module TicTacToe.FS.Domain

type HorizontalPosition = Left | Center | Right
type VerticalPosition = Top | Middle | Bottom
type CellPosition = HorizontalPosition * VerticalPosition

type Player = PlayerO | PlayerX

type CellState =
    | Played of Player
    | Empty

type Cell = {
    Position : CellPosition
    State : CellState
    } with
    static member Empty position =
        { Position = position; State = Empty}

/// Everything the UI needs to know to display the board
type DisplayInfo = {
    Cells : Cell list
    }

type MoveCapability = unit -> MoveResult

/// A capability along with the position the capability is associated with.
/// This allows the UI to show information so that the user
/// can pick a particular capability to exercise.
and NextMoveInfo = {
    // the pos is for UI information only
    // the actual pos is baked into the cap.
    PositionToPlay : CellPosition
    Capability : MoveCapability
    }

/// The result of a move. It includes:
/// * The information on the current board state.
/// * The capabilities for the next move, if any.
and MoveResult =
    | PlayerXToMove of DisplayInfo * NextMoveInfo list
    | PlayerOToMove of DisplayInfo * NextMoveInfo list
    | GameWon of DisplayInfo * Player
    | GameTied of DisplayInfo

// Only the newGame function is exported from the implementation
// all other functions come from the results of the previous move
type TicTacToeAPI = {
    NewGame : MoveCapability
    }
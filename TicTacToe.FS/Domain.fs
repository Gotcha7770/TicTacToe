module TicTacTos.FS.Domain

type HorizontalPosition = Left | HCenter | Right
type VerticalPosition = Top | VCenter | Bottom

type CellPosition = HorizontalPosition * VerticalPosition

type XPosition = XPosition of CellPosition
type OPosition = OPosition of CellPosition

type Player = PlayerO | PlayerX

type CellState =
    | Played of Player
    | Empty

type Cell = {
    pos : CellPosition
    state : CellState
    }

type ValidMovesForPlayerX = XPosition list
type ValidMovesForPlayerO = OPosition list

// abstract base class
type GameState() = class end

type MoveResult =
    | PlayerXToMove of ValidMovesForPlayerX
    | PlayerOToMove of ValidMovesForPlayerO
    | GameWon of Player
    | GameTied

// the "use-cases"
type NewGame<'GameState> = 'GameState * MoveResult
type PlayerXMoves<'GameState> = 'GameState * XPosition -> 'GameState * MoveResult
type PlayerOMoves<'GameState> = 'GameState * OPosition -> 'GameState * MoveResult

type GetCells<'GameState> = 'GameState -> Cell list
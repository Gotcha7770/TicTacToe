using Microsoft.Extensions.Configuration;
using OpenAI.ChatGpt;
using OpenAI.ChatGpt.Models.ChatCompletion;
using OpenAI.ChatGpt.Modules.StructuredResponse;
using TicTacToe.Interfaces;
using TicTacToe.Models;

namespace TicTacToe.ChatGPT;

public class ChatGPTPlayer : IPlayer, IDisposable
{
    private const string PromptTemplate = """
                                          You are an AI playing a game of Tic Tac Toe as the {1} player.
                                          Your goal is to place three of your marks in a horizontal, vertical, or diagonal row to win the game.
                                          The game starts with the 'X' player making the first move.
                                          Players alternate turns, placing their mark in an empty square.
                                          You are highly skilled and strive to play the optimal move.
                                          Each square on the game board can be identified by its row (from top to bottom)
                                          and column (from left to right), both ranging from 0 to 2.
                                          The current state of the board is represented by a 3x3 grid. 'X' represents the X player pieces,
                                          'O' represents O player pieces, and '_' represents an empty space.
                                          Here's the current state of the game:\n
                                          {0}\n
                                          Reading from left to right, the columns are 0, 1, and 2, and reading from top to bottom, the rows are 0, 1, and 2.
                                          It's your turn to play as the '{1}' player. Please specify your move as a row and column number.
                                          What is your next move?
                                          """;

    private readonly OpenAiClient _client;

    public ChatGPTPlayer(Symbol symbol, IConfiguration configuration)
    {
        Symbol = symbol;
        string apiKey = configuration[$"{ChatGPTSettings.Key}:ApiKey"];
        if (apiKey is null)
            throw new ArgumentNullException(nameof(apiKey));

        _client = new OpenAiClient(apiKey);
    }

    public Symbol Symbol { get; }

    public async ValueTask<Move> GetNextMove(Field field, CancellationToken cancellationToken = default)
    {
        string prompt = string.Format(PromptTemplate, field, Symbol);
        var dialog = Dialog.StartAsSystem(prompt);
        var cell = await _client.GetStructuredResponse<Cell>(dialog, model: ChatCompletionModels.Gpt3_5_Turbo, cancellationToken: cancellationToken);

        return new Move(cell, Symbol);
    }

    public void Dispose() => _client.Dispose();
}
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TMP_Text labelCurrentPlayer;
    [SerializeField] private TMP_Text labelWhatHappened;
    [SerializeField] private Board board;
    [SerializeField] private TMP_Text labelDiceResult;
    [SerializeField] private BoardRules boardRules;
    [Space]
    [Header("Starting Values")]
    [Tooltip("Text displayed at the start to represent the initial dice result.")]
    [SerializeField] private string diceStartText = "?";
    [Tooltip("The starting position of Player 1.")]
    [SerializeField] private int p1StartPosition = 1;
    [Tooltip("The starting position of Player 2.")]
    [SerializeField] private int p2StartPosition = 1;
    [Tooltip("The starting turn of the game. Enter 1 for Player 1's turn or 2 for Player 2's turn.")]
    [SerializeField] private int startingTurn = 1;
    [Header("In Game Values")]
    [Tooltip("The text to notify the player that they must miss their turn.")]
    [SerializeField] private string missedTurnText = " había perdido el turno - no juega";
    [Tooltip("The text that displays if player 1 wins.")]
    [SerializeField] private string player1WinningMessage = " Looks like you've 'RED' the game well!";
    [Tooltip("The text that displays if player 2 wins.")]
    [SerializeField] private string player2WinningMessage = " GREEN and bear it, you've won!";
    [Tooltip("The text leading up to the number obtained on the dice roll.")]
    [SerializeField] private string youRolledText = "You rolled a ";
    [Tooltip("The text leading up to the tile that the player will land on.")]
    [SerializeField] private string yourGoingToText = ", and ended up on tile ";
    [Tooltip("The ammount of seconds a player stays on a special tile, before moving to their final destination.")]
    [SerializeField] private int secondsToWaitBeforeSpecialMove = 1;
    [Tooltip("The ammount of seconds between the end of one round, and the begining of another.")]
    [SerializeField] private int secondsBetweenRounds = 2;
    [Tooltip("The tile that the player must land on to win. This should be the last, or highest numbered tile.")]
    [SerializeField] private int winningTile = 36;
    [Tooltip("The lowest number possible to obtain during the dice role.")]
    [SerializeField] private int lowestDiceNumber = 1;
    [Tooltip("The highest number possible to obtain during the dice role.")]
    [SerializeField] private int highestDiceNumber = 6;


    private bool gameState = false;
    private string currentPlayerStartText = "";
    private string whatHappenedStartText = "";
    private bool player1MissesTurn = false;
    private bool player2MissesTurn = false;
    public int player1Position = 1;
    private int player2Position = 1;
    public int turn = 1;
    private bool player1Turn = true;
    private bool gameOver = false;
    private int diceResult = 0;
    private bool waitingForDice = false;

    private void Start()
    {
        boardRules.Initialize();
        SetUpBoard(p1StartPosition, p2StartPosition, startingTurn, gameState, currentPlayerStartText, whatHappenedStartText, diceStartText);
        StartCoroutine(StartRound());
    }

    public void SetUpBoard(
        int p1StartPosition, 
        int p2StartPosition, 
        int startingTurn, 
        bool turnState,
        string currentPlayerStartText,
        string whatHappenedStartText, 
        string diceStartText)
    {
        labelCurrentPlayer.text = currentPlayerStartText;
        labelWhatHappened.text = whatHappenedStartText;
        labelDiceResult.text = diceStartText;

        player1Position = p1StartPosition;
        player2Position = p2StartPosition;
        turn = startingTurn;
        gameOver = turnState;
        //TAREA USAR ESTE METODO EN VEZ DEL START
    }

    private IEnumerator StartRound()  // ---> TAREA: Refactorear este método para que sea mas "clean code"
    {
        // Set up the round
        ResetRound();
        
        // Wait for dice roll
        while (diceResult == 0)
        {
            yield return new WaitForEndOfFrame();
        }
        waitingForDice = false;

        //Player 1s turn
        if (player1Turn && !player1MissesTurn)
        {
            MovePlayer(ref player1Position);
            yield return new WaitForSeconds(secondsToWaitBeforeSpecialMove);
            CheckForSpecialTile(ref player1Position);
            CheckForWinner(ref player1Position);
        }

        //Player 2s turn
        else if (!player1Turn && !player2MissesTurn)
        {
            MovePlayer(ref player2Position);
            yield return new WaitForSeconds(secondsToWaitBeforeSpecialMove);
            CheckForSpecialTile(ref player2Position);
            CheckForWinner(ref player2Position);
        }

        //Player misses a turn
        else if (player1MissesTurn || player2MissesTurn)
        {
            labelWhatHappened.text = missedTurnText;
            player1MissesTurn = false;
            player2MissesTurn = false;
        }

        // End turn
        player1Turn = !player1Turn;

        //Start a new round
        if (!gameOver)
        {
            yield return new WaitForSeconds(secondsBetweenRounds);
            StartCoroutine(StartRound());
        }
    }

    private void ResetRound()
    {
        labelDiceResult.text = diceStartText;
        diceResult = 0;
        labelWhatHappened.text = whatHappenedStartText;
        turn = player1Turn ? 1 : 2;
        labelCurrentPlayer.text = turn.ToString();
        waitingForDice = true;
    }

    private void MovePlayer(ref int posicionJugador)
    {
        posicionJugador = Math.Min(winningTile, posicionJugador + diceResult);
        labelWhatHappened.text = youRolledText + diceResult.ToString() + yourGoingToText + posicionJugador.ToString();
        board.MovePlayerToCell(turn, posicionJugador);
    }

    private void CheckForWinner(ref int playerPosition)
    {
        gameOver = playerPosition == winningTile;
        if (player1Position == winningTile)
            labelWhatHappened.text = player1WinningMessage;
        else if (player2Position == winningTile)
            labelWhatHappened.text = player2WinningMessage;
    }

    private void CheckForSpecialTile(ref int playerPosition)
    {
        playerPosition = IfSpecialDetermineNewPosition(turn, playerPosition);
        board.MovePlayerToCell(turn, playerPosition);
    }

    private int IfSpecialDetermineNewPosition(int idJugador, int posicionJugador)
    {
        BoardRuleResult result = new BoardRuleResult(posicionJugador);

        foreach (var rule in boardRules.rules)
        {
            if (rule.IsCompatible(posicionJugador))
                result = rule.Act(idJugador, posicionJugador);
        }

        player1MissesTurn = player1MissesTurn || result.jugador1PierdeTurno;
        player2MissesTurn = player2MissesTurn || result.jugador2PierdeTurno;

        labelWhatHappened.text += result.textWhatHappened;

        return result.newPosition;
    }

    // This method is referenced by an On Click Event assigned to the dice in the inspector.
    private void OnDiceRoll()
    {
        if (!waitingForDice)
            return;

        System.Random r = new System.Random();

        int resultado = r.Next(lowestDiceNumber, (highestDiceNumber+1));
        labelDiceResult.text = resultado.ToString();

        diceResult = resultado;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoardRules", menuName = "ScriptableObjects/BoardRules", order = 1)]
public class BoardRules : ScriptableObject
{

    public List<BoardRule> rules = new List<BoardRule>();

    private Dictionary<int, int> goForwardCells = new Dictionary<int, int>();
    private Dictionary<int, int> goBackwardCells = new Dictionary<int, int>();
    private int[] loseTurnCells = new int[] { };
    private int[] throwAgainCells = new int[] { };
    public void Initialize()
    {
        goForwardCells.Add(2, 21);
        goForwardCells.Add(7, 11);
        goForwardCells.Add(14, 22);
        goForwardCells.Add(22, 24);

        goBackwardCells.Add(12, 1);
        goBackwardCells.Add(25, 9);
        goBackwardCells.Add(30, 27);
        goBackwardCells.Add(33, 20);

        loseTurnCells = new int[] { 5, 18 };

        throwAgainCells = new int[] { 31 };

        rules.Add(new GoForward(goForwardCells));
        rules.Add(new GoBackward(goBackwardCells));
        rules.Add(new LoseTurn(loseTurnCells));
        rules.Add(new ThrowAgain(throwAgainCells));
    }
}

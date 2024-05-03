using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Transform[] Cells;
    [SerializeField] private Transform Player1Token;
    [SerializeField] private Transform Player2Token;

    public void MovePlayerToCell(int player, int cell)
    {
        if (player == 1)
        {
            Player1Token.SetParent(Cells[cell - 1]);
            Player1Token.localPosition = Vector2.zero;
        }
        else
        {
            Player2Token.SetParent(Cells[cell - 1]);
            Player2Token.localPosition = Vector2.zero;
        }
    }
}


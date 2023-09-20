using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private Piece[,] chessBoard = new Piece[8, 8];
    [SerializeField] List<Square> squares;

    public void MovePiece()
    {
        
    }
    public bool ValidateMovement(Piece piece, Vector2Int nextPosition)
    {
        bool obstacleFound = false;
        Vector2Int currentPosition = piece.CurrentPositionInTable;

        int deltaX = currentPosition.x - nextPosition.x;
        int deltaY = currentPosition.y - nextPosition.y;

        if (piece is Knight)
        {
            obstacleFound = false;
        }
        else
        {
            for (int i = currentPosition.x + Sign(deltaX), j = currentPosition.y + Sign(deltaY); (i != nextPosition.x || j != nextPosition.y); i += Sign(deltaX), j += Sign(deltaY))
            {
                if (i != nextPosition.x || j != nextPosition.y)
                {
                    break;
                }
                if (chessBoard[i, j] != null)
                {
                    obstacleFound = true;
                    break;
                }
            }
        }

        return (!obstacleFound && piece.ValidateMovement(nextPosition));
    }

    int Sign(int number)
    {
        if (number > 0)
        {
            return 1;
        }
        else if (number < 0)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}

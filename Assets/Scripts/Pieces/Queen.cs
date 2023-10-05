using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    private void Start()
    {
        MovementRange = new Vector2Int(7, 7);
    }

    public override bool ValidateMovement(Vector2Int nextPosition)
    {
        int deltaX = Mathf.Abs(nextPosition.x - CurrentPositionInBoard.x);
        int deltaY = Mathf.Abs(nextPosition.y - CurrentPositionInBoard.y);

        if ((deltaX > 0 && deltaY == 0) || (deltaX == 0 && deltaY > 0))
        {
            return true;
        }

        if (deltaX == deltaY)
        {
            return true;
        }

        return false;
    }
}


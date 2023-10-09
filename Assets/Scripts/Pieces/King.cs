using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    private void Start()
    {
        movementRange = new Vector2Int(1, 1);
    }

    public override bool ValidateMovement(Vector2Int nextPosition)
    {
        int deltaX = Mathf.Abs(nextPosition.x - CurrentPositionInBoard.x);
        int deltaY = Mathf.Abs(nextPosition.y - CurrentPositionInBoard.y);

        return ((deltaX <= movementRange.x) && (deltaY <= movementRange.y));
    }
}

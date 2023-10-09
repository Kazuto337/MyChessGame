using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    private void Start()
    {
        movementRange = new Vector2Int(7, 7);
    }

    public override bool ValidateMovement(Vector2Int nextPosition)
    {
        float deltaX = Mathf.Abs(nextPosition.x - CurrentPositionInBoard.x);
        float deltaY = Mathf.Abs(nextPosition.y - CurrentPositionInBoard.y);

        return (deltaX == 0 && deltaY > 0) || (deltaX > 0 && deltaY == 0);
    }
}

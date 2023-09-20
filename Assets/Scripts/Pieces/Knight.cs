using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    void Start()
    {
        MovementRange = new Vector2Int(1, 2);
    }

    public override bool ValidateMovement(Vector2Int nextPosition)
    {
        int deltaX = Mathf.Abs(nextPosition.x - CurrentPositionInTable.x);
        int deltaY = Mathf.Abs(nextPosition.y - CurrentPositionInTable.y);

        return (deltaX == MovementRange.x && deltaY == MovementRange.y) || (deltaX == MovementRange.y && deltaY == MovementRange.x);
    }
}

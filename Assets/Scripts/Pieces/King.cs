using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    private void Start()
    {
        MovementRange = new Vector2Int(1, 1);
    }

    public override bool ValidateMovement(Vector2Int nextPosition)
    {
        int deltaX = Mathf.Abs(nextPosition.x - CurrentPositionInTable.x);
        int deltaY = Mathf.Abs(nextPosition.y - CurrentPositionInTable.y);

        return (deltaX >= MovementRange.x) && (deltaY >= MovementRange.y) && (deltaX > 0 || deltaY > 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    private void Start()
    {
        MovementRange = new Vector2Int(0, 1);
    }
    public override bool ValidateMovement(Vector2Int nextPosition)
    {
        float deltaX = Mathf.Abs(nextPosition.x - CurrentPositionInTable.x);
        float deltaY = Mathf.Abs(nextPosition.y - CurrentPositionInTable.y);

        return (deltaX == MovementRange.x && deltaY == MovementRange.y);
    }
}

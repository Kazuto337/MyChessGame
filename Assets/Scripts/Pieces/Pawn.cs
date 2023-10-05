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
        float deltaX = nextPosition.x - CurrentPositionInBoard.x;
        float deltaY = nextPosition.y - CurrentPositionInBoard.y;

        if (team == PiecesTeams.Black)
        {
            deltaY *= -1;
        }

        return (deltaX == MovementRange.x && deltaY == MovementRange.y);
    }
}

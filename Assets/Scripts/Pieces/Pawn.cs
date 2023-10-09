using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    private bool firstMovement;
    private void Start()
    {
        firstMovement = true;
    }
    private void Update()
    {
        switch (firstMovement)
        {
            case true:
                movementRange = new Vector2Int(0, 2);
                break;

            case false:
                movementRange = new Vector2Int(0, 1);
                break;
        }
    }
    public override bool ValidateMovement(Vector2Int nextPosition)
    {
        float deltaX = nextPosition.x - CurrentPositionInBoard.x;
        float deltaY = nextPosition.y - CurrentPositionInBoard.y;

        if (team == PiecesTeams.Black)
        {
            deltaY *= -1;
        }

        if (deltaX == movementRange.x && (deltaY >= 1 && deltaY <= movementRange.y))
        {
            firstMovement = false;
            return true;
        }
        else return false;

    }
}

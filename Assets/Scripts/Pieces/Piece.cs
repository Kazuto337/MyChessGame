using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PiecesTeams
{
    White = 1, Black = 2,
}
public abstract class Piece : MonoBehaviour
{
    private Vector2Int movementRange;
    private Vector2Int currentPositionInTable;
    [SerializeField] protected PiecesTeams team;

    public Vector2Int MovementRange { get => movementRange; set => movementRange = value; }
    public Vector2Int CurrentPositionInTable { get => currentPositionInTable; set => currentPositionInTable = value; }
    public PiecesTeams Team { get => team; }

    public virtual bool ValidateMovement(Vector2Int nextPosition)
    {
        return true;
    }
}

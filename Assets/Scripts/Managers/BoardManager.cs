using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoardManager : MonoBehaviour
{
    public enum SelectionState
    {
        selection = 0,
        movement
    }

    [SerializeField] List<Square> gameSquares;
    private Square[,] chessBoard = new Square[8, 8];

    [Header("GameStatus")]

    [SerializeField] List<Square> squares;
    Square currentSquare;
    [SerializeField] SelectionState selectionState = SelectionState.selection;

    PlayerInput playerInput;
    InputAction interactAction;
    Camera _camera;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        interactAction = playerInput.actions["Interact"];
        _camera = Camera.main;

        LoadChessBoard();
    }

    private void OnEnable()
    {
        interactAction.performed += SelectSquare;
    }

    public void SelectSquare(InputAction.CallbackContext context)
    {

        Vector2 touchPosition = new Vector2();
        if (Touchscreen.current != null)
        {
            touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else if (Mouse.current != null)
        {
            touchPosition = Mouse.current.position.ReadValue();
        }

        Ray ray = _camera.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, 100f))
        {
            return;
        }

        if (selectionState == SelectionState.selection)
        {
            if (hit.collider.TryGetComponent<Square>(out Square outputSquare))
            {
                SelectSquare(outputSquare);
            }
            else
            {
                DeselectSquare();
            }
        }
        else
        {
            if (hit.collider.TryGetComponent<Square>(out Square outputSquare))
            {
                SelectSquare(outputSquare, currentSquare.CurrentPiece);
            }
            else
            {
                selectionState = SelectionState.selection;
                DeselectSquare();
            }
        }
    }

    private void DeselectSquare()
    {
        currentSquare.isSelected = false;
        currentSquare = null;
    }

    private void SelectSquare(Square selectedSquare)
    {
        if (selectedSquare == currentSquare)
        {
            DeselectSquare();
        }

        if (currentSquare != null)
        {
            currentSquare.isSelected = false;
        }
        currentSquare = selectedSquare;
        currentSquare.isSelected = true;

        if (currentSquare.CurrentPiece != null)
        {
            selectionState = SelectionState.movement;
            DrawPossibleMovements();
        }
    }
    private void SelectSquare(Square nextSquare, Piece currentPiece)
    {
        if (nextSquare == currentSquare)
        {
            selectionState = SelectionState.selection;

            DeselectSquare();
            return;
        }

        if (!currentPiece.ValidateMovement(nextSquare.BoardCoordinate) || !ValidateMovement(currentPiece, nextSquare.BoardCoordinate))
        {
            Debug.LogWarning("INVALID MOVE");

            selectionState = SelectionState.selection;

            DeselectSquare();
            return;
        }

        currentPiece.gameObject.transform.position = nextSquare.transform.position;

        nextSquare.ChangeCurrentPiece(currentPiece);
        currentPiece.CurrentPositionInBoard = nextSquare.BoardCoordinate;

        currentSquare.ClearPiece();
        selectionState = SelectionState.selection;

        DeselectSquare();
    }

    private void DrawPossibleMovements()
    {
        //Do Something
    }

    public bool ValidateMovement(Piece piece, Vector2Int nextPosition)
    {
        Vector2Int initialPosition = piece.CurrentPositionInBoard;
        int deltaX = nextPosition.x - initialPosition.x;
        int deltaY = nextPosition.y - initialPosition.y;

        if (chessBoard[nextPosition.x, nextPosition.y].CurrentPiece != null && chessBoard[nextPosition.x, nextPosition.y].CurrentPiece.Team == piece.Team)
        {
            return false;
        }

        if (piece is Knight)
        {
            return true;
        }

        if (deltaX != 0 && deltaY == 0)
        {

            for (int i = initialPosition.x + Sign(deltaX); i < nextPosition.x; i += Sign(deltaX))
            {
                if (chessBoard[i, initialPosition.y].CurrentPiece != null)
                {
                    Debug.LogWarning("INVALID MOVE - OBSTACLE FOUND AT [" + i + " , " + initialPosition.y + "]");
                    return false;
                }
            }

            return true;
        }
        else if (deltaX == 0 && deltaY != 0)
        {
            for (int j = initialPosition.y + Sign(deltaY); j < nextPosition.y; j += Sign(deltaY))
            {
                if (chessBoard[initialPosition.x, j].CurrentPiece != null)
                {
                    Debug.LogWarning("INVALID MOVE - OBSTACLE FOUND AT [" + initialPosition.x+" , "+ j + "]");
                    return false;
                }
            }

            return true;
        }
        else if (deltaX != 0 && deltaY != 0)
        {
            for (int i = initialPosition.x + Sign(deltaX) , j = initialPosition.y + Sign(deltaY); i != nextPosition.x || j != nextPosition.y; i += Sign(deltaX), j+= Sign(deltaY))
            {
                if (chessBoard[i, j].CurrentPiece != null)
                {
                    Debug.LogWarning("INVALID MOVE - OBSTACLE FOUND AT: [" + i+" , "+ j + "]");
                    return false;
                }
            }

            return true;
        }

        return true;
    }

    private void LoadChessBoard()
    {
        int squareIndex = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                chessBoard[i, j] = gameSquares[squareIndex];
                squareIndex++;
            }
            if (squareIndex == gameSquares.Count)
            {
                break;
            }
        }
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

    private void OnDisable()
    {
        interactAction.performed -= SelectSquare;
    }
}

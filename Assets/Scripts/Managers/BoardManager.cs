using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoardManager : MonoBehaviour
{
    public enum SelectionState
    {
        selection=0,
        movement
    }

    [Header("GameStatus")]
    private Piece[,] chessBoard = new Piece[8, 8];
    [SerializeField] List<Square> squares;
    Square currentSquare;
    SelectionState selectionState = SelectionState.selection;

    PlayerInput playerInput;
    InputAction interactAction;
    Camera _camera;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        interactAction = playerInput.actions["Interact"];
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        interactAction.performed += SelectSquare;
    }
    private void Start()
    {
        foreach (Square item in squares)
        {
            int x = item.BoardCoordinate.x;
            int y = item.BoardCoordinate.y;

            chessBoard[x, y] = item.currentPiece;
        }
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

        if (hit.collider.TryGetComponent<Square>(out Square outputSquare))
        {
            SelectSquare(outputSquare);
        }
        else
        {
            DeselectSquare();
        }
    }

    private void DeselectSquare()
    {
        currentSquare.isSelected = false;
        currentSquare = null;
    }
    private void SelectSquare(Square selecetedSquare)
    {
        if (selecetedSquare == currentSquare)
        {
            DeselectSquare();
        }

        if (currentSquare != null)
        {
            currentSquare.isSelected = false; 
        }
        currentSquare = selecetedSquare;
        currentSquare.isSelected = true;

        if (selecetedSquare.currentPiece != null)
        {

        }
    }

    public bool ValidateMovement(Piece piece, Vector2Int nextPosition)
    {
        bool obstacleFound = false;
        Vector2Int currentPosition = piece.CurrentPositionInTable;

        int deltaX = currentPosition.x - nextPosition.x;
        int deltaY = currentPosition.y - nextPosition.y;

        if (piece is Knight)
        {
            obstacleFound = false;
        }
        else
        {
            for (int i = currentPosition.x + Sign(deltaX), j = currentPosition.y + Sign(deltaY); (i != nextPosition.x || j != nextPosition.y); i += Sign(deltaX), j += Sign(deltaY))
            {
                if (i != nextPosition.x || j != nextPosition.y)
                {
                    break;
                }
                if (chessBoard[i, j] != null)
                {
                    obstacleFound = true;
                    break;
                }
            }
        }

        return (!obstacleFound && piece.ValidateMovement(nextPosition));
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

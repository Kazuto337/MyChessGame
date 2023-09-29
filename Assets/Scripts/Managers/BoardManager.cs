using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoardManager : MonoBehaviour
{
    private Piece[,] chessBoard = new Piece[8, 8];
    [SerializeField] List<Square> squares;
    Square currentSquareSelected;

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

    public void SelectSquare(InputAction.CallbackContext context)
    {
        print("Click");

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

        if (Physics.Raycast(ray, out hit, 100f , 3))
        {
            print(hit.collider.name);
            if (currentSquareSelected != null)
            {
                if (hit.collider.gameObject != currentSquareSelected.gameObject)
                {
                    currentSquareSelected.isSelected = false; 
                }
            }

            if (currentSquareSelected != hit.collider.GetComponent<Square>() || currentSquareSelected == null)
            {                
                currentSquareSelected = hit.collider.GetComponent<Square>();
                currentSquareSelected.isSelected = true;
            }
            else if (hit.collider.gameObject == gameObject)
            {
                currentSquareSelected = null;
            }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField] private Vector2Int boardCoordinate;
    public bool isSelected;
    [SerializeField] private Piece currentPiece;
    Material outlineShader;
    Vector3 initialPosition;

    public Vector2Int BoardCoordinate { get => boardCoordinate;}
    public Piece CurrentPiece { get => currentPiece;}

    private void Start()
    {
        outlineShader = GetComponent<Renderer>().materials[1];
        initialPosition = transform.position;

        if (currentPiece != null)
        {
            currentPiece.CurrentPositionInBoard = boardCoordinate; 
        }
    }

    public void ChangeCurrentPiece(Piece newPiece)
    {
        currentPiece = newPiece;
    }
    public void ClearPiece()
    {
        currentPiece = null;
    }

    private void Update()
    {
        switch (isSelected)
        {
            case true:
                outlineShader.SetFloat("_OutlineSize", 1.07f);
                transform.position = new Vector3(initialPosition.x , initialPosition.y + 0.5f , initialPosition.z );
                break;

            case false:
                outlineShader.SetFloat("_OutlineSize", 0f);
                transform.position = initialPosition;
                break;
        }
    }
}

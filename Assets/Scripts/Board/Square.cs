using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public Vector2Int boardCoordinate;
    public bool isSelected;
    [SerializeField] Piece currentPiece;
    Material outlineShader;
    Vector3 initialPosition;

    private void Start()
    {
        outlineShader = GetComponent<Renderer>().materials[1];
        initialPosition = transform.position;
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

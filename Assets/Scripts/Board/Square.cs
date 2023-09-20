using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public Vector2Int boardCoordinate;
    bool isSelected;
    Piece currentPiece;
    Material outlineShader;

    private void Start()
    {
        outlineShader = GetComponent<Renderer>().materials[1];
    }

    public void Update()
    {
        if (isSelected)
        {
            outlineShader.SetFloat("_OutlineSize" , 1.07f);
        }
        else
        {
            outlineShader.SetFloat("_OutlineSize", 0f);
        }
    }
    public void OnSelected()
    {
        if (currentPiece != null)
        {
            isSelected = true;
        }
        else isSelected = false;
    }
}

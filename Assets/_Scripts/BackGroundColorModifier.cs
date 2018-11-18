using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundColorModifier : MonoBehaviour {

    public Renderer[] backGroungColorMesh;
    public Color[] meshColors;
    public Color cameraSolidColor;
   
	void Start () 
    {
        for (int i = 0; i < backGroungColorMesh.Length; i++)
        {
            Material[] color = backGroungColorMesh[i].materials;
            color[0].color = meshColors[i];
            backGroungColorMesh[i].materials = color;
        }

        Camera.main.backgroundColor = cameraSolidColor;
    }
}

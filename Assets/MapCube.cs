using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCube 
{
    Vector3 position;
    MapCube(Vector2 pos)
    {
        position.x = pos.x;
        position.y = 1;
        position.z = pos.y;
    }
    public void Draw()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawCube(position, Vector3.one);
    }


}

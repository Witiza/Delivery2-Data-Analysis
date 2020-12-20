using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCube 
{
    public Vector3 position;
    public MapCube(Vector2 pos)
    {
        position.x = pos.x;
        position.y = 1;
        position.z = pos.y;
    }
    public void Draw()
    {

    }


}

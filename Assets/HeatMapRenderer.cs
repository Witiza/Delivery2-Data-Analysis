
using UnityEngine;
using UnityEditor;

[SerializeField]
[ExecuteAlways]
public class HeatMapRenderer : MonoBehaviour
{
    MapCube[,] heat_matrix = null;
    Vector2 size = new Vector2(100, 100);
    Mesh plane;
    private void Start()
    {
    }
    
   

    public void Draw(MapCube[,] heat_matrix)
    {
        this.heat_matrix = heat_matrix;

    }


}

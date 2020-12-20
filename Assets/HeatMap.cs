using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class HeatMap : EditorWindow
{
    //---------HeatMap variables---------------------//
    Vector2 size;
    Vector2 middlepoint = Vector2.zero;
    GameObject[,] heat_matrix;
    public HeatMapRenderer renderer;
    GameObject cube_prefab;


    [MenuItem("Window/Data Analysis/HeatMap")]
    public  void ShowWindow()
    {
        GetWindow<HeatMap>("HeatMap");
    }

    private void OnGUI()
    {
        GUILayout.Label(SceneManager.GetActiveScene().name, EditorStyles.boldLabel);

        size = EditorGUILayout.Vector2Field("Map Size", size);
        middlepoint = EditorGUILayout.Vector2Field("Middle Point", middlepoint);
        cube_prefab = (GameObject)EditorGUILayout.ObjectField(cube_prefab, typeof(GameObject), true);

        if (GUILayout.Button("Generate"))
        {
            DeleteMap();
            GenerateHeatmap();
        }
        if (GUILayout.Button("Delete"))
        {
            DeleteMap();
        }

        //Debug.Log(renderer.name);
    }

    private void GenerateHeatmap()
    {
        heat_matrix = null;
        heat_matrix = new GameObject[(int)size.x, (int)size.y];
        for (int i = 0;i<size.x;i++)
        { 
            for(int j = 0;j<size.y;j++)
            {
                Vector2 pos = middlepoint;
                pos.x += i-size.x/2;
                pos.y += j-size.y/2;
                heat_matrix[i, j] = Instantiate(cube_prefab);
                heat_matrix[i, j].GetComponent<MapCube>().GenerateColor();
                heat_matrix[i, j].GetComponent<MapCube>().AdjoustPosition(pos);
            }
        }
    }
    private void DeleteMap()
    {
        if (heat_matrix != null)
        {
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    DestroyImmediate(heat_matrix[i, j]);
                }
            }
            heat_matrix = null;
        }

    }



}

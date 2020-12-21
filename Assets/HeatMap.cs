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
    public int events_per_pos;
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
        events_per_pos = EditorGUILayout.IntField("Max events per position", events_per_pos);
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
        size.x = (int)size.x;
        size.y = (int)size.y;

        if (size.x > 0 && size.y > 0)
        {
            Debug.Log("HEYO");
            heat_matrix = null;
            heat_matrix = new GameObject[(int)size.x, (int)size.y];
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    Vector2 pos = middlepoint;
                    pos.x += i - size.x / 2;
                    pos.y += j - size.y / 2;
                    heat_matrix[i, j] = Instantiate(cube_prefab);
                    heat_matrix[i, j].GetComponent<MapCube>().GenerateColor();
                    heat_matrix[i, j].GetComponent<MapCube>().AdjoustPosition(pos);
                }
            }
        }
        else
        {
            Debug.LogError("The heatmap needs a minimum size");
        }
        
    }
    private void DeleteMap()
    {
        if (heat_matrix != null && heat_matrix.Length > 0)
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

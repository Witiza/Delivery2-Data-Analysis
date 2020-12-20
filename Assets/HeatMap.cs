using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class HeatMap : EditorWindow
{
    //---------HeatMap variables---------------------//
    Vector2 size;
    Vector2 middlepoint = Vector2.zero;
    MapCube[,] heat_matrix;
    public HeatMapRenderer renderer;


    [MenuItem("Window/Data Analysis/HeatMap")]
    public static void ShowWindow()
    {

        GetWindow<HeatMap>("HeatMap");
    }

    private void OnGUI()
    {
        GUILayout.Label(SceneManager.GetActiveScene().name, EditorStyles.boldLabel);

        size = EditorGUILayout.Vector2Field("Map Size", size);
        middlepoint = EditorGUILayout.Vector2Field("Middle Point", middlepoint);

        if (GUILayout.Button("Generate"))
        {
            GenerateHeatmap();
        }
        renderer = (HeatMapRenderer)EditorGUILayout.ObjectField(renderer, typeof(HeatMapRenderer), true);
        
        //Debug.Log(renderer.name);
    }

    private void GenerateHeatmap()
    {
        heat_matrix = null;
        heat_matrix = new MapCube[(int)size.x, (int)size.y];

        for (int i = 0;i<size.x;i++)
        { 
            for(int j = 0;j<size.y;j++)
            {
                Vector2 pos = middlepoint;
                pos.x += i-size.x/2;
                pos.y += j-size.y/2;
                heat_matrix[i, j] = new MapCube(pos);
            }
        }
        renderer.Draw(heat_matrix);
    }



}

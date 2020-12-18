using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class HeatMap : EditorWindow
{
    //---------HeatMap variables---------------------//
    Vector2 size;
    Vector2 middlepoint;
    MapCube[,] heat_matrix;


    [MenuItem("Window/Data Analysis/HeatMap")]
    public static void ShowWindow()
    {

        GetWindow<HeatMap>("HeatMap");
    }

    private void OnGUI()
    {
        GUILayout.Label(SceneManager.GetActiveScene().name, EditorStyles.boldLabel);

        size = EditorGUILayout.Vector2Field("Map Size", size);
        GenerateHeatmap();
        if (GUILayout.Button("Generate"))
        {
            GenerateHeatmap();
        }
    }

    private void GenerateHeatmap()
    {
       for(int i = 0;i<size.x;i++)
        { 
            for(int j = 0;j<size.y;j++)
            {
                heat_matrix[i, j].Draw();
            }
        }
    }



}
